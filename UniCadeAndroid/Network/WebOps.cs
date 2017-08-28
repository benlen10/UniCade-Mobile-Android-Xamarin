using System.Diagnostics.CodeAnalysis;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Interfaces;

namespace UniCadeAndroid.Network
{
    internal class WebOps
    {
        #region Properties

        /// <summary>
        /// The current API to scan
        /// </summary>
        public static Enums.Api CurrentApi = Enums.Api.ThegamesDb;

        /// <summary>
        /// Specifies if the publisher will be scraped
        /// </summary>
        public static bool ParsePublisher = true;

        /// <summary>
        /// Specifies if the critic score will be scraped
        /// </summary>
        public static bool ParseCriticScore = true;

        /// <summary>
        /// Specifies if the developer will be scraped
        /// </summary>
        public static bool ParseDeveloper = true;

        /// <summary>
        /// Specifies if the description will be scraped
        /// </summary>
        public static bool ParseDescription = true;

        /// <summary>
        /// Specifies if the ESRB rating will be scraped
        /// </summary>
        public static bool ParseEsrbRating = true;

        /// <summary>
        /// Specifies if the ESRB descriptor will be scraped
        /// </summary>
        public static bool ParseEsrbDescriptors = true;

        /// <summary>
        /// Specifies if the player count will be scraped
        /// </summary>
        public static bool ParsePlayerCount = true;

        /// <summary>
        /// Specifies if the release date will be scraped
        /// </summary>
        public static bool ParseReleaseDate = true;

        /// <summary>
        /// Specifies if the genres will be scraped
        /// </summary>
        public static bool ParseGenres = true;

        /// <summary>
        /// Specifies if the the list of other platforms will be scraped
        /// </summary>
        public static bool ParseOtherPlatforms = true;

        /// <summary>
        /// Specifies if user review score will be scraped
        /// </summary>
        public static bool ParseUserScore = true;

        /// <summary>
        /// Specifies if the box front image will be scraped
        /// </summary>
        public static bool ParseBoxFrontImage = true;

        /// <summary>
        /// Specifies if the box back image will be scraped
        /// </summary>
        public static bool ParseBoxBackImage = true;

        /// <summary>
        /// Specifies if the screenshot will be scraped
        /// </summary>
        public static bool ParseScreenshot = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleDeveloper = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleCpu = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleRam = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleGraphics = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleDescription = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleNativeResolution = true;

        /// <summary>
        /// 
        /// </summary>
        public static bool ParseConsoleUserReviews = true;

        #endregion

        #region Public Methods

        /// <summary>
        /// Scrape game info for the specified game from online databases
        /// </summary>
        [SuppressMessage("ReSharper", "UnusedVariable")]
        public static bool ScrapeInfo(IGame game)
        {
            if (game == null) { return false; }

            if (CurrentApi.Equals(Enums.Api.ThegamesDb))
            {
                GamesdbApi.UpdateGameInfo(game);
            }
            else if (CurrentApi.Equals(Enums.Api.MobyGames))
            {
                var result = MobyGamesApi.FetchGameInfo(game);
            }
            else if (CurrentApi.Equals(Enums.Api.Igdb))
            {
                //TODO
            }
            return true;
        }

        #endregion
    }
}
