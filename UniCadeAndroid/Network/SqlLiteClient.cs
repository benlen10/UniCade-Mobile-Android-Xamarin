using System;
using System.Data.SQLite;
using System.IO;
using System.Text;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Interfaces;
using UniCadeAndroid.Objects;
using Environment = Android.OS.Environment;

namespace UniCadeAndroid.Network
{
    internal static class SqlLiteClient
    {
        #region Properties

        /// <summary>
        /// The current connection to the SQL database
        /// </summary>
        private static SQLiteConnection _connection;

        /// <summary>
        /// The currently logged in username
        /// </summary>
        private static string _currentSqlUsername;

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Create a new local database file and connect
        /// </summary>
        internal static void Connect()
        {
            //If the sql database file does not exist, create it
            var sdCardPath = Environment.ExternalStorageDirectory.Path;
            if (!File.Exists(sdCardPath + ConstPaths.SqlDatabaseFilePath))
            {
                SQLiteConnection.CreateFile(sdCardPath + ConstPaths.SqlDatabaseFilePath);

                //Connect to the new database
                _connection = new SQLiteConnection($"Data Source={sdCardPath + ConstPaths.SqlDatabaseFilePath};Version=3;");
                _connection.Open();

                //Create a new table to store all user info
                ExecuteNonQuery(SqlCommands.CreateUsersTable);
            }

            if (_connection == null)
            {
                _connection = new SQLiteConnection($"Data Source={sdCardPath + ConstPaths.SqlDatabaseFilePath};Version=3;");
                _connection.Open();
            }
        }

