using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Net;
using Android.OS;
using Android.Widget;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Activities
{

    /// <summary>
    /// Backend logic for displaying a fullscreen image
    /// </summary>
    [Activity(Label = "", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class FullscreenImageActivity : Activity
    {
        #region Private Instance Variables

        private ImageView _scaleImage;

        private Button _importImageButton;

        private Button _deleteImageButton;

        private Button _closeButton;

        private string _imageType;

        #endregion

        /// <summary>
        /// Set the view and initalize the activity
        /// </summary>
        /// <param name="savedInstanceState">Saved instance state.</param>
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScaleImage);

            FindElementsById();

            _scaleImage.SetImageBitmap(GameInfoActivity.CurrentImageBitmap);

            _imageType = Intent.GetStringExtra("ImageType");

            CreateEventHandlers();
        }

        /// <summary>
        /// Dynamically set the window title to specify the image type
        /// </summary>
		public override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			Window.SetTitle(_imageType + " Image");
		}

        /// <summary>
        /// Find all elements based on their unique ids
        /// </summary>
        private void FindElementsById()
        {
            _scaleImage = FindViewById<ImageView>(Resource.Id.FullscreenImageViewId);
            _importImageButton = FindViewById<Button>(Resource.Id.ImportImageButton);
            _deleteImageButton = FindViewById<Button>(Resource.Id.DeleteImageButton);
            _closeButton = FindViewById<Button>(Resource.Id.CloseImageButton);
        }

        /// <summary>
        /// Create event handlers for all interactive elements within the current view
        /// </summary>
        private void CreateEventHandlers()
        {
            _importImageButton.Click += (sender, e) =>
            {
                if (GameInfoActivity.CurrentImageBitmap != null)
                {
                    Toast.MakeText(ApplicationContext, "Please delete the current image first", ToastLength.Long).Show();
                }
                else
                {
                    var imageIntent = new Intent();
                    imageIntent.SetType("image/*");
                    imageIntent.SetAction(Intent.ActionGetContent);
                    StartActivityForResult(Intent.CreateChooser(imageIntent, $"Select new {_imageType} image"), 0);
                }
            };

            _deleteImageButton.Click += (sender, e) =>
            {
                
                var sdCardPath = Environment.ExternalStorageDirectory.Path;
                string consoleName = MainActivity.CurrentGame.ConsoleName;
                string gameName = MainActivity.CurrentGame.ConsoleName;
                string filePath = sdCardPath + ConstPaths.GameImagesPath + consoleName + "/" + gameName + $"_{_imageType}.jpg";

                //Attempt to delete the current image if it exists
				if (File.Exists(filePath))
				{
                    File.Delete(filePath);
                    Toast.MakeText(ApplicationContext, "Image deleted", ToastLength.Long).Show();
                    GameInfoActivity.CurrentImageBitmap = null;
				}
                else{
                    Toast.MakeText(ApplicationContext, "Could not delete image", ToastLength.Long).Show();
                }
            };

            _closeButton.Click += (sender, e) =>
            {
                Finish();
            };
        }

        /// <summary>
        /// Handles the result of the image import operation
        /// </summary>
        /// <param name="requestCode">Request code.</param>
        /// <param name="resultCode">Result code.</param>
        /// <param name="data">The target image URI</param>
		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);

			if (resultCode == Result.Ok)
			{
				Uri imageUri = data.Data;
                string imageFilePath = imageUri.Path;

				var sdCardPath = Environment.ExternalStorageDirectory.Path;
                string destImagePath = sdCardPath + ConstPaths.GameImagesPath + MainActivity.CurrentGame.ConsoleName + "/" + MainActivity.CurrentGame.ConsoleName + $"_{_imageType}.jpg";
                File.Copy(imageFilePath, destImagePath);
                Toast.MakeText(ApplicationContext, "Image imported", ToastLength.Long).Show();
			}
            else{
                Toast.MakeText(ApplicationContext, "Failed to import imgage", ToastLength.Long).Show();
            }

            Finish();
		}
    }
}
