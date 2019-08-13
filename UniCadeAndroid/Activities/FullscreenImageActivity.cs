using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Activities
{

    [Activity(Label = "FullscreemImage")]
    public class FullscreenImageActivity : Activity
    {
        #region Private Instance Variables

        private ImageView _scaleImage;

        private Button _importImageButton;

        private Button _deleteImageButton;

        private Button _closeButton;

        #endregion

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.ScaleImage);

            FindElementsById();

            _scaleImage.SetImageBitmap(GameInfoActivity.CurrentImageBitmap);
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
                Toast.MakeText(ApplicationContext, "Import new Image", ToastLength.Long).Show();
                //TODO: Import imgae
            };

            _deleteImageButton.Click += (sender, e) =>
            {
                
                var sdCardPath = Environment.ExternalStorageDirectory.Path;
                string consoleName = MainActivity.CurrentGame.ConsoleName;
                string gameName = MainActivity.CurrentGame.ConsoleName;
				string filePath = sdCardPath + ConstPaths.GameImagesPath + consoleName + "/" + gameName + "_BoxFront.jpg";
				if (File.Exists(filePath))
				{
                    File.Delete(filePath);
                    Toast.MakeText(ApplicationContext, "Image deleted", ToastLength.Long).Show();
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
    }
}
