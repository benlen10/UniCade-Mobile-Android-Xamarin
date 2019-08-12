
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
    }
}
