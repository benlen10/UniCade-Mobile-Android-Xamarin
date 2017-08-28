
namespace UniCadeAndroid.Constants
{
    internal static class ConstValues
    {
        #region Constants 

        /// <summary>
        /// The maximum number of consoles allowed 
        /// </summary>
        internal const int MaxConsoleCount = 100;

        /// <summary>
        /// The maximum number of games allowed per console
        /// </summary>
        internal const int MaxGameCount = 5000;

        /// <summary>
        /// The maximum number of users allowed 
        /// </summary>
        internal const int MaxUserCount = 50;

        /// <summary>
        /// The max length for console
        /// </summary>
        internal const int MaxConsoleNameLength = 35;

        /// <summary>
        /// The max length for directory paths
        /// </summary>
        internal const int MaxPathLength = 1000;

        /// <summary>
        /// The min length for directory paths
        /// </summary>
        internal const int MinPathLength = 4;

        /// <summary>
        /// The max length a ROM file extension
        /// </summary>
        internal const int MaxFileExtLength = 1000;

        /// <summary>
        /// The max length a the console info
        /// </summary>
        internal const int MaxConsoleInfoLength = 5000;

        /// <summary>
        /// The max length a the console info
        /// </summary>
        internal const int MaxLaunchParamsLength = 1000;

        /// <summary>
        /// The max char length for a username
        /// </summary>
        internal const int MaxUsernameLength = 30;

        /// <summary>
        /// The min char length for a username
        /// </summary>
        internal const int MinUsernameLength = 4;

        /// <summary>
        /// The max char length for user info descriptions
        /// </summary>
        internal const int MaxUserInfoLength = 200;

        /// <summary>
        /// The max char length for user email addresses
        /// </summary>
        internal const int MaxEmailLength = 200;

        /// <summary>
        /// The max char length for user email addresses
        /// </summary>
        internal const int MinEmailLength = 4;

        /// <summary>
        /// The max char length for game filenames
        /// </summary>
        internal const int MaxGameFilenameLength = 200;

        /// <summary>
        /// The max char length for game titles
        /// </summary>
        internal const int MaxGameTitleLength = 200;

        /// <summary>
        /// The max char length for game descriptions
        /// </summary>
        internal const int MaxGameDescriptionLength = 5000;

        /// <summary>
        /// The max char length for game publisher names
        /// </summary>
        internal const int MaxPublisherDeveloperLength = 500;

        /// <summary>
        /// The max char length for game generes
        /// </summary>
        internal const int MaxGameGenreLength = 500;

        /// <summary>
        /// The max char length for game tags
        /// </summary>
        internal const int MaxGameTagsLength = 500;

        /// <summary>
        /// The max char length for game user/critic review scores
        /// </summary>
        internal const int MaxGameReviewScoreLength = 20;

        /// <summary>
        /// The max char length for game trivia
        /// </summary>
        internal const int MaxGameTriviaLength = 500;

        /// <summary>
        /// The max char length for the supported player count
        /// </summary>
        internal const int MaxGamePlayercountLength = 500;

        /// <summary>
        /// The max number of esrb descriptors for a game
        /// </summary>
        internal const int MaxEsrbDescriptorsCount = 10;

        /// <summary>
        /// The max char length for game esrb summary
        /// </summary>
        internal const int MaxGameEsrbSummaryLength = 1000;

        /// <summary>
        /// The max char length for a console's CPU description
        /// </summary>
        internal const int MaxConsoleCpuStringLength = 50;

        /// <summary>
        /// The max char length for a console's RAM description
        /// </summary>
        internal const int MaxConsoleRamStringLength = 50;

        /// <summary>
        /// The max char length for a console's graphcis card description
        /// </summary>
        internal const int MaxConsoleGraphicsStringLength = 50;

        /// <summary>
        /// The max char length for a console's max display resolution description
        /// </summary>
        internal const int MaxConsoleDisplayResolutionLength = 50;

        /// <summary>
        /// The max char length for a console's user review score
        /// </summary>
        internal const int MaxConsoleRatingLength = 50;

        /// <summary>
        /// The max char length for additional info for a console
        /// </summary>
        internal const int MaxAdditionalConsoleInfoLength = 5000;

        /// <summary>
        /// The max char length for a web link
        /// </summary>
        internal const int MaxWebLinkLength = 1000;

        /// <summary>
        /// The min length for a User password
        /// </summary>
        internal const int MinUserPasswordLength = 4;

        /// <summary>
        /// The max length for a User password
        /// </summary>
        internal const int MaxUserPasswordLength = 30;
    
        /// <summary>
        /// The min length for a game filename
        /// </summary>
        internal const int MinGameFileNameLength = 3;

        /// <summary>
        /// The min length for a game filename
        /// </summary>
        internal const int MaxGameFileNameLength = 30;

        /// <summary>
        /// The min length for a game filename
        /// </summary>
        internal const string DatabaseFilePath = @"/UniCade/database.xml";

        /// <summary>
        /// The min length for a game filename
        /// </summary>
        internal const string PreferencesFilePath = @"/UniCade/preferences.xml";

        /// <summary>
        /// The current hash key used to generate the license key
        /// </summary>
        internal const string HashKey = "JI3vgsD6Nc6VSMrNw0b4wvuJmDw6Lrld";

        /// <summary>
        /// The current API key for MobyGames
        /// </summary>
        internal const string MobyGamesApiKey = "xkaobsFA5xGdr8PEBtT8Zg==";

        /// <summary>
        /// The current path for the SQL database file
        /// </summary>
        internal const string SqlDatabaseFileName = @"/UniCade/Unicade.sqlite";

        /// <summary>
        /// The path to the console images media folder
        /// </summary>
        internal const string ConsoleImagesPath = @"/UniCade/Media/Consoles/";

        /// <summary>
        /// The path to the console images media folder
        /// </summary>
        internal const string ConsoleLogoImagesPath = @"/UniCade/Media/ConsolesLogos/";

        /// <summary>
        /// The path to the game images base folder
        /// </summary>
        internal const string GameImagesPath = @"/UniCade/Media/Games/";

        #endregion

        #region  Static Readonly Fields

        /// <summary>
        /// Global invalid characters 
        /// </summary>
        internal static readonly char[] InvalidChars = { '|', '*' };

        #endregion

   
    }
}
