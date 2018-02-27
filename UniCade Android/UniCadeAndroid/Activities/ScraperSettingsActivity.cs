
using Android.App;
using Android.OS;
using Android.Widget;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "Web Scraper Settings")]
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
                //TODO:  Apply Settings
            };

        }
    }
}
