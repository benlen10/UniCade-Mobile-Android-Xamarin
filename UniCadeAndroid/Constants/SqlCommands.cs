
namespace UniCadeAndroid.Constants
{
    internal static class SqlCommands
    {
        /// <summary>
        /// Creates a new games table for a single user
        /// </summary>
        public const string CreateGamesTable = @"
DROP TABLE IF EXISTS games_[Username];
CREATE TABLE games_[Username](
  filename        TEXT NOT NULL, -- full ROM filename
  title           TEXT NOT NULL, -- Game title
  console         TEXT NOT NULL, -- Console game
  launchCount     INTEGER,   -- Number of times the game has been launched
  releaseDate     TEXT,      -- Release date
  publisher       TEXT,      -- Game publisher
  developer       TEXT,      -- Game developer
  userScore       TEXT,      -- Average user score
  criticScore     TEXT,      -- Average critic score
  players         TEXT,      -- Number of supported local players
  trivia          TEXT,      -- Trivia or extra info related to the game
  esrbRating      TEXT,      -- ESRB content rating
  esrbDescriptors TEXT,      -- ESRB content rating descriptors
  esrbSummary     TEXT,      -- ESRB content rating summary
  description     INTEGER,   -- Brief game description
  genres          TEXT,      -- Main genres associated with the game
  tags            TEXT,      -- Revelant game tags
  otherConsoles   TEXT,      -- A list of other consoles that this game is available for
  videoLink       TEXT,      -- A web link to video gameplay or the game trailer
  gamesDbApiId    TEXT,      -- The game's API id for TheGamesDB
  mobygamesApiId  TEXT,      -- The game's API id for MobyGames
  mobygamesApiUrl TEXT,      -- The game's direct url for MobyGames
  IgdbApiId       TEXT,      -- The game's API id for IGDB
  favorite        INTEGER,   -- int value describing the favorite status
  PRIMARY KEY(title, console));";


        /// <summary>
        /// Creates a new table to store all unicade users
        /// </summary>
        public const string CreateUsersTable = @"
DROP TABLE IF EXISTS users;
CREATE TABLE users(
  username        TEXT,    -- The current user's username (Primary key)
  password        TEXT,    -- The current user's password
  email           TEXT,    -- The current user's email address
  userinfo        TEXT,    -- A brief description of the user
  userLoginCount  INTEGER, -- The number of times the current user has logged in
  userLaunchCount INTEGER, -- The total number of games the user has launched
  allowedEsrb      TEXT,   -- The max ESRB rating the user is allowed to launch
  PRIMARY KEY(username));";


        /// <summary>
        /// Creates a new table to store all consoles
        /// </summary>
        public const string CreateConsolesTable = @"
DROP TABLE IF EXISTS consoles_[Username];
CREATE TABLE consoles_[Username](
  consoleName                TEXT,  -- The common name of the console
  consoleInfo                TEXT,  -- Basic console description and info
  launchParams               TEXT,  -- The launch params for the current emulator   
  releaseDate                TEXT,  -- The original release date for the console
  romExtension               TEXT,  -- The extensions for ROMS belonging to the current console
  romFolderPath              TEXT,  -- The full path to the rom directory for the current console
  emulatorExePath              TEXT,  -- The full path to the rom directory for the current console
  developer                  TEXT,  -- The developer of the console
  cpu                        TEXT,  -- The CPU of the console
  ram                        TEXT,  -- The amount and type of RAM for the console
  graphics                   TEXT,  -- The graphics card for the console
  displayResolution          TEXT,  -- The console's native display resolution
  consoleRating              TEXT,  -- The average user review score for the console
  additionalConsoleInfo      TEXT,  -- Additional info for the console
  gamesDbApiId               TEXT,  -- The console's API id for TheGamesDB
  mobygamesApiId             TEXT,  -- The console's API id for MobyGames
  IgdbApiId                  TEXT,  -- The console's API id for IGDB
  PRIMARY KEY(consoleName));";
    }
}
