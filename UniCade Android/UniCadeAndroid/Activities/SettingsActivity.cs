using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "UniCade Mobile Settings")]
    public class SettingsActivity : Activity
    {
        #region Private Instance Variables

        private Button _loadDatabaseButton;

        private Button _loadBackupButton;

        private Button _saveDatabaseButton;

        private Button _backupDatabaseButton;

        private CheckBox _showSplashScreenCheckbox;

        private CheckBox _passwordProtectSettingsCheckBox;

        private CheckBox _enableFingerprintProtectionCheckbox;

        private CheckBox _displayModernEsrbIconsCheckBox;

        private Button _deleteAllLocalImagesButton;

        private Button _unicadeCloudButton;

        private Button _webScraperSettingsButton;

        private Button _enterLicenseButton;

        private Button _applyButton;

        private Button _closeSettingsButton;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set the view
            SetContentView(Resource.Layout.SettingsView);

            FindElementsById();

            LinkClickHandlers();

        }

		private void FindElementsById()
		{
            _loadDatabaseButton = FindViewById<Button>(Resource.Id.LoadDatabaseButton);
            _loadBackupButton = FindViewById<Button>(Resource.Id.LoadBackupButton);
            _saveDatabaseButton = FindViewById<Button>(Resource.Id.SaveDatabaseButton);
            _backupDatabaseButton = FindViewById<Button>(Resource.Id.BackupDatabaseButton);
            _showSplashScreenCheckbox = FindViewById<CheckBox>(Resource.Id.ShowSplashScreenCheckbox);
            _passwordProtectSettingsCheckBox = FindViewById<CheckBox>(Resource.Id.PasswordProtectSettingsCheckbox);
            _enableFingerprintProtectionCheckbox = FindViewById<CheckBox>(Resource.Id.EnableFingerprintSecurityCheckbox);
            _displayModernEsrbIconsCheckBox = FindViewById<CheckBox>(Resource.Id.DisplayModernESRBIconsCheckbox);
            _deleteAllLocalImagesButton = FindViewById<Button>(Resource.Id.DeleteAllLocalImagesButton);
            _unicadeCloudButton = FindViewById<Button>(Resource.Id.UniCadeCloudButton);
            _webScraperSettingsButton = FindViewById<Button>(Resource.Id.WebScraperSettingsButton);
            _enterLicenseButton = FindViewById<Button>(Resource.Id.EnterLicenseKeyButton);
            _applyButton = FindViewById<Button>(Resource.Id.ApplyButton);
            _closeSettingsButton = FindViewById<Button>(Resource.Id.CloseButton);
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

        private void HandleLicenseKey(string text){
            Toast.MakeText(this, "License: " + text, ToastLength.Short).Show();
        }

		private void HandleSetPassword(string text)
		{
            if (Utilties.CheckForInvalidChars(text))
            {
                Toast.MakeText(this, "Password contains invalid chars", ToastLength.Short).Show();
                _passwordProtectSettingsCheckBox.Checked = false;
                return;
            }
            if(text.Length < ConstValues.MinUserPasswordLength){
                Toast.MakeText(this, $"Password must be at least {ConstValues.MinUserPasswordLength} chars", ToastLength.Short).Show();
                _passwordProtectSettingsCheckBox.Checked = false;
				return;
            }
            if (text.Length > ConstValues.MaxUserPasswordLength)
			{
				Toast.MakeText(this, $"Password must be less than {ConstValues.MinUserPasswordLength} chars", ToastLength.Short).Show();
                _passwordProtectSettingsCheckBox.Checked = false;
				return;
			}
            _passwordProtectSettingsCheckBox.Checked = true;
            Program.PasswordProtection = text;
		}

		private void LinkClickHandlers()
		{
		    _loadDatabaseButton.Click += (sender, e) =>
		    {
		        FileOps.LoadDatabase();
		    };

		    _loadBackupButton.Click += (sender, e) =>
		    {
		        FileOps.LoadDatabase(ConstPaths.DatabaseFileBackupPath);
            };

		    _saveDatabaseButton.Click += (sender, e) =>
		    {
		        FileOps.SaveDatabase();
		    };

		    _backupDatabaseButton.Click += (sender, e) =>
		    {
		        FileOps.SaveDatabase(ConstPaths.DatabaseFileBackupPath);
		    };

		    _deleteAllLocalImagesButton.Click += (sender, e) =>
		    {

		    };

		    _unicadeCloudButton.Click += (sender, e) =>
		    {
		        var intent = new Intent(this, typeof(LoginActivity));
		        StartActivity(intent);
		    };

            _webScraperSettingsButton.Click += (sender, e) =>
			{
                var intent = new Intent(this, typeof(ScraperSettingsActivity));
				StartActivity(intent);
			};

            _passwordProtectSettingsCheckBox.Click += (sender, e) =>
			{
                if(_passwordProtectSettingsCheckBox.Checked){
                    Program.PasswordProtection = "";
                    Toast.MakeText(this, "Password cleared", ToastLength.Short).Show();
                }
                else{
                    ShowInputDialog("Please enter a new password", HandleSetPassword);
                }
			};

		    _enterLicenseButton.Click += (sender, e) =>
		    {
                ShowInputDialog("Enter License Key", HandleLicenseKey);
		    };

            _closeSettingsButton.Click += (sender, e) =>
		    {
		        Finish();
		    };

		    _applyButton.Click += (sender, e) =>
		    {
                Program.UseModernEsrbLogos = _displayModernEsrbIconsCheckBox.Checked;
		    };
        }

	}
}