        /// <summary>
        /// Create a new unicade cloud user
        /// </summary>
        /// <returns>True if the user was created sucuessfully </returns>
        internal static bool CreateNewUser(string username, string password, string email, string userInfo, string allowedEsrb)
        {
            //Check if an user with the existing username exists
            string command = $"SELECT * FROM users WHERE username = \"{username}\"";
            var reader = ExecuteQuery(command);
            if (reader.HasRows)
            {
                throw new ArgumentException("Username already exists");
            }

            //Validate username
            if (username == null)
            {
                throw new ArgumentException("Username cannot be null");
            }
            if (Utilties.CheckForInvalidChars(username))
            {
                throw new ArgumentException("Username contains invalid characters");
            }
            if (username.Length < ConstValues.MinUsernameLength)
            {
                throw new ArgumentException("Username must be at least 4 chars");
            }
            if (username.Length > ConstValues.MaxUsernameLength)
            {
                throw new ArgumentException($"Username cannot exceed {ConstValues.MaxUsernameLength} chars");
            }

            //Validate password
            if (password == null)
            {
                throw new ArgumentException("Password cannot be null");
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

            //Validate email
            if (email == null)
            {
                throw new ArgumentException("Email cannot be null");
            }
            if (email.Length < ConstValues.MinEmailLength)
            {
                throw new ArgumentException($"Email must be at least {ConstValues.MinEmailLength} chars");
            }
            if (Utilties.CheckForInvalidChars(email))
            {
                throw new ArgumentException("Email contains invalid characters");
            }
            if (!email.Contains("@"))
            {
                throw new ArgumentException("Email is invalid");
            }
            if (email.Length > ConstValues.MaxEmailLength)
            {
                throw new ArgumentException($"Email cannot exceed {ConstValues.MaxEmailLength} chars");
            }

            //Validate user info
            if (userInfo == null)
            {
                throw new ArgumentException("User info cannot be null");
            }
            if (Utilties.CheckForInvalidChars(userInfo))
            {
                throw new ArgumentException("User info contains invalid characters");
            }
            if (userInfo.Length > ConstValues.MaxUserInfoLength)
            {
                throw new ArgumentException($"User info cannot exceed {ConstValues.MaxUserInfoLength} chars");
            }


            //Create a new user entry in the users table
            command = $"INSERT INTO users (username,password,email,userinfo,allowedEsrb) VALUES (\"{username}\", \"{password}\", \"{email}\", \"{userInfo}\", \"{allowedEsrb}\");";
            ExecuteNonQuery(command);

            //Create a new games table for the user
            command = SqlCommands.CreateGamesTable.Replace("[Username]", username);
            ExecuteNonQuery(command);

            //Create a new consoles table for the user
            command = SqlCommands.CreateConsolesTable.Replace("[Username]", username);
            ExecuteNonQuery(command);
            return true;
        }

        /// <summary>
        /// Delete the current UniCade cloud user from the database and set the currentUsername to null
        /// </summary>
        internal static void DeleteCurrentUser()
        {
            if (_currentSqlUsername != null)
            {
                //Delete the current user from the users table
                string command = $"DELETE FROM users WHERE username = \"{_currentSqlUsername}\";";
                ExecuteNonQuery(command);

                //Drop the user's games table
                command = $"DROP TABLE games_{_currentSqlUsername}";
                ExecuteNonQuery(command);

                //Set the current user to null
                _currentSqlUsername = null;
            }
        }

        /// <summary>
        /// Login a SQL user
        /// </summary>
        /// <returns>True if the username and password were accepted</returns>
        internal static bool Login(string username, string password)
        {
            if (username == null || password == null)
            {
                return false;
            }

            string command = $"SELECT * FROM users WHERE username = \"{username}\"";
            var reader = ExecuteQuery(command);
            while (reader.Read())
            {
                //If the password is valid, set the _currentSqlUsername
                if (password.Equals(reader["password"].ToString()))
                {
                    _currentSqlUsername = username;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Log out the current SQL user
        /// </summary>
        internal static void Logout()
        {
            _currentSqlUsername = null;
            //MessageBox.Show("User logged out");
        }

        /// <summary>
        /// Upload a single game to the SQL database
        /// </summary>
        /// <param name="game">The game to upload</param>
        /// <returns></returns>
        internal static bool UploadGame(IGame game)
        {
            if (_currentSqlUsername == null || game == null)
            {
                return false;
            }

            //Convert the favorite bool value to an int value
            int fav = game.Favorite ? 1 : 0;

            //Generate and execute teh command
            string command = $"INSERT OR REPLACE INTO games_{ _currentSqlUsername} (filename, title, console, launchCount, releaseDate, publisher, developer, userScore, criticScore, players, trivia, esrbRating, esrbDescriptors, esrbSummary, description, genres, tags, otherConsoles, videoLink, gamesDbApiId, mobygamesApiId, mobygamesApiUrl, IgdbApiId, favorite)  VALUES (\"{game.FileName}\", \"{game.Title}\", \"{game.ConsoleName}\" ,{game.GetLaunchCount()}, \"{game.ReleaseDate}\", \"{game.PublisherName}\", \"{game.DeveloperName}\", \"{game.UserReviewScore}\",  \"{game.CriticReviewScore}\", \"{game.SupportedPlayerCount}\", \"{game.Trivia}\", \"{game.EsrbRating.GetStringValue()}\", \"{game.GetEsrbDescriptorsString()}\", \"{game.EsrbSummary}\", \"{game.Description}\", \"{game.Genres}\", \"{game.Tags}\", \"{game.OtherPlatforms}\", \"{game.VideoLink}\", {game.GamesdbApiId}, {game.MobygamesApiId}, \"{game.MobyGamesUrl}\", {game.IgdbApiId}, {fav});";
            ExecuteNonQuery(command);                                                                                                                                                                                                                                                                                                                                           
            return true;
        }

        /// <summary>
        /// Upload a single console to the SQL database
        /// </summary>
        /// <param name="console">The console to upload</param>
        /// <returns></returns>
        internal static bool UploadConsole(IConsole console)
        {
            if (_currentSqlUsername == null || console == null)
            {
                return false;
            }

            //Generate and execute the command
            string command = $"INSERT OR REPLACE INTO consoles_{ _currentSqlUsername} (consoleName, consoleInfo, launchParams, releaseDate, romExtension, romFolderPath, emulatorExePath, developer, cpu, ram, graphics, displayResolution, consoleRating, additionalConsoleInfo, gamesDbApiId, mobygamesApiId, IgdbApiId) VALUES (\"{console.ConsoleName}\", \"{console.ConsoleInfo}\", \"{console.LaunchParams}\" ,{console.ReleaseDate}, \"{console.RomExtension}\", \"{console.RomFolderPath}\", \"{console.EmulatorExePath}\", \"{console.Developer}\", \"{console.Cpu}\",  \"{console.Ram}\", \"{console.Graphics}\", \"{console.DisplayResolution}\", \"{console.ConsoleRating}\", \"{console.AdditionalConsoleInfo}\", \"{console.GamesdbApiId}\", \"{console.MobygamesApiId}\", \"{console.IgdbApiId}\");";
            ExecuteNonQuery(command);
            return true;
        }

        /// <summary>
        /// Download the metadata for the specified console
        /// </summary>
        /// <returns>true if the console metadata was downloaded</returns>
        internal static bool DownloadConsoleInfo(IConsole console)
        {
            if (_currentSqlUsername == null || console == null)
            {
                return false;
            }

            //Generate and execute the command
            string command = $"SELECT * FROM consoles_{_currentSqlUsername} WHERE consoleName = \"{console.ConsoleName}\"";
            var reader = ExecuteQuery(command);

            //Return false if the console is not found
            if (!reader.HasRows)
            {
                return false;
            }

            //Populate the console fields
            reader.Read();
            console.ReleaseDate = reader["releaseDate"].ToString();
            console.ConsoleInfo = reader["consoleInfo"].ToString();
            console.EmulatorExePath = reader["emulatorExePath"].ToString();
            console.LaunchParams = reader["launchParams"].ToString();
            console.RomExtension = reader["romExtension"].ToString();
            console.RomFolderPath = reader["romFolderPath"].ToString();
            console.Developer = reader["developer"].ToString();
            console.Cpu = reader["cpu"].ToString();
            console.Ram = reader["ram"].ToString();
            console.Graphics = reader["graphics"].ToString();
            console.DisplayResolution = reader["displayResolution"].ToString();
            console.ConsoleRating = reader["consoleRating"].ToString();
            console.AdditionalConsoleInfo = reader["additionalConsoleInfo"].ToString();
            console.GamesdbApiId = int.Parse(reader["gamesDbApiId"].ToString());
            console.MobygamesApiId = int.Parse(reader["mobygamesApiId"].ToString());
            console.IgdbApiId = int.Parse(reader["IgdbApiId"].ToString());

            return true;
        }

        /// <summary>
        /// Download metadata for all consoles 
        /// </summary>
        /// <returns></returns>
        internal static bool DownloadAllConsoles()
        {
            if (_currentSqlUsername == null)
            {
                return false;
            }

            foreach (string consoleName in Database.GetConsoleList())
            {
                DownloadConsoleInfo(Database.GetConsole(consoleName));
            }

            return true;

        }

        /// <summary>
        /// Upload metadata for all consoles 
        /// </summary>
        /// <returns></returns>
        internal static bool UploadAllConsoles()
        {
            if (_currentSqlUsername == null)
            {
                return false;
            }

            foreach (string consoleName in Database.GetConsoleList())
            {
                UploadConsole(Database.GetConsole(consoleName));
            }

            return true;

        }

        /// <summary>
        /// Delete all games for the current user
        /// </summary>
        /// <returns>true if the user's games were deleted</returns>
        internal static bool DeleteAllUserGames()
        {
            if (_currentSqlUsername == null)
            {
                return false;
            }

            string command = $"DELETE FROM games_{_currentSqlUsername}";
            ExecuteNonQuery(command);
            return true;
        }


        /// <summary>
        /// Delete all games for the current user
        /// </summary>
        /// <returns>true if the user's games were deleted</returns>
        internal static bool DeleteAllUserConsoles()
        {
            if (_currentSqlUsername == null)
            {
                return false;
            }

            string command = $"DELETE FROM consoles_{_currentSqlUsername}";
            ExecuteNonQuery(command);
            return true;
        }

        /// <summary>
        /// Download metadata for a single game
        /// </summary>
        /// <param name="game">The game to update</param>
        /// <returns>True if the game was found and metadata updated</returns>
        internal static bool DownloadGameInfo(IGame game)
        {
            if (_currentSqlUsername == null || game == null)
            {
                return false;
            }

            string command = $"SELECT * FROM games_{_currentSqlUsername} WHERE filename = \"{game.FileName}\" AND console = \"{game.ConsoleName}\"";

            var reader = ExecuteQuery(command);

            //Return false if the game is not found
            if (!reader.HasRows)
            {
                return false;
            }

            //Populate the game fields
            reader.Read();
            game.SetLaunchCount(int.Parse(reader["launchCount"].ToString()));
            game.ReleaseDate = reader["releaseDate"].ToString();
            game.PublisherName = reader["publisher"].ToString();
            game.DeveloperName = reader["developer"].ToString();
            game.UserReviewScore = reader["userScore"].ToString();
            game.CriticReviewScore = reader["criticScore"].ToString();
            game.SupportedPlayerCount = reader["players"].ToString();
            game.Trivia = reader["trivia"].ToString();
            game.EsrbRating = Utilties.ParseEsrbRating(reader["esrbRating"].ToString());
            game.AddEsrbDescriptorsFromString(reader["esrbDescriptors"].ToString());
            game.EsrbSummary = reader["esrbSummary"].ToString();
            game.Description = reader["description"].ToString();
            game.Genres = reader["genres"].ToString();
            game.Tags = reader["tags"].ToString();
            game.OtherPlatforms = reader["otherConsoles"].ToString();
            game.VideoLink = reader["videoLink"].ToString();
            game.GamesdbApiId = int.Parse(reader["gamesDbApiId"].ToString());
            game.MobygamesApiId = int.Parse(reader["mobygamesApiId"].ToString());
            game.MobyGamesUrl = reader["mobygamesApiUrl"].ToString();
            game.IgdbApiId = int.Parse(reader["IgdbApiId"].ToString());
            game.Favorite = int.Parse(reader["favorite"].ToString()) == 1;
            return true;
        }

        /// <summary>
        /// Returns the currently logged in username
        /// </summary>
        /// <returns></returns>
        internal static string GetCurrentUsername()
        {
            return _currentSqlUsername;
        }

        /// <summary>
        /// Upload all game metadata for games within the specified the console
        /// </summary>
        /// <param name="console">The console to upload</param>
        /// <returns>True if the games were sucuessfully uploaded</returns>
        internal static bool UploadAllGamesForConsole(IConsole console)
        {
            if (_currentSqlUsername == null || console == null)
            {
                return false;
            }
            StringBuilder command = new StringBuilder();

            //Append all insert commands into a single string for efficiency
            foreach (string gameName in console.GetGameList())
            {
                Game game = (Game)console.GetGame(gameName);
                int fav = game.Favorite ? 1 : 0;
                command.Append(
                    $"INSERT OR REPLACE INTO games_{ _currentSqlUsername} (filename, title, console, launchCount, releaseDate, publisher, developer, userScore, criticScore, players, trivia, esrbRating, esrbDescriptors, esrbSummary, description, genres, tags, otherConsoles, videoLink, gamesDbApiId, mobygamesApiId, mobygamesApiUrl, IgdbApiId, favorite)  VALUES (\"{game.FileName}\", \"{game.Title}\", \"{game.ConsoleName}\" ,{game.GetLaunchCount()}, \"{game.ReleaseDate}\", \"{game.PublisherName}\", \"{game.DeveloperName}\", \"{game.UserReviewScore}\",  \"{game.CriticReviewScore}\", \"{game.SupportedPlayerCount}\", \"{game.Trivia}\", \"{game.EsrbRating.GetStringValue()}\", \"{game.GetEsrbDescriptorsString()}\", \"{game.EsrbSummary}\", \"{game.Description}\", \"{game.Genres}\", \"{game.Tags}\", \"{game.OtherPlatforms}\", \"{game.VideoLink}\", \"{game.GamesdbApiId}\", \"{game.MobygamesApiId}\", \"{game.MobyGamesUrl}\",\"{game.IgdbApiId}\",  {fav});");
            }
            ExecuteNonQuery(command.ToString());

            return true;
        }

        /// <summary>
        /// Upload metadata for all games across all consoles
        /// </summary>
        /// <returns>true if the operation was successful</returns>
        internal static bool UploadAllGames()
        {
            if (_currentSqlUsername == null)
            {
                return false;
            }

            foreach (string consoleName in Database.GetConsoleList())
            {
                UploadAllGamesForConsole(Database.GetConsole(consoleName));
            }
            return true;
        }

        /// <summary>
        /// Download metadata for all games belonging to the specified console
        /// </summary>
        /// <param name="console">The console to fetch data for</param>
        /// <returns>true if the operation was successful</returns>
        internal static bool DownloadAllGamesForConsole(IConsole console)
        {
            if (_currentSqlUsername == null || console == null)
            {
                return false;
            }

            //Generate and execute the SQL command
            string command = $"SELECT * FROM games_{_currentSqlUsername} WHERE console = \"{console.ConsoleName}\";";
            var reader = ExecuteQuery(command);

            while (reader.Read())
            {
                //Attempt to fetch a game object by its title
                Game game = (Game) console.GetGame(reader["title"].ToString());

                //If a game was found, update its metadata
                if (game != null)
                {
                    game.SetLaunchCount(int.Parse(reader["launchCount"].ToString()));
                    game.ReleaseDate = reader["releaseDate"].ToString();
                    game.PublisherName = reader["publisher"].ToString();
                    game.DeveloperName = reader["developer"].ToString();
                    game.UserReviewScore = reader["userScore"].ToString();
                    game.CriticReviewScore = reader["criticScore"].ToString();
                    game.SupportedPlayerCount = reader["players"].ToString();
                    game.Trivia = reader["trivia"].ToString();
                    game.EsrbRating = Utilties.ParseEsrbRating(reader["esrbRating"].ToString());
                    game.AddEsrbDescriptorsFromString(reader["esrbDescriptors"].ToString());
                    game.EsrbSummary = reader["esrbSummary"].ToString();
                    game.Description = reader["description"].ToString();
                    game.Genres = reader["genres"].ToString();
                    game.Tags = reader["tags"].ToString();
                    game.Favorite = int.Parse(reader["favorite"].ToString()) == 1;
                }
            }

            return true;
        }

        /// <summary>
        /// Download metadata for all games across all consoles
        /// </summary>
        /// <returns></returns>
        internal static bool DownloadAllGames()
        {
            if (_currentSqlUsername == null)
            {
                return false;
            }

            //Download metadata for all games across all consoles
            Database.GetConsoleList().ForEach(c => DownloadAllGamesForConsole(Database.GetConsole(c)));
            return true;
        }

        /// <summary>
        /// Close the current SQL connection
        /// </summary>
        internal static void Disconnect()
        {
            _connection?.Close();
        }


        #endregion

        #region  Helper Methods

        /// <summary>
        /// Execute a non query SQL command
        /// </summary>
        /// <param name="input">The command string to execute</param>
        /// <returns>The int status of the operation</returns>
        // ReSharper disable once UnusedMethodReturnValue.Local
        private static int ExecuteNonQuery(string input)
        {
            if (input != null)
            {
                Connect();
                SQLiteCommand command = new SQLiteCommand(input, _connection);
                return command.ExecuteNonQuery();
            }
            return -1;
        }

        /// <summary>
        /// Execute an SQL query return a SQLiteDataReader object
        /// </summary>
        /// <param name="query">The command string to execute</param>
        /// <returns>A SQLiteDataReader object representing the response</returns>
        private static SQLiteDataReader ExecuteQuery(string query)
        {
            if (query != null)
            {
                Connect();
                SQLiteCommand command = new SQLiteCommand(query, _connection);
                return command.ExecuteReader();
            }
            return null;
        }
        
        #endregion
    }
}
