using System.IO;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Activities
{

    [Activity(Label = "", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class FullscreenImageActivity : Activity
    {
        #region Private Instance Variables

        private ImageView _scaleImage;

        private Button _importImageButton;

        private Button _deleteImageButton;

        private Button _closeButton;

        private string _imageType;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScaleImage);

            FindElementsById();

            _scaleImage.SetImageBitmap(GameInfoActivity.CurrentImageBitmap);

            _imageType = Intent.GetStringExtra("ImageType");

            CreateEventHandlers();
        }

		public override void OnAttachedToWindow()
		{
			base.OnAttachedToWindow();
			Window.SetTitle(_imageType + " Image");
		}

        private void FindElementsById()
        {
            _scaleImage = FindViewById<ImageView>(Resource.Id.FullscreenImageViewId);
            _importImageButton = FindViewById<Button>(Resource.Id.ImportImageButton);
            _deleteImageButton = FindViewById<Button>(Resource.Id.DeleteImageButton);
            _closeButton = FindViewById<Button>(Resource.Id.CloseImageButton);
        }

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
				if (File.Exists(filePath))
				{
                    File.Delete(filePath);
                    Toast.MakeText(ApplicationContext, "Image deleted", ToastLength.Long).Show();
                    GameInfoActivity.CurrentImageBitmap = null;
				}
                else{
                    Toast.MakeText(ApplicationContext, "Could not delete imgae", ToastLength.Long).Show();
                }
                Finish();
            };

            _closeButton.Click += (sender, e) =>
            {
                Finish();
            };
        }

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
