using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Interfaces;

namespace UniCadeAndroid.Objects
{
    [DataContract]
    internal class User : IUser
    {

        #region Properties

        /// <summary>
        /// The current username
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Username cannot be null");
                }
                if (_userExists && _username.Equals("UniCade"))
                {
                    throw new ArgumentException("Default UniCade account cannot be renamed");
                }
                if (_userExists && (Database.GetUser(value) != null))
                {
                    throw new ArgumentException("Username already exists");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Username contains invalid characters");
                }
                if (value.Length < ConstValues.MinUsernameLength)
                {
                    throw new ArgumentException("Username must be at least 4 chars");
                }
                if (value.Length > ConstValues.MaxUsernameLength)
                {
                    throw new ArgumentException($"Username cannot exceed {ConstValues.MaxUsernameLength} chars");
                }
                _username = value;
            }
        }

        /// <summary>
        /// A brief description of the user
        /// </summary>
        public string UserInfo
        {
            get => _userInfo;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("User info cannot be null");
                }
                if (_userExists && _username.Equals("UniCade"))
                {
                    throw new ArgumentException("User info for the default UniCade account cannot be edited");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("User info contains invalid characters");
                }
                if (value.Length > ConstValues.MaxUserInfoLength)
                {
                    throw new ArgumentException($"User info cannot exceed {ConstValues.MaxUserInfoLength} chars");
                }
                _userInfo = value;
            }
        }

        /// <summary>
        /// The user's email address
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Email cannot be null");
                }
                if (_userExists && _username.Equals("UniCade"))
                {
                    throw new ArgumentException("User email for the default UniCade account cannot be edited");
                }
                if (value.Length < 6)
                {
                    throw new ArgumentException("Email must be at least 5 chars");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Email contains invalid characters");
                }
                if (!value.Contains("@"))
                {
                    throw new ArgumentException("Email is invalid");
                }
                if (value.Length > ConstValues.MaxEmailLength)
                {
                    throw new ArgumentException($"Email cannot exceed {ConstValues.MaxEmailLength} chars");
                }
                _email = value;
            }
        }

        /// <summary>
        /// The password for the current user
        /// </summary>
        public string ProfilePicture
        {
            get => _profilePicturePath;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Profile picture path cannot be null");
                }
                if (_userExists && _username.Equals("UniCade"))
                {
                    throw new ArgumentException("Profile pic path for the default UniCade account cannot be edited");
                }
                if (value.Length > ConstValues.MaxPathLength)
                {
                    throw new ArgumentException($"Profile Picture path cannot exceed {ConstValues.MaxPathLength} chars");
                }
                _profilePicturePath = value;
            }
        }

        /// <summary>
        /// The max allowed ESRB for the current user (Parental Controls)
        /// </summary>
        [DataMember]
        public Enums.EsrbRatings AllowedEsrbRatings { get; set; }

        #endregion

        #region Private Instance Variables

        /// <summary>
        /// The current username
        /// </summary>
        [DataMember]
        private string _username;

        /// <summary>
        /// The current user description
        /// </summary>
        [DataMember]
        private string _userInfo;

        /// <summary>
        /// The password for the current user
        /// </summary>
        [DataMember]
        private string _password;

        /// <summary>
        /// The email address for the current user
        /// </summary>
        [DataMember]
        private string _email;

        /// <summary>
        /// The path to the user profile picture
        /// </summary>
        [DataMember]
        private string _profilePicturePath;

        /// <summary>
        /// The total number of games this user has launched
        /// </summary>
        [DataMember]
        private int _userLaunchCount;

        /// <summary>
        /// The total number of times this user has logged in
        /// </summary>
        [DataMember]
        private int _userLoginCount;

        /// <summary>
        /// specifies if this user is teh default unicade user
        /// </summary>
        [DataMember]
        private readonly bool _userExists;

        /// <summary>
        /// A list of the user's favorite games
        /// </summary>
        [DataMember]
        private List<Game> _favoritesList;

        #endregion

        #region Constructors

        public User(string userName, string password, int loginCount, string email, int totalLaunchCount, string userInfo, Enums.EsrbRatings allowedEsrbRatings, string profPic)
        {
            Username = userName;
            SetUserPassword(password);
            _userLoginCount = loginCount;
            _userLaunchCount = totalLaunchCount;
            UserInfo = userInfo;
            AllowedEsrbRatings = allowedEsrbRatings;
            Email = email;
            ProfilePicture = profPic;
            _favoritesList = new List<Game>();
            _userExists = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the password for the current user
        /// </summary>
        /// <param name="password">The new password</param>
        /// <returns>true if the password was changed successfully</returns>
        public bool SetUserPassword(string password)
        {
            if (password == null)
            {
                throw new ArgumentException("Password cannot be null");
            }
            if (_userExists && _username.Equals("UniCade"))
            {
                throw new ArgumentException("Password for the default UniCade account cannot be edited");
            }
            if (password.Length < ConstValues.MinUserPasswordLength)
            {
                throw new ArgumentException($"Password length cannot be less than {ConstValues.MinUserPasswordLength} chars");
            }
            if (password.Length > ConstValues.MaxUserPasswordLength)
            {
                throw new ArgumentException($"Password length cannot exceed {ConstValues.MaxUserPasswordLength} chars");
            }
            if (Utilties.CheckForInvalidChars(password))
            {
                throw new ArgumentException("Password contains invalid characters");
            }

            _password = CryptoEngine.Sha256Hash(password);
            return true;
        }

        /// <summary>
        /// Returns true if the entered password is correct
        /// </summary>
        /// <returns>true if the password matches the saved hash</returns>
        public bool ValidatePassword(string password)
        {
            return _password.Equals(CryptoEngine.Sha256Hash(password));
        }

        /// <summary>
        /// Return the total numer of games this user has launched
        /// </summary>
        /// <returns>userLaunchCount</returns>
        public int GetUserLaunchCount()
        {
            return _userLaunchCount;
        }

        /// <summary>
        /// Incriment the launch count for the current user by 1
        /// </summary>
        public void IncrementUserLaunchCount()
        {
            _userLaunchCount++;
        }

        /// <summary>
        /// Return the total number of times this user has logged in
        /// </summary>
        /// <returns>userLoginCount</returns>
        public int GetUserLoginCount()
        {
            return _userLoginCount;
        }

        /// <summary>
        /// Incriment the login count for the current user by 1
        /// </summary>
        public void IncrementUserLoginCount()
        {
            _userLoginCount++;
        }

        /// <summary>
        /// Add a favorie game to the current favorites list
        /// </summary>
        /// <param name="game">The game to add to the favorites list</param>
        /// <returns>false if a game with the same title and console already exists</returns>
        public bool AddFavorite(IGame game)
        {
            if (_favoritesList.Find(g => g.Title.Equals(game.Title) && g.ConsoleName.Equals(game.ConsoleName))== null)
            {
                _favoritesList.Add((Game) game);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove a game from the current favorites list
        /// </summary>
        public bool RemoveFavorite(IGame game)
        {
            if (game != null)
            {
                return _favoritesList.Remove((Game) game);
            }
            return false;
        }

        /// <summary>
        /// Return a copt of the current favories list of IGame Objects
        /// </summary>
        public List<IGame> GetFavoritesList()
        {
            return new List<IGame>(_favoritesList);
        }

        #endregion
    }
}

