
using Android.App;
using Android.OS;

namespace UniCadeAndroid.Activities
{
    [Activity(Label = "UniCade Cloud Login", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation, ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class LoginActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LoginView);
        }
    }
}
