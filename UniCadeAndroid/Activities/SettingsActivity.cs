
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        #region Private Instance Variables

        private Button _loadDatabaseButton;

        private Button _loadBackupButton;

        private Button _saveDatabaseButton;

        private Button _backupDatabaseButton;

        private CheckBox _loadDatabaseOnStartupCheckbox;

        private CheckBox _rescanLocalLibrariesOnStartupCheckbox;

        private CheckBox _displayConsoleLogoCheckbox;

        private CheckBox _displayEsrbLogoCheckbox;

        private Button _deleteAllLocalImagesButton;

        private Button _unicadeCloudButton;

        private Button _webScraperSettingsButton;

        private EditText _passwordEditText;

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
            _loadDatabaseOnStartupCheckbox = FindViewById<CheckBox>(Resource.Id.LoadDatabaseOnStartupCheckbox);
            _rescanLocalLibrariesOnStartupCheckbox = FindViewById<CheckBox>(Resource.Id.RescanLocalLibrariesOnStartupCheckbox);
            _displayConsoleLogoCheckbox = FindViewById<CheckBox>(Resource.Id.DisplayConsoleLogoCheckbox);
            _displayEsrbLogoCheckbox = FindViewById<CheckBox>(Resource.Id.DisplayEsrbLogoCheckbox);
            _deleteAllLocalImagesButton = FindViewById<Button>(Resource.Id.DeleteAllLocalImagesButton);
            _unicadeCloudButton = FindViewById<Button>(Resource.Id.UniCadeCloudButton);
            _webScraperSettingsButton = FindViewById<Button>(Resource.Id.WebScraperSettingsButton);
            _passwordEditText = FindViewById<EditText>(Resource.Id.PasswordEditText);
            _enterLicenseButton = FindViewById<Button>(Resource.Id.EnterLicenseKeyButton);
            _applyButton = FindViewById<Button>(Resource.Id.ApplyButton);
            _closeSettingsButton = FindViewById<Button>(Resource.Id.CloseButton);
		}

		private void LinkClickHandlers()
		{
            _webScraperSettingsButton.Click += (sender, e) =>
			{
                var intent = new Intent(this, typeof(ScraperSettingsActivity));
				StartActivity(intent);
			};

			_unicadeCloudButton.Click += (sender, e) =>
			{
                var intent = new Intent(this, typeof(LoginActivity));
				StartActivity(intent);
			};
		}

	}
}
