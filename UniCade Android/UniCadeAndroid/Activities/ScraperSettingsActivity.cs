
using Android.App;
using Android.OS;
using Android.Widget;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "Web Scraper Settings")]
    public class ScraperSettingsActivity : Activity
    {
        #region Private Instance Variables

        private Button _applyButton;

        private Button _closeButton;

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
            _applyButton = FindViewById<Button>(Resource.Id.ApplyButton);
            _closeButton = FindViewById<Button>(Resource.Id.CloseButton);
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
