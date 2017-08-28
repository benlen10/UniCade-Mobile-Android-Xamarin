using Android.App;
using Android.Content;
using System.IO;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "GameInfoActivity")]
    public class GameInfoActivity : Activity
    {

		#region Private Instance Variables

        private TextView _titleTextView;

        private TextView _consoleTextView;

        private TextView _publisherTextView;

        private TextView _criticScoreTextView;

        private TextView _playersTextView;

        private TextView _esrbRatingTextView;

        private TextView _esrbDescriptorsTextView;

        private TextView _playersCountTextView;

        private TextView _releaseDateTextView;

        private TextView _descriptionTextView;

        private Button _rescrapeGameButton;

        private Button _rescrapeConsoleButton;

        private Button _saveInfoButton;

        private Button _closeInfoButton;

        private Button _refreshInfoButton;

        private ImageView _boxFrontImageView;

        private ImageView _boxBackImageView;

        private ImageView _screenshotImageView;

        private ImageView _esrbLogoImageView;

		#endregion

		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			// Set the view
			SetContentView(Resource.Layout.SettingsView);

			FindElementsById();

			CreateEventHandlers();

            PopulateGameInfo();

            PopulateGameImages();
        }

        private void PopulateGameInfo()
        {
            _titleTextView.Text = MainActivity.CurrentGame.Title;
            _consoleTextView.Text = MainActivity.CurrentGame.Title;
            _publisherTextView.Text = MainActivity.CurrentGame.Title;
            _criticScoreTextView.Text = MainActivity.CurrentGame.Title;
            _playersTextView.Text = MainActivity.CurrentGame.Title;
            _esrbRatingTextView.Text = MainActivity.CurrentGame.Title;
            _esrbDescriptorsTextView.Text = MainActivity.CurrentGame.Title;
            _releaseDateTextView.Text = MainActivity.CurrentGame.Title;
            _descriptionTextView.Text = MainActivity.CurrentGame.Title;
        }

        private void PopulateGameImages()
        {
            var sdCardPath = Environment.ExternalStorageDirectory.Path;
            string imagePath = sdCardPath + ConstValues.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "\\" + MainActivity.CurrentGame.Title + "_BoxFront.jpg";
            if (File.Exists(imagePath))
            {
                Bitmap bitmap = BitmapFactory.DecodeFile(imagePath);
                _boxFrontImageView.SetImageBitmap(bitmap);
            }

            imagePath = sdCardPath + ConstValues.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "\\" + MainActivity.CurrentGame.Title + "_BoxBack.jpg";
            if (File.Exists(imagePath))
            {
                Bitmap bitmap = BitmapFactory.DecodeFile(imagePath);
                _boxBackImageView.SetImageBitmap(bitmap);
            }

            imagePath = sdCardPath + ConstValues.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "\\" + MainActivity.CurrentGame.Title + "_Screenshot.jpg";
            if (File.Exists(imagePath))
            {
                Bitmap bitmap = BitmapFactory.DecodeFile(imagePath);
                _screenshotImageView.SetImageBitmap(bitmap);
            }
        }


        private void FindElementsById()
        {
            _titleTextView = FindViewById<TextView>(Resource.Id.TitleTextView);
            _consoleTextView = FindViewById<TextView>(Resource.Id.ConsoleTextView);
            _publisherTextView = FindViewById<TextView>(Resource.Id.PublisherTextView);
            _criticScoreTextView = FindViewById<TextView>(Resource.Id.CriticScoreTextView);
            _playersTextView = FindViewById<TextView>(Resource.Id.PlayersTextView);
            _esrbRatingTextView = FindViewById<TextView>(Resource.Id.EsrbRatingTextView);
            _esrbDescriptorsTextView = FindViewById<TextView>(Resource.Id.EsrbDescriptorsTextView);
            _releaseDateTextView = FindViewById<TextView>(Resource.Id.ReleaseDateTextView);
            _descriptionTextView = FindViewById<TextView>(Resource.Id.DescriptonTextView);
            _rescrapeGameButton = FindViewById<Button>(Resource.Id.RescrapeGameButton);
            _rescrapeConsoleButton = FindViewById<Button>(Resource.Id.RescrapeConsoleButton);
            _saveInfoButton = FindViewById<Button>(Resource.Id.SaveButton);
            _closeInfoButton = FindViewById<Button>(Resource.Id.CloseButton);
            _refreshInfoButton = FindViewById<Button>(Resource.Id.RefreshButton);
            _boxFrontImageView = FindViewById<ImageView>(Resource.Id.BoxFrontImageView);
            _boxBackImageView = FindViewById<ImageView>(Resource.Id.BoxBackImageView);
            _screenshotImageView = FindViewById<ImageView>(Resource.Id.ScreenshotImageView);
            _esrbLogoImageView = FindViewById<ImageView>(Resource.Id.EsrbLogoImageView);
        }

        private void CreateEventHandlers(){
            _boxFrontImageView.Click += (sender, e) =>
            {
                //TODO: Expand box front image
            };

            _boxBackImageView.Click += (sender, e) =>
            {
                //TODO: Expand box back image
            };

            _screenshotImageView.Click += (sender, e) =>
            {
                //TODO: Expand screenshot image
            };

            _rescrapeGameButton.Click += (sender, e) =>
            {
                //TODO: 
            };

            _rescrapeConsoleButton.Click += (sender, e) =>
            {
                //TODO: 
            };

            _saveInfoButton.Click += (sender, e) =>
            {
               // SaveGameInfo();
            };

            _closeInfoButton.Click += (sender, e) =>
            {
                //TODO:
            };

            _refreshInfoButton.Click += (sender, e) =>
            {
                //TODO: 
            };

        }
    }
}
