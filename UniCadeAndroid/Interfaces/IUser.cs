using System.Collections.Generic;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Interfaces
{
    public interface IUser
    {
        #region Properties

        /// <summary>
        /// The max allowed ESRB for the current user (Parental Controls)
        /// </summary>
        Enums.EsrbRatings AllowedEsrbRatings { get; set; }

        /// <summary>
        /// The user's email address
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// The filename and location for the user's profile pictur
        /// </summary>
        string ProfilePicture { get; set; }


        /// <summary>
        /// A brief description of the user
        /// </summary>
        string UserInfo { get; set; }

        /// <summary>
        /// The current user's username
        /// </summary>
        string Username { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Update the password for the current user
        /// </summary>
        /// <param name="password">The new password</param>
        /// <returns>true if the password was changed successfully</returns>
        bool SetUserPassword(string password);

        /// <summary>
        /// Returns true if the entered password is correct
        /// </summary>
        /// <returns>true if the password matches the saved hash</returns>
        bool ValidatePassword(string password);


        /// <summary>
        /// Return the total numer of games this user has launched
        /// </summary>
        /// <returns>userLaunchCount</returns>
        int GetUserLaunchCount();


        /// <summary>
        /// Incriment the launch count for the current user by 1
        /// </summary>
        void IncrementUserLaunchCount();

        /// <summary>
        /// Return the total number of times this user has logged in
        /// </summary>
        /// <returns>userLoginCount</returns>
        int GetUserLoginCount();

        /// <summary>
        /// Incriment the login count for the current user by 1
        /// </summary>
        void IncrementUserLoginCount();

        /// <summary>
        ///Add a favorie game to the current favorites list
        /// </summary>
        bool AddFavorite(IGame game);

        /// <summary>
        /// Remove a game from the current favorites list
        /// </summary>
        bool RemoveFavorite(IGame game);

        /// <summary>
        /// Return a copy of the current favories list of IGame Objects
        /// </summary>
        List<IGame> GetFavoritesList();

        #endregion
    }
}