
namespace UniCadeAndroid.Backend
{
    internal class Preferences
    {
        #region Properties

        /// <summary>
        /// Specifies if the UniCade splash screen should be displayed when the interface is launched
        /// </summary>
        public static bool ShowSplashScreen;

        /// <summary>
        /// Specifies if the modern ESRB logos will be displayed instead of the classic logos
        /// </summary>
        public static bool UseModernEsrbLogos;

        /// <summary>
        /// The password required to launch the settings window
        /// </summary>
        public static string PasswordProtection;

        /// <summary>
        /// The user name for the current license holder
        /// </summary>
        public static string UserLicenseName;

        /// <summary>
        /// The curent license key
        /// </summary>
        public static string UserLicenseKey;

        /// <summary>
        /// True if the current license key is valid
        /// </summary>
        public static bool IsLicenseValid = false;

     
        #endregion
    }
}