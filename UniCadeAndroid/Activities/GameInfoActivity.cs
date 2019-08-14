using System;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Widget;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Network;
using Environment = Android.OS.Environment;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "Game Info", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameInfoActivity : Activity
    {

		#region Properties

		public static Bitmap CurrentImageBitmap;

		#endregion

		#region Private Instance Variables

		private TextView _titleTextView;

        private TextView _consoleTextView;

        private TextView _publisherTextView;

        private TextView _criticScoreTextView;

        private TextView _playersTextView;

        private TextView _esrbRatingTextView;

        private TextView _esrbDescriptorsTextView;

        private TextView _releaseDateTextView;

        private TextView _launchCountTextView;

        private TextView _descriptionTextView;

        private Button _downloadInfoButton;

        private Button _saveInfoButton;

        private Button _closeInfoButton;

        private ImageView _boxFrontImageView;

        private ImageView _boxBackImageView;

        private ImageView _screenshotImageView;

        private ImageView _esrbLogoImageView;

        private Bitmap _boxFrontBitmap;

        private Bitmap _boxBackBitmap;

		private Bitmap _screenshotBitmap;

		#endregion

		/// <summary>
		/// Set the view and initalize the activity
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

			// Set the view
			SetContentView(Resource.Layout.GameInfoView);

			FindElementsById();

			CreateEventHandlers();

            RefreshGameInfo();

            PopulateGameImages();
        }

        private void RefreshGameInfo()
        {
            Console.WriteLine("Debug");
            _titleTextView.Text = "Title: " + MainActivity.CurrentGame.Title;
            _consoleTextView.Text = "Console: " + MainActivity.CurrentGame.ConsoleName;
            _publisherTextView.Text = "Publisher: " + MainActivity.CurrentGame.PublisherName;
            _criticScoreTextView.Text = "Critic Score: " + MainActivity.CurrentGame.CriticReviewScore;
            _playersTextView.Text = "Player Count: " + MainActivity.CurrentGame.SupportedPlayerCount;
            _esrbRatingTextView.Text = "ESRB Rating: " + MainActivity.CurrentGame.EsrbRating.GetStringValue();
            _esrbDescriptorsTextView.Text = "ESRB Descriptors: " + MainActivity.CurrentGame.GetEsrbDescriptorsString();
            _launchCountTextView.Text = "Launch Count: " + MainActivity.CurrentGame.GetLaunchCount();
            _releaseDateTextView.Text = "Release Date: " + MainActivity.CurrentGame.ReleaseDate;
            _descriptionTextView.Text = "Description:" + MainActivity.CurrentGame.Description;
            _esrbLogoImageView.SetImageResource(Utilties.GetEsrbLogoImage(MainActivity.CurrentGame.EsrbRating));
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


		private void PopulateGameImages()
        {
            var sdCardPath = Environment.ExternalStorageDirectory.Path;
            string imagePath = sdCardPath + ConstPaths.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "/" + MainActivity.CurrentGame.Title + "_BoxFront.jpg";
            if (File.Exists(imagePath))
            {
                _boxFrontBitmap = BitmapFactory.DecodeFile(imagePath);
                _boxFrontImageView.SetImageBitmap(_boxFrontBitmap);
            }
            else{
                _boxFrontBitmap = null;
            }

            imagePath = sdCardPath + ConstPaths.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "/" + MainActivity.CurrentGame.Title + "_BoxBack.jpg";
            if (File.Exists(imagePath))
            {
                _boxBackBitmap = BitmapFactory.DecodeFile(imagePath);
                _boxBackImageView.SetImageBitmap(_boxBackBitmap);
            }
			else
			{
                _boxBackBitmap = null;
			}

            imagePath = sdCardPath + ConstPaths.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "/" + MainActivity.CurrentGame.Title + "_Screenshot.jpg";
            if (File.Exists(imagePath))
            {
                _screenshotBitmap = BitmapFactory.DecodeFile(imagePath);
                _screenshotImageView.SetImageBitmap(_screenshotBitmap);
            }
			else
			{
                _screenshotBitmap = null;
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
            _launchCountTextView = FindViewById<TextView>(Resource.Id.LaunchCountTextView);
            _releaseDateTextView = FindViewById<TextView>(Resource.Id.ReleaseDateTextView);
            _descriptionTextView = FindViewById<TextView>(Resource.Id.DescriptonTextView);
            _downloadInfoButton = FindViewById<Button>(Resource.Id.DownloadInfoButton);
            _saveInfoButton = FindViewById<Button>(Resource.Id.SaveButton);
            _closeInfoButton = FindViewById<Button>(Resource.Id.CloseButton);
            _boxFrontImageView = FindViewById<ImageView>(Resource.Id.BoxFrontImageView);
            _boxBackImageView = FindViewById<ImageView>(Resource.Id.BoxBackImageView);
            _screenshotImageView = FindViewById<ImageView>(Resource.Id.ScreenshotImageView);
            _esrbLogoImageView = FindViewById<ImageView>(Resource.Id.EsrbLogoImageView);

            //Allow the descripton textview to scroll vertically
            _descriptionTextView.MovementMethod = new Android.Text.Method.ScrollingMovementMethod();
        }

        private void CreateEventHandlers(){
            _titleTextView.Click += (sender, e) =>
			{
				Toast.MakeText(ApplicationContext, "Title field is readonly", ToastLength.Long).Show();
			};

            _consoleTextView.Click += (sender, e) =>
			{
				Toast.MakeText(ApplicationContext, "Console field is readonly", ToastLength.Long).Show();
			};

            _publisherTextView.Click += (sender, e) =>
			{
                  ShowInputDialog("Enter Publisher", HandlePublisher);
			};

            _criticScoreTextView.Click += (sender, e) =>
			{
                ShowInputDialog("Enter Critic Score", HandleCriticScore);
			};

            _playersTextView.Click += (sender, e) =>
			{
                ShowInputDialog("Enter Player Count", HandlePlayersCount);
			};

            _esrbRatingTextView.Click += (sender, e) =>
			{
                ShowInputDialog("Enter ESRB Rating", HandleEsrbRating);
			};

            _esrbDescriptorsTextView.Click += (sender, e) =>
			{
                ShowInputDialog("Enter ESRB Descriptors", HandleEsrbRating);
			};

            _launchCountTextView.Click += (sender, e) =>
			{
				Toast.MakeText(ApplicationContext, "Launch count is readonly", ToastLength.Long).Show();
			};

            _releaseDateTextView.Click += (sender, e) =>
			{
                ShowInputDialog("Enter Release Date", HandleReleaseDate);
			};

            _descriptionTextView.Click += (sender, e) =>
			{
                ShowInputDialog("Enter Description", HandleDescription);
			};
            
            _boxFrontImageView.Click += (sender, e) =>
            {
                if(_boxFrontBitmap == null){
                    DisplayToast("No box front image present");
                }
				CurrentImageBitmap = _boxFrontBitmap;
				var intent = new Intent(this, typeof(FullscreenImageActivity));
				intent.PutExtra("ImageType", "BoxFront");
				StartActivity(intent);
            };

            _boxBackImageView.Click += (sender, e) =>
            {
                if (_boxBackBitmap != null)
                {
					DisplayToast("No box back image present");
				}
					CurrentImageBitmap = _boxBackBitmap;
					var intent = new Intent(this, typeof(FullscreenImageActivity));
                    intent.PutExtra("ImageType", "BoxBack");
					StartActivity(intent);

            };

            _screenshotImageView.Click += (sender, e) =>
            {
                if (_screenshotBitmap == null)
                {
                    DisplayToast("No screenshot image present");
                }
					CurrentImageBitmap = _screenshotBitmap;
					var intent = new Intent(this, typeof(FullscreenImageActivity));
                    intent.PutExtra("ImageType", "Screenshot");
					StartActivity(intent);
            };

            _downloadInfoButton.Click += (sender, e) =>
            {
                WebOps.ScrapeInfo(MainActivity.CurrentGame);
                Toast.MakeText(ApplicationContext, "Game Metadata Downloaded", ToastLength.Long).Show();
            };

            _saveInfoButton.Click += (sender, e) =>
            {
                FileOps.SaveDatabase();
                Toast.MakeText(ApplicationContext, "Database Saved", ToastLength.Long).Show();
            };

            _closeInfoButton.Click += (sender, e) =>
            {
                Finish();
            };

        }

		private void HandlePublisher(string text)
		{
               try
               {
                   MainActivity.CurrentGame.PublisherName = text;
               }
               catch (ArgumentException exception)
               {
                   Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
               }
            RefreshGameInfo();
		}

		private void HandleCriticScore(string text)
		{
			try
			{
                MainActivity.CurrentGame.CriticReviewScore = text;
			}
			catch (ArgumentException exception)
			{
				Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
			}
            RefreshGameInfo();
		}

		private void HandlePlayersCount(string text)
		{
			try
			{
                MainActivity.CurrentGame.SupportedPlayerCount = text;
			}
			catch (ArgumentException exception)
			{
				Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
			}
            RefreshGameInfo();
		}

		private void HandleEsrbRating(string text)
		{
			try
			{
                MainActivity.CurrentGame.EsrbRating = Utilties.ParseEsrbRating(text);
			}
			catch (ArgumentException exception)
			{
				Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
			}
            RefreshGameInfo();
		}

		private void HandleEsrbDescriptors(string text)
		{
			try
			{
                MainActivity.CurrentGame.AddEsrbDescriptorsFromString(text);
			}
			catch (ArgumentException exception)
			{
				Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
			}
            RefreshGameInfo();
		}

		private void HandleReleaseDate(string text)
		{
			try
			{
                MainActivity.CurrentGame.ReleaseDate = text;
			}
			catch (ArgumentException exception)
			{
				Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
			}
            RefreshGameInfo();
		}

		private void HandleDescription(string text)
		{
			try
			{
                MainActivity.CurrentGame.Description = text;
			}
			catch (ArgumentException exception)
			{
				Toast.MakeText(ApplicationContext, exception.Message, ToastLength.Long).Show();
			}
            RefreshGameInfo();
		}

		public void DisplayToast(string message, ToastLength length = ToastLength.Short)
		{
			Toast.MakeText(ApplicationContext, message, length).Show();
		}
    }
}
