using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Interfaces;

namespace UniCadeAndroid.Objects
{
    [DataContract]
    internal class Console : IConsole
    {
        #region Properties

        /// <summary>
        /// The common display name for the console
        /// </summary>
        public string ConsoleName
        {
            get => _consoleName;
            set
            {
                if (_consoleName != null)
                {
                    if (value == null)
                    {
                        throw new ArgumentException("Console name cannot be null");
                    }
                    if (value.Length == 0)
                    {
                        throw new ArgumentException("Console name cannot be empty");
                    }
                    if (Utilties.CheckForInvalidChars(value))
                    {
                        throw new ArgumentException("Console name contains invalid characters");
                    }
                    if (value.Length > ConstValues.MaxConsoleNameLength)
                    {
                        throw new ArgumentException(
                            $"Console name cannot exceed {ConstValues.MaxConsoleNameLength} chars");
                    }
                }
                _consoleName = value;
            }
        }

        /// <summary>
        /// The original release date for the console
        /// </summary>
        public string ReleaseDate
        {
            get => _releaseDate;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Release date cannot be null");
                }
                if (!Utilties.IsAllDigits(value))
                {
                    throw new ArgumentException("Release date must be only digits");
                }
                if (value.Length != 4)
                {
                    throw new ArgumentException("Release date must be four digits");
                }
                _releaseDate = value;
            }
        }

        /// <summary>
        /// Full path for the emulators folder
        /// </summary>
        public string EmulatorExePath
        {
            get => _emulatorExePath;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Emulator path cannot be null");
                }
                if (value.Length < 4)
                {
                    throw new ArgumentException("Emulator path too short");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Emulator path contains invalid characters");
                }
                if (!value.Contains(":\\"))
                {
                    throw new ArgumentException("Emulator path invalid");
                }
                if (value.Length > ConstValues.MaxPathLength)
                {
                    throw new ArgumentException($"Emulator path cannot exceed {ConstValues.MaxPathLength} chars");
                }
                _emulatorExePath = value;
            }
        }

        /// <summary>
        /// The full path to the rom directory for the current console
        /// </summary>
        public string RomFolderPath
        {
            get => _romFolderPath;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("ROM path cannot be null");
                }
                if (value.Length < 4)
                {
                    throw new ArgumentException("ROM path too short");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("ROM path contains invalid characters");
                }
                if (!value.Contains(":\\"))
                {
                    throw new ArgumentException("ROM path invalid");
                }
                if (value.Length > ConstValues.MaxPathLength)
                {
                    throw new ArgumentException($"ROM path cannot exceed {ConstValues.MaxPathLength} chars");
                }
                _romFolderPath = value;
            }
        }

        /// <summary>
        /// The extensions for the current console
        /// </summary>
        public string RomExtension
        {
            get => _romExtensions;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("ROM file extension cannnot be null");
                }
                if (value.Length == 0)
                {
                    throw new ArgumentException("ROM file extension cannnot be empty");
                }
                if (Utilties.CheckForInvalidSplitChars(value))
                {
                    throw new ArgumentException("File extension contains invalid characters");
                }
                if (!value.Contains("."))
                {
                    throw new ArgumentException("File extension invalid");
                }
                if (value.Length > ConstValues.MaxFileExtLength)
                {
                    throw new ArgumentException(
                        $"ROM extension length cannot exceed {ConstValues.MaxFileExtLength} chars");
                }
                _romExtensions = value;
            }
        }

        /// <summary>
        /// Basic console description and info
        /// </summary>
        public string ConsoleInfo
        {
            get => _consoleInfo;
            set
            { 
                if (value == null)
                {
                    throw new ArgumentException("Console info cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Console info contains invalid characters");
                }
                if (value.Length > ConstValues.MaxConsoleInfoLength)
                {
                    throw new ArgumentException(
                        $"ROM extension length cannot exceed {ConstValues.MaxConsoleInfoLength} chars");
                }
                _consoleInfo = value;
            }
        }

        /// <summary>
        /// The launch params for the current emulator
        /// </summary>
        public string LaunchParams
        {
            get => _launchParams;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Launch params cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Launch params contain invalid characters");
                }
                if (value.Length > ConstValues.MaxLaunchParamsLength)
                {
                    throw new ArgumentException(
                        $"Launch params length cannot exceed {ConstValues.MaxLaunchParamsLength} chars");
                }
                _launchParams = value;
            }
        }

        /// <summary>
        /// The developer of the console
        /// </summary>
        public string Developer
        {
            get => _developer;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Console developer cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Console developer contains invalid characters");
                }
                if (value.Length > ConstValues.MaxPublisherDeveloperLength)
                {
                    throw new ArgumentException(
                        $"Console developelength cannot exceed {ConstValues.MaxPublisherDeveloperLength} chars");
                }
                _developer = value;
            }
        }

        /// <summary>
        /// The CPU of the console
        /// </summary>
        public string Cpu
        {
            get => _cpu;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("CPU description cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("CPU description contains invalid characters");
                }
                if (value.Length > ConstValues.MaxConsoleCpuStringLength)
                {
                    throw new ArgumentException(
                        $"CPU description length cannot exceed {ConstValues.MaxConsoleCpuStringLength} chars");
                }
                _cpu = value;
            }
        }

        /// <summary>
        /// The amount and type of RAM for the console
        /// </summary>
        public string Ram
        {
            get => _ram;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("RAM description cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("RAM description contains invalid characters");
                }
                if (value.Length > ConstValues.MaxConsoleRamStringLength)
                {
                    throw new ArgumentException(
                        $"RAM description length cannot exceed {ConstValues.MaxConsoleRamStringLength} chars");
                }
                _ram = value;
            }
        }

        /// <summary>
        /// The graphics card for the console
        /// </summary>
        public string Graphics
        {
            get => _graphics;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Graphics description cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Graphics description contains invalid characters");
                }
                if (value.Length > ConstValues.MaxConsoleGraphicsStringLength)
                {
                    throw new ArgumentException(
                        $"Graphics description length cannot exceed {ConstValues.MaxConsoleGraphicsStringLength} chars");
                }
                _graphics = value;
            }
        }

        /// <summary>
        /// The console's native display resolution
        /// </summary>
        public string DisplayResolution
        {
            get => _displayResolution;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Display Resolution cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Display Resolution contains invalid characters");
                }
                if (value.Length > ConstValues.MaxConsoleDisplayResolutionLength)
                {
                    throw new ArgumentException(
                        $"Display Resolution length cannot exceed {ConstValues.MaxConsoleDisplayResolutionLength} chars");
                }
                _displayResolution = value;
            }
        }

        /// <summary>
        /// The average user reviews for the console
        /// </summary>
        public string ConsoleRating
        {
            get => _consoleRating;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Console rating cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Console rating contains invalid characters");
                }
                if (value.Length > ConstValues.MaxConsoleRatingLength)
                {
                    throw new ArgumentException(
                        $"Console rating length cannot exceed {ConstValues.MaxConsoleRatingLength} chars");
                }
                _consoleRating = value;
            }
        }

        /// <summary>
        ///  Additional info for the console
        /// </summary>
        public string AdditionalConsoleInfo
        {
            get => _additionalConsoleInfo;
            set
            {
                if (value == null)
                {
                    throw new ArgumentException("Additional Console Info cannnot be null");
                }
                if (Utilties.CheckForInvalidChars(value))
                {
                    throw new ArgumentException("Additional Console Info contains invalid characters");
                }
                if (value.Length > ConstValues.MaxAdditionalConsoleInfoLength)
                {
                    throw new ArgumentException(
                        $"Additional Console Info length cannot exceed {ConstValues.MaxAdditionalConsoleInfoLength} chars");
                }
                _additionalConsoleInfo = value;
            }
        }

        #endregion

        #region  API IDs

        /// <summary>
        /// The unique game identifier for the GamesDB API
        /// </summary>
        [DataMember]
        public int GamesdbApiId { get; set; }

        /// <summary>
        /// The unique game identifier for the MobyGames API
        /// </summary>
        [DataMember]
        public int MobygamesApiId { get; set; }

        /// <summary>
        /// The unique game identifier for the IGDB API
        /// </summary>
        [DataMember]
        public int IgdbApiId { get; set; }

        #endregion

        #region  Private Instance Fields

        /// <summary>
        /// The common display name for the console
        /// </summary>
        [DataMember]
        private string _consoleName;

        /// <summary>
        /// The original release date for the console
        /// </summary>
        [DataMember]
        private string _releaseDate;

        /// <summary>
        /// Full path for the emulators folder
        /// </summary>
        [DataMember]
        private string _emulatorExePath;

        /// <summary>
        /// The full path to the rom directory for the current console
        /// </summary>
        [DataMember]
        private string _romFolderPath;

        /// <summary>
        /// The extensions for the current console
        /// </summary>
        [DataMember]
        private string _romExtensions;

        /// <summary>
        /// Basic console description and info
        /// </summary>
        [DataMember]
        private string _consoleInfo;

        /// <summary>
        /// The launch params for the current emulator
        /// </summary>
        [DataMember]
        private string _launchParams;

        /// <summary>
        /// The developer of the console
        /// </summary>
        [DataMember]
        private string _developer;

        /// <summary>
        /// The CPU of the console
        /// </summary>
        [DataMember]
        private string _cpu;

        /// <summary>
        /// The amount and type of RAM for the console
        /// </summary>
        [DataMember]
        private string _ram;

        /// <summary>
        /// The graphics card for the console
        /// </summary>
        [DataMember]
        private string _graphics;

        /// <summary>
        ///  The console's native display resolution
        /// </summary>
        [DataMember]
        private string _displayResolution;

        /// <summary>
        /// The average user reviews for the console
        /// </summary>
        [DataMember]
        private string _consoleRating;

        /// <summary>
        /// Additional info for the console
        /// </summary>
        [DataMember]
        private string _additionalConsoleInfo;

        /// <summary>
        /// A list of game objects for the current console instance
        /// </summary>
        [DataMember]
        private List<Game> _gameList;

        #endregion

        #region Constructors 

        /// <summary>
        /// Basic constructor for a new game console
        /// </summary>
        public Console(string consoleName)
        {
            ConsoleName = consoleName;
            _gameList = new List<Game>();
        }

        /// <summary>
        /// Full constructor for creating a new game console
        /// </summary>
        /// <param name="name"></param>
        /// <param name="emuExePath"></param>
        /// <param name="romFolderPath"></param>
        /// <param name="romExt"></param>
        /// <param name="consoleInfo"></param>
        /// <param name="launchParam"></param>
        /// <param name="releaseDate"></param>
        public Console(string name, string emuExePath, string romFolderPath, string romExt, string consoleInfo, string launchParam, string releaseDate)
        {
            ConsoleName = name;
            EmulatorExePath = emuExePath;
            RomFolderPath = romFolderPath;
            RomExtension = romExt;
            ConsoleInfo = consoleInfo;
            LaunchParams = launchParam;
            ReleaseDate = releaseDate;
            _gameList = new List<Game>();
        }

        #endregion 

        #region Public Methods

        /// <summary>
        /// Add a new game to the current console
        /// </summary>
        /// <param name="game"></param>
        /// <returns>'true' if the game was sucuessfully added</returns>
        public bool AddGame(IGame game)
        {
            //If the game console does not match the current console, return false
            if (!game.ConsoleName.Equals(ConsoleName))
            {
                return false;
            }

            //Verify that the game count does not exceed the max value
            if (_gameList.Count >= ConstValues.MaxGameCount)
            {
                return false;
            }

            //If a game with an identical title (or filename) name already exists, return false
            if (_gameList.Find(e => e.Title.Equals(game.Title)) != null)
            {
                return false;
            }

            //If all conditions are valid, add the game and increment the game count for both the console and database 
            _gameList.Add((Game) game);
            return true;
        }

        /// <summary>
        /// Removes a game with the specified title from the Console
        /// Returns false if the fame title does not exist
        /// </summary>
        /// <param name="gameTitle"></param>
        /// <returns></returns>
        public bool RemoveGame(string gameTitle)
        {
            //Attempt to fetch the console from the current list
            Game game = _gameList.Find(e => e.ConsoleName.Equals(gameTitle));

            if (game != null)
            {
                _gameList.Remove(game);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Return the IGame object with the specified title
        /// </summary>
        /// <param name="gameTitle">The title of the game to fetch</param>
        /// <returns>IGame object with the matching title</returns>
        public IGame GetGame(string gameTitle)
        {
            if (gameTitle != null)
            {
                return _gameList.Find(c => c.Title.Equals(gameTitle));
            }
            return null;
        }

        /// <summary>
        /// Return a string list of all game titles
        /// </summary>
        /// <returns></returns>
        public List<string> GetGameList()
        {
            return _gameList.Select(g => g.Title).ToList();
        }

        /// <summary>
        /// Return a string list of all game titles that are set as a favorite
        /// </summary>
        /// <returns> A list of favorite game titles</returns>
        public List<string> GetFavoriteGameList()
        {
            return (from game in _gameList where game.Favorite select game.Title).ToList();
        }

        /// <summary>
        /// Return the current number of games in the console
        /// </summary>
        /// <returns>the current game count</returns>
        public int GetGameCount()
        {
            return _gameList?.Count ?? 0;
        }

        #endregion
    }
}