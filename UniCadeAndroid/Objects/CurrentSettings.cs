using System.Collections.Generic;
using System.Runtime.Serialization;

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
        /// Specifies if the modern ESRB logos will be displayed instead of the classic logos
        /// </summary>
        [DataMember]
        internal bool UseModernEsrbLogos;

        /// <summary>
        /// If this value is greater than 0, passcode protection is enabled
        /// </summary>
        [DataMember]
        internal string PasswordProtection;
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
