using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Security;
using System.Collections.Generic;
using System.Linq;
using Android;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using UniCadeAndroid.Objects;
using System;
using Android.Hardware.Fingerprints;
using Android.Views;
using Android.Support.V4.Hardware.Fingerprint;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "UniCade Mobile", MainLauncher = true, ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class MainActivity : Activity
    {

        #region Private Instance Variables 

        private Button _settingsButton;

        private ListView _gameSelectionListView;

        private Spinner _consoleSelectionSpinner;

        private EditText _searchBarEditText;

        private TextView _gameCountTextView;

        private ImageView _consoleImageView;

        private string _searchText = "";

        private List<GameListObject> _currentGameList;

		FingerprintManagerApi _dialogFrag;
        FingerprintManagerCompat _fingerprintManager;
		
        readonly string[] _requiredPermissions =
        {
            Manifest.Permission.ReadExternalStorage,
            Manifest.Permission.WriteExternalStorage,
            Manifest.Permission.UseFingerprint
        };

        #endregion

        #region Properties

        /// <summary>
        /// The currently selected IGame object
        /// </summary>
        public static Game CurrentGame;

        #endregion

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            CheckPermissions();
        }

        private void Startup()
        {
            //Initalize the database, preform an initial scan and refresh the total game count
            Database.Initalize();

            //Validate the media directory and attempt to laod both the database.xml and preferences.xml files
            if (!FileOps.StartupScan())
            {
                return;
            }

            //Refresh the total came count across all consoles
            Database.RefreshTotalGameCount();

            SetContentView(Resource.Layout.MainView);

            FindElementsById();

            CreateEventHandlers();

            PopulateConsoleSpinner();

            SetupFingerprintScanner();
        }

        private bool SetupFingerprintScanner(){
            FingerprintManager fingerprintManager = this.GetSystemService(FingerprintService) as FingerprintManager;
            if (!fingerprintManager.IsHardwareDetected){
                DisplayToast("Fingerprint hardware not detected");
                return false;
			}
            if (!fingerprintManager.HasEnrolledFingerprints)
			{
                DisplayToast("No fingerprints are currently enrolled");
				return false;
			}

            //Check for permissions
            Android.Content.PM.Permission permissionResult = ContextCompat.CheckSelfPermission(this, Manifest.Permission.UseFingerprint);
            if(permissionResult == Android.Content.PM.Permission.Denied){
                DisplayToast("Fingerprint Permission Denied");
				return false;
            }

            _fingerprintManager = FingerprintManagerCompat.From(this);
            _dialogFrag = FingerprintManagerApi.NewInstance(_fingerprintManager);
            return true;
        }

		void StartFingerprintScan(object sender, EventArgs args)
		{
			Permission permissionResult = ContextCompat.CheckSelfPermission(this,
																				   Manifest.Permission.UseFingerprint);
			if (permissionResult == Permission.Granted)
			{
				_dialogFrag.Init();
				_dialogFrag.Show(FragmentManager, "fingerprint_auth_fragment");
			}
			else
			{
				DisplayToast("Fingerprint Permission Denied");
			}
		}

        public void PopulateConsoleSpinner()
        {

            List<string> consoleList = Database.GetConsoleList().ToList();
            consoleList.Add("All Games");

            var consoleSpinnerAdapter =
                new ArrayAdapter(this, Resource.Layout.CustomSpinnerItem, consoleList);

            _consoleSelectionSpinner.Adapter = consoleSpinnerAdapter;
        }

        private void CheckPermissions()
        {

            if (ContextCompat.CheckSelfPermission(this, _requiredPermissions[0]) == (int)Permission.Granted)
            {
                Startup();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, _requiredPermissions, 0);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if (ContextCompat.CheckSelfPermission(this, _requiredPermissions[0]) == (int)Permission.Granted)
            {
                Startup();
            }
            else
            {
               DisplayToast("Fatal Error: Storage Access Required");
                Finish();
            }

        }

		public void AuthenticationSuccessful()
		{
            DisplayToast("Fingerprint Authentication Successful!");
			var intent = new Intent(this, typeof(SettingsActivity));
			StartActivity(intent);
		}

		public void FingerprintAuthenticationFailed()
		{
            DisplayToast("Fingerprint Authentication Failed");
		}

        private void RefreshGameList()
        {
            UpdateGameCount();
            _gameSelectionListView.Adapter = null;
            var currentConsoleName = _consoleSelectionSpinner.SelectedItem.ToString();
            _currentGameList = new List<GameListObject>();

            if (currentConsoleName == "All Games")
            {
                foreach (string consoleName in Database.GetConsoleList())
                {
                    foreach (string gameTitle in Database.GetConsole(consoleName).GetGameList())
                    {
                        var item = new GameListObject
                        {
                            Title = gameTitle,
                            Console = consoleName,
                            ImageResourceId = 0
                        };
                        if ((_searchText.Length > 0 && item.Title.ContainsIgnoreCase(_searchText)) || _searchText.Length == 0)
                        {
                            _currentGameList.Add(item);
                        }
                    }
                }
            }
            else
            {
                foreach (var gameTitle in Database.GetConsole(currentConsoleName).GetGameList())
                {
                    var item = new GameListObject
                    {
                        Title = gameTitle,
                        Console = currentConsoleName,
                        ImageResourceId = 0
                    };
                    if ((_searchText.Length > 0 && item.Title.ContainsIgnoreCase(_searchText)) || _searchText.Length == 0)
                    {
                        _currentGameList.Add(item);
                    }
                }
            }

            var gameListAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemActivated2, _currentGameList);
            _gameSelectionListView.Adapter = gameListAdapter;

            _gameSelectionListView.ChoiceMode = ChoiceMode.Single;

            _gameSelectionListView.Adapter = new GameListViewActivity(this, _currentGameList);
            _gameSelectionListView.ItemClick += OnListItemClick;
        }

        private void UpdateGameCount(){
            var currentConsoleName = _consoleSelectionSpinner.SelectedItem.ToString();
            if (currentConsoleName == "All Games")
            {
                _gameCountTextView.Text = "Total Games: " + Database.GetTotalGameCount().ToString();
            }
            else{
                int consoleGameCount = Database.GetConsole(currentConsoleName).GetGameCount();
				_gameCountTextView.Text = "Total Games: " + Database.GetTotalGameCount().ToString() + " Console Games: " + consoleGameCount;
            }
        }

        private void OnListItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var listItem = _currentGameList[e.Position];

            CurrentGame = (Game)Database.GetConsole(listItem.Console).GetGame(listItem.Title);

            if (CurrentGame != null)
            {
                var intent = new Intent(this, typeof(GameInfoActivity));
                StartActivity(intent);
            }
            else
            {
                DisplayToast("Cannot open settings window");
            }
        }

        private void FindElementsById()
        {
            _settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            _gameSelectionListView = FindViewById<ListView>(Resource.Id.GameSelectionListView);
            _consoleSelectionSpinner = FindViewById<Spinner>(Resource.Id.ConsoleSelectionSpinner);
            _searchBarEditText = FindViewById<EditText>(Resource.Id.SearchBarEditTExt);
            _gameCountTextView = FindViewById<TextView>(Resource.Id.GameCountTextView);
            _consoleImageView = FindViewById<ImageView>(Resource.Id.ConsoleImageView);
 }


		protected void ShowInputDialog(string title, Action<string> handlerFunction)
		{

			EditText editText = new EditText(this);
			AlertDialog.Builder dialogBuilder = new AlertDialog.Builder(this);
			dialogBuilder.SetTitle(title);
			dialogBuilder.SetPositiveButton("Enter", (senderAlert, args) =>
			{
				handlerFunction(editText.Text);
			});

			dialogBuilder.SetNegativeButton("Cancel", (senderAlert, args) =>
			{

			});
			dialogBuilder.SetView(editText);
			dialogBuilder.Show();
		}

        private void HandleEnteredPassword(string text){
            if(text == Preferences.PasswordProtection){
				var intent = new Intent(this, typeof(SettingsActivity));
				StartActivity(intent);
            }
            else{
                DisplayToast("Invalid Password");
            }
        }

        private void CreateEventHandlers()
        {
            _settingsButton.Click += (sender, e) =>
            {
                if (Preferences.FingerprintProtectionEnabled)
                {
                    StartFingerprintScan(sender, e);
                }
                else{
					if (Preferences.PasswordProtection != null)
					{
						ShowInputDialog("Please Enter Password", HandleEnteredPassword);
					}
					else
					{
						var intent = new Intent(this, typeof(SettingsActivity));
						StartActivity(intent);
					}
                }
            };

            _consoleSelectionSpinner.ItemSelected += (sender, e) =>
            {
                RefreshGameList();
            };

            _searchBarEditText.TextChanged += (sender, e) =>
            {
				_searchText = _searchBarEditText.Text;
				RefreshGameList();
            };
        }

        public void DisplayToast(string message, ToastLength length=ToastLength.Short){
            Toast.MakeText(ApplicationContext, message, length).Show();
        }
    }
}

