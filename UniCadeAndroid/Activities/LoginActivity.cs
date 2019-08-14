
using Android.App;
using Android.Content.PM;
using Android.OS;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "UniCade Cloud Login", ConfigurationChanges = ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class LoginActivity : Activity
    {
		/// <summary>
		/// Set the view and initalize the activity
		/// </summary>
		/// <param name="savedInstanceState">Saved instance state.</param>
		protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginView);
        }
    }
}
