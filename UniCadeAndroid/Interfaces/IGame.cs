using System.Collections.Generic;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Interfaces
{
    public interface IGame
    {
        #region Properties

        /// <summary>
        /// The name of the console that the game belongs to
        /// </summary>
        string ConsoleName { get;}

        /// <summary>
        /// The average critic review score out of 100
        /// </summary>
        string CriticReviewScore { get; set; }

        /// <summary>
        /// Brief game description or overview
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// The developer of the game
        /// </summary>
        string DeveloperName { get; set; }

        /// <summary>
        /// The ESRB content rating
        /// </summary>
        Enums.EsrbRatings EsrbRating { get; set; }

        /// <summary>
        /// Detailed summary of the ESRB rating
        /// </summary>
        string EsrbSummary { get; set; }

        /// <summary>
        /// A list of other platforms this game is available for
        /// </summary>
        string OtherPlatforms { get; set; }

        /// <summary>
        /// Int value representing the favorite status of the game
        /// </summary>
        bool Favorite { get; set; }

        /// <summary>
        /// The raw filename for the ROM file
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The genere(s) for the current game
        /// </summary>
        string Genres { get; set; }

        /// <summary>
        /// The supported number of players
        /// </summary>
        string SupportedPlayerCount { get; set; }

        /// <summary>
        /// The publisher of the game
        /// </summary>
        string PublisherName { get; set; }

        /// <summary>
        /// The original release date of the game
        /// </summary>
        string ReleaseDate { get; set; }

        /// <summary>
        /// A list of common tags tags for the current gam
        /// </summary>
        string Tags { get; set; }

        /// <summary>
        /// The common title (display name) of the game
        /// </summary>
        string Title { get; }

        /// <summary>
        /// Trivia facts for the current game
        /// </summary>
        string Trivia { get; set; }

        /// <summary>
        /// The average user review score out of 100
        /// </summary>
        string UserReviewScore { get; set; }

        /// <summary>
        /// The average user review score out of 100
        /// </summary>
        string VideoLink { get; set; }

        #endregion

        #region  API IDs

        /// <summary>
        /// The unique game identifier for the GamesDB API
        /// </summary>
        int GamesdbApiId { get; set; }

        /// <summary>
        /// The unique game identifier for the MobyGames API
        /// </summary>
        int MobygamesApiId { get; set; }

        /// <summary>
        /// The direct url for MobyGames
        /// </summary>
        string MobyGamesUrl { get; set; }

        /// <summary>
        /// The unique game identifier for the IGDB API
        /// </summary>
        int IgdbApiId { get; set; }

        #endregion

        #region  Public Methods

        /// <summary>
        /// Return the number of times the game has been launched
        /// </summary>
        /// <returns>LaunchCount</returns>
        int GetLaunchCount();

        /// <summary>
        /// Increments the current launch count by 1
        /// </summary>
        void IncrementLaunchCount();

        /// <summary>
        /// Reets the current launch count to 0
        /// </summary>
        void ResetLaunchCount();

        /// <summary>
        /// Sets the current launch count
        /// </summary>
        void SetLaunchCount(int count);

        /// <summary>
        /// Adds a new ESRB descriptor enum 
        /// Returns false if the same descriptor already exists
        /// </summary>
        /// <param name="descriptor">ESRB descriptor enum object</param>
        /// <returns>false if the desctiptor already exists</returns>
        bool AddEsrbDescriptor(Enums.EsrbDescriptors descriptor);

        /// <summary>
        /// Parse a string and add esrb descriptor enums seperated by ',' 
        /// </summary>
        /// <param name="esrbString"></param>
        void AddEsrbDescriptorsFromString(string esrbString);

        bool DeleteEsrbDescriptor(Enums.EsrbDescriptors descriptor);

        /// <summary>
        /// Remove all ESRB descriptors from the current game object
        /// </summary>
        void ClearEsrbDescriptors();

        /// <summary>
        /// Return the current list of ESRB descriptor enum objects
        /// </summary>
        /// <returns></returns>
        List<Enums.EsrbDescriptors> GetEsrbDescriptors();

        /// <summary>
        /// Return the string representation of the ESRB descriptors
        /// </summary>
        /// <returns>string representation of current ESRB descriptors</returns>
        string GetEsrbDescriptorsString();

        #endregion
    }
}