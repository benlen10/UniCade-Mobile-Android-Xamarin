using System.Collections;
using System.Diagnostics;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Backend
{
    internal class Program
    {
        #region Properties

        /// <summary>
        /// Specifies if the UniCade splash screen should be displayed when the interface is launched
        /// </summary>
        public static bool ShowSplashScreen;

        /// <summary>
        /// Specifies if the the ROM directories should be automatically rescanned 
        /// when the interface is launched
        /// </summary>
        public static bool RescanOnStartup;

        /// <summary>
        /// True if there is a current game process running
        /// </summary>
        public static bool IsProcessActive;

        /// <summary>
        /// The instance of the current game process
        /// </summary>
        public static Process CurrentProcess;

        /// <summary>
        /// Specifies if certain ESRB ratings should be restricted globally (regardless of user)
        /// </summary>
        public static Enums.EsrbRatings RestrictGlobalEsrbRatings;

        /// <summary>
        /// Specifies if the modern ESRB logos will be displayed instead of the classic logos
        /// </summary>
        public static bool UseModernEsrbLogos;

        /// <summary>
        /// Specifies if the command line interface should be launched on startup instead of the GUI
        /// </summary>
        public static bool PerferCmdInterface;

        /// <summary>
        /// Specifies if a loading screen should be displayed when launching a game
        /// </summary>
        public static bool ShowLoadingScreen;

        /// <summary>
        /// The password required to launch the settings window
        /// </summary>
        public static string PasswordProtection;

        /// <summary>
        /// Specifies if ROM files are required to have the proper extension in order to be imported
        /// </summary>
        public static bool EnforceFileExtensions;

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

        /// <summary>
        /// The list of currently active consoles
        /// </summary>
        public static ArrayList ActiveConsoleList;

        /// <summary>
        /// The index number for the currently displayed console
        /// </summary>
        public static int IndexNumber;

        /// <summary>
        /// True if the password entered in the passWindow is valid
        /// </summary>
        public static bool IsPasswordValid;

        /// <summary>
        /// True if the game selection screen is the current screen
        /// </summary>
        public static bool IsGameSelectionPageActive;

        /// <summary>
        /// True if a game is currently running
        /// </summary>
        public static bool IsGameRunning;

        /// <summary>
        /// True if the game info window is currently active
        /// </summary>
        public static bool IsInfoWindowActive;

        /// <summary>
        /// True if the SettingsWindow is currently visible
        /// </summary>
        public static bool IsSettingsWindowActive;

        /// <summary>
        /// True if currenly only favorites are being displayed
        /// </summary>
        public static bool IsFavoritesViewActive;

        /// <summary>
        /// Specifies if the ESRB logo should be displayed while browsing games
        /// </summary>
        public static bool DisplayEsrbWhileBrowsing;

        /// <summary>
        /// Specifies if games with a restricted ESRB rating should be hidden 
        /// </summary>
        public static bool HideRestrictedEsrbGames;

        #endregion
    }
}