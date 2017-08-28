using System.Collections.Generic;
using System.Runtime.Serialization;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Objects
{
    [DataContract]
    public class CurrentSettings
    {
        #region Properties

        /// <summary>
        /// Specifies if the UniCade splash screen should be displayed when the interface is launched
        /// </summary>
        [DataMember]
        internal bool ShowSplashScreen;

        /// <summary>
        /// Specifies if the the ROM directories should be automatically rescanned 
        /// when the interface is launched
        /// </summary>
        [DataMember]
        internal bool RescanOnStartup;

        /// <summary>
        /// Specifies if certain ESRB ratings should be restricted globally (regardless of user)
        /// </summary>
        [DataMember]
        internal Enums.EsrbRatings RestrictGlobalEsrbRatings;

        /// <summary>
        /// Specifies if the modern ESRB logos will be displayed instead of the classic logos
        /// </summary>
        [DataMember]
        internal bool UseModernEsrbLogos;

        /// <summary>
        /// Specifies if the command line interface should be launched on startup instead of the GUI
        /// </summary>
        [DataMember]
        internal bool PerferCmdInterface;

        /// <summary>
        /// Specifies if a loading screen should be displayed when launching a game
        /// </summary>
        [DataMember]
        internal bool ShowLoadingScreen;

        /// <summary>
        /// If this value is greater than 0, passcode protection is enabled
        /// </summary>
        [DataMember]
        internal string PasswordProtection;

        /// <summary>
        /// Specifies if ROM files are required to have the proper extension in order to be imported
        /// </summary>
        [DataMember]
        internal bool EnforceFileExtensions;

        /// <summary>
        /// Specifies is PayPerPlay is enforced
        /// </summary>
        [DataMember]
        internal bool PayPerPlayEnabled;

        /// <summary>
        /// Specifies the number of coins required if payperplay is enabled
        /// </summary>
        [DataMember]
        internal int CoinsRequired;

        /// <summary>
        /// Specifies the current number of coins
        /// </summary>
        [DataMember]
        internal int CurrentCoins;

        /// <summary>
        /// The user name for the current license holder
        /// </summary>
        [DataMember]
        internal string UserLicenseName;

        /// <summary>
        /// The curent license key
        /// </summary>
        [DataMember]
        internal string UserLicenseKey;

        /// <summary>
        /// The list of current users
        /// </summary>
        [DataMember]
        internal List<User> UserList;

        #endregion

        #region Constructors

        /// <summary>
        /// Public constructor for the CurrentSettings class
        /// </summary>
        public CurrentSettings()
        {
            UserList = new List<User>();
        }

        #endregion

    }
}
