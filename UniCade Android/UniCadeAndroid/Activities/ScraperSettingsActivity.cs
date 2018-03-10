using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Widget;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Network;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "UniCAde Web Scraper Settings")]
    public class ScraperSettingsActivity : Activity
    {
        #region Private Instance Variables

        private Spinner _apiSelectionSpinner;

        private Button _applyButton;

        private Button _closeButton;

        private CheckBox _releaseDateCheckbox;

        private CheckBox _criticScoreCheckBox;

        private CheckBox _userScoreCheckbox;

        private CheckBox _publisherDeveloperCheckbox;

        private CheckBox _esrbRatingDescriptorsCheckbox;

        private CheckBox _playerCountCheckBox;

        private CheckBox _descriptionCheckbox;

        private CheckBox _genresCheckbox;

        private CheckBox _otherAvailableConsolesheckbox;

        private CheckBox _boxFrontCheckBox;

        private CheckBox _boxBackCheckBox;

        private CheckBox _screenshotCheckBox;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScraperSettingsView);

            FindElementsById();

            LinkClickHandlers();

            PopulateCheckboxes();

            PopulateApiSelectionSpinner();
        }

        private void PopulateCheckboxes()
        {
            _releaseDateCheckbox.Checked = WebOps.ParseReleaseDate;
            _criticScoreCheckBox.Checked = WebOps.ParseCriticScore;
            _userScoreCheckbox.Checked = WebOps.ParseUserScore;
            _publisherDeveloperCheckbox.Checked = WebOps.ParsePublisher;
            _esrbRatingDescriptorsCheckbox.Checked = WebOps.ParseEsrbRating;
            _playerCountCheckBox.Checked = WebOps.ParsePlayerCount;
            _descriptionCheckbox.Checked = WebOps.ParseDescription;
            _genresCheckbox.Checked = WebOps.ParseGenres;
            _otherAvailableConsolesheckbox.Checked = WebOps.ParseOtherPlatforms;
            _boxFrontCheckBox.Checked = WebOps.ParseBoxFrontImage;
            _boxBackCheckBox.Checked = WebOps.ParseBoxBackImage;
            _screenshotCheckBox.Checked = WebOps.ParseScreenshot;
        }

        private void PopulateApiSelectionSpinner()
        {
            List<string> apiList = (from Enums.Api item in Enum.GetValues(typeof(Enums.Api)) select item.GetStringValue()).ToList();

            var consoleSpinnerAdapter = new ArrayAdapter(this, Resource.Layout.CustomSpinnerItem, apiList);

            _apiSelectionSpinner.Adapter = consoleSpinnerAdapter;
        }

        private void FindElementsById()
        {
            _apiSelectionSpinner = FindViewById<Spinner>(Resource.Id.ApiSelectionSpinner);
            _applyButton = FindViewById<Button>(Resource.Id.ApplyButton);
            _closeButton = FindViewById<Button>(Resource.Id.CloseButton);
            _releaseDateCheckbox = FindViewById<CheckBox>(Resource.Id.ReleaseDateCheckbox);
            _criticScoreCheckBox = FindViewById<CheckBox>(Resource.Id.CriticScoreCheckbox);
            _userScoreCheckbox = FindViewById<CheckBox>(Resource.Id.UserScoreCheckbox);
            _publisherDeveloperCheckbox = FindViewById<CheckBox>(Resource.Id.PubusherDeveloperCheckbox);
            _esrbRatingDescriptorsCheckbox = FindViewById<CheckBox>(Resource.Id.EsrbRatingCheckbox);
            _playerCountCheckBox = FindViewById<CheckBox>(Resource.Id.PlayerCountCheckbox);
            _descriptionCheckbox = FindViewById<CheckBox>(Resource.Id.DescriptionCheckbox);
            _genresCheckbox = FindViewById<CheckBox>(Resource.Id.GenresCheckbox);
            _otherAvailableConsolesheckbox = FindViewById<CheckBox>(Resource.Id.OtherAvailablePlatformsCheckbox);
            _boxFrontCheckBox = FindViewById<CheckBox>(Resource.Id.BoxBackImageCheckbox);
            _boxBackCheckBox = FindViewById<CheckBox>(Resource.Id.BoxBackImageCheckbox);
            _screenshotCheckBox = FindViewById<CheckBox>(Resource.Id.ScreenshotImageCheckbox);
        }

        private void LinkClickHandlers()
        {
            _closeButton.Click += (sender, e) =>
            {
                Finish();
            };

            _applyButton.Click += (sender, e) =>
            {
                WebOps.CurrentApi = (Enums.Api) _apiSelectionSpinner.SelectedItemPosition;
                WebOps.ParseReleaseDate = _releaseDateCheckbox.Checked;
                WebOps.ParseCriticScore = _criticScoreCheckBox.Checked;
                WebOps.ParseUserScore = _userScoreCheckbox.Checked;
                WebOps.ParsePublisher = _publisherDeveloperCheckbox.Checked;
                WebOps.ParseDeveloper = _publisherDeveloperCheckbox.Checked;
                WebOps.ParseEsrbRating = _esrbRatingDescriptorsCheckbox.Checked;
                WebOps.ParseEsrbDescriptors = _esrbRatingDescriptorsCheckbox.Checked;
                WebOps.ParsePlayerCount = _playerCountCheckBox.Checked;
                WebOps.ParseDescription = _descriptionCheckbox.Checked;
                WebOps.ParseGenres = _genresCheckbox.Checked;
                WebOps.ParseOtherPlatforms = _otherAvailableConsolesheckbox.Checked;
                WebOps.ParseBoxFrontImage = _boxFrontCheckBox.Checked;
                WebOps.ParseBoxBackImage = _boxBackCheckBox.Checked;
                WebOps.ParseScreenshot = _screenshotCheckBox.Checked;
            };
        }
    }
}
