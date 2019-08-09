using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using Android.App;
using Android.Widget;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Interfaces;
using UniCadeAndroid.Objects;
using Console = UniCadeAndroid.Objects.Console;


namespace UniCadeAndroid.Backend
{
    internal class FileOps
    {
        #region Properties

        /// <summary>
        /// True if the current game is a steam URL
        /// </summary>
        public static bool IsSteamGame;

        #endregion

        #region Public Methods

        /// <summary>
        /// Load the database file from the specified path
        /// </summary>
        /// <returns>false if the database file does not exist</returns>
        public static bool LoadDatabase(string path = ConstPaths.DatabaseFilePath)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = sdCardPath + path;

            if (!File.Exists(filePath))
            {
                return false;
            }

            List<Console> consoleList;

            DataContractSerializer s = new DataContractSerializer(typeof(List<Console>));
            using (FileStream fs = File.Open(filePath, FileMode.Open))
            {
               consoleList = (List<Console>)s.ReadObject(fs);
            }

            consoleList.ForEach(c => Database.AddConsole(c));

            return true;
        }

        /// <summary>
        /// Save the database to the specified path. Delete any preexisting database files
        /// </summary>
        public static bool SaveDatabase(string path = ConstPaths.DatabaseFilePath)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = sdCardPath + path;

            var consoleList = Database.GetConsoleList().Select(consoleName => (Console)Database.GetConsole(consoleName)).ToList();
            
            var xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            DataContractSerializer s = new DataContractSerializer(typeof(List<Console>));
            using (var xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                s.WriteObject(xmlWriter, consoleList);
            }
            
            return true;
        }


        /// <summary>
        /// Load preferences from the specified file path
        /// </summary>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static bool LoadPreferences(string path = ConstPaths.PreferencesFilePath)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = sdCardPath + path;

            //First check if the database file exists
            if (!File.Exists(filePath))
            {
                return false;
            }

            CurrentSettings currentSettings;
            
            DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(CurrentSettings));
            using (FileStream fileStream = File.Open(filePath, FileMode.Open))
            {
                currentSettings = (CurrentSettings)dataContractSerializer.ReadObject(fileStream);
            }

            Preferences.ShowSplashScreen = currentSettings.ShowSplashScreen;
            Preferences.UseModernEsrbLogos = currentSettings.UseModernEsrbLogos;
            Preferences.UserLicenseKey = currentSettings.UserLicenseKey;
            Preferences.UserLicenseName = currentSettings.UserLicenseName;
            Preferences.PasswordProtection = currentSettings.PasswordProtection;
            currentSettings.UserList.ForEach(u => Database.AddUser(u));
            
            return true;

        }

        /// <summary>
        /// Save preferences file to the specified path
        /// </summary>
        public static bool SavePreferences(string path = ConstPaths.PreferencesFilePath)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
            var filePath = sdCardPath + path;

            var currentSettings = new CurrentSettings
            {
                ShowSplashScreen = Preferences.ShowSplashScreen,
                UseModernEsrbLogos = Preferences.UseModernEsrbLogos,
                UserLicenseKey = Preferences.UserLicenseKey,
                UserLicenseName = Preferences.UserLicenseName,
                PasswordProtection = Preferences.PasswordProtection
            };

            foreach (string userName in Database.GetUserList())
            {
                currentSettings.UserList.Add((User)Database.GetUser(userName));
            }


            var xmlWriterSettings = new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = "\t"
            };

            DataContractSerializer s = new DataContractSerializer(typeof(CurrentSettings));
            using (var xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings))
            {
                s.WriteObject(xmlWriter, currentSettings);
            }
            return true;
        }

        /// <summary>
        /// Scan the target directory for new ROM files and add them to the active database
        /// </summary>
        public static void ScanAllConsoles()
        {
            foreach (string consoleName in Database.GetConsoleList())
            {
                ScanSingleConsole(Database.GetConsole(consoleName));
            }
        }

        /// <summary>
        /// Scan the specied folder for games within a single console
        /// Note: This is a helper function called multiple times by the primary scan function
        /// </summary>
        public static bool ScanSingleConsole(IConsole console)
        {
            if (console == null)
            {
                return false;
            }
            //Attempt to open the directory and fetch file entries
            string[] fileEntries = null;
            try
            {
                fileEntries = Directory.GetFiles(console.RomFolderPath);
            }
            catch
            {
                ShowNotification("Directory not found");
            }

            //Add games to the current console object
            if (fileEntries != null)
            {
                foreach (string fileName in fileEntries)
                {
                    string gameTitle = fileName.Split('.')[0];
                    if (gameTitle != null)
                    {
                        if (console.GetGame(gameTitle) == null)
                        {
                            try
                            {
                                console.AddGame(new Game(Path.GetFileName(fileName), console.ConsoleName));
                            }
                            catch (ArgumentException exception)
                            {
                                ShowNotification("Game cannot be added: " + exception.Message);
                            }
                        }
                    }
                }
            }

            //Delete nonexistent games
            foreach (string gameTitle in console.GetGameList())
            {
                if (fileEntries != null)
                {
                    if (!fileEntries.Contains(gameTitle))
                    {
                        console.RemoveGame(gameTitle);
                    }
                }
            }
            return true;
        }

  
        /// <summary>
        /// Restore the default consoles. These changes will take effect Immediately. 
        /// </summary>
        public static void RestoreDefaultConsoles()
        {
            Database.AddConsole(new Console("Sega Genesis", @"C:\UniCade\Emulators\Fusion\Fusion.exe", @"C:\UniCade\ROMS\Sega Genesis\", ".bin*.iso*.gen*.32x", "consoleInfo", "%file -gen -auto -fullscreen", "1990"));
            Database.AddConsole(new Console("Nintendo Wii", @"C:\UniCade\Emulators\Dolphin\dolphin.exe", @"C:\UniCade\ROMS\Nintendo Wii\", ".gcz*.iso", "consoleInfo", "/b /e %file", "2006"));
            Database.AddConsole(new Console("Nintendo DS", @"C:\UniCade\Emulators\NDS\DeSmuME.exe", @"C:\UniCade\ROMS\Nintendo DS\", ".nds", "consoleInfo", "%file", "2005"));
            Database.AddConsole(new Console("Nintendo GBC", @"C:\UniCade\Emulators\GBA\VisualBoyAdvance.exe", @"C:\UniCade\ROMS\Nintendo GBC\", ".gbc", "consoleInfo", "%file", "1998"));
            Database.AddConsole(new Console("MAME", @"C:\UniCade\Emulators\MAME\mame.bat", @"C:\UniCade\Emulators\MAME\roms\", ".zip", "consoleInfo", "", "1980")); //%file -skip_gameinfo -nowindow
            Database.AddConsole(new Console("PC", @"C:\Windows\explorer.exe", @"C:\UniCade\ROMS\PC\", ".lnk*.url", "consoleInfo", "%file", "1980"));
            Database.AddConsole(new Console("Nintendo GBA", @"C:\UniCade\Emulators\GBA\VisualBoyAdvance.exe", @"C:\UniCade\ROMS\Nintendo GBA\", ".gba", "consoleInfo", "%file", "2001"));
            Database.AddConsole(new Console("Nintendo Gamecube", @"C:\UniCade\Emulators\Dolphin\dolphin.exe", @"C:\UniCade\ROMS\Nintendo Gamecube\", ".iso*.gcz", "consoleInfo", "/b /e %file", "2001"));
            Database.AddConsole(new Console("NES", @"C:\UniCade\Emulators\NES\Jnes.exe", @"C:\UniCade\ROMS\NES\", ".nes", "consoleInfo", "%file", "1983"));
            Database.AddConsole(new Console("SNES", @"C:\UniCade\Emulators\ZSNES\zsnesw.exe", @"C:\UniCade\ROMS\SNES\", ".smc", "consoleInfo", "%file", "1990"));
            Database.AddConsole(new Console("Nintendo N64", @"C:\UniCade\Emulators\Project64\Project64.exe", @"C:\UniCade\ROMS\Nintendo N64\", ".n64*.z64", "consoleInfo", "%file", "1996"));
            Database.AddConsole(new Console("Sony Playstation", @"C:\UniCade\Emulators\ePSXe\ePSXe.exe", @"C:\UniCade\ROMS\Sony Playstation\", ".iso*.bin*.img", "consoleInfo", "-nogui -loadbin %file", "1994"));
            Database.AddConsole(new Console("Sony Playstation 2", @"C:\UniCade\Emulators\PCSX2\pcsx2.exe", @"C:\UniCade\ROMS\Sony Playstation 2\", ".iso*.bin*.img", "consoleInfo", "%file", "2000"));
            Database.AddConsole(new Console("Atari 2600", @"C:\UniCade\Emulators\Stella\Stella.exe", @"C:\UniCade\ROMS\Atari 2600\", ".iso*.bin*.img", "consoleInfo", "file", "1977"));
            Database.AddConsole(new Console("Sega Dreamcast", @"C:\UniCade\Emulators\NullDC\nullDC_Win32_Release-NoTrace.exe", @"C:\UniCade\ROMS\Sega Dreamcast\", ".iso*.bin*.img", "consoleInfo", "-config ImageReader:defaultImage=%file", "1998"));
            Database.AddConsole(new Console("Sony PSP", @"C:\UniCade\Emulators\PPSSPP\PPSSPPWindows64.exe", @"C:\UniCade\ROMS\Sony PSP\", ".iso*.cso", "consoleInfo", "%file", "2005"));
            Database.AddConsole(new Console("Nintendo Wii U", @"C:\UniCade\Emulators\WiiU\cemu.exe", @"C:\UniCade\ROMS\Atari 2600\", ".iso*.bin*.img", "consoleInfo", "file", "2012"));
            Database.AddConsole(new Console("Nintendo 3DS", @"C:\UniCade\Emulators\PS3\3ds.exe", @"C:\UniCade\ROMS\Nintendo 3DS\", ".iso", "consoleInfo", "%file", "2014"));
        }

        /// <summary>
        /// Restore default preferences. These updated preferences will take effect immediatly.
        /// NOTE: These changes are not automatically saved to the database file.
        /// </summary>
        public static void RestoreDefaultPreferences()
        {
            Database.RestoreDefaultUser();
            Preferences.ShowSplashScreen = false;
          }

        /// <summary>
        /// Preforms the initial file system operations when the program is launched
        /// </summary>
        public static bool StartupScan()
        {

            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;

            //If preferences file does not exist, load default preference values and save a new file
            if (!LoadPreferences())
            {
                RestoreDefaultPreferences();
                SavePreferences();
                ShowNotification("Preference file not found.\n Loading defaults...");
            }

            //Verify the current user license and set flag
            Preferences.IsLicenseValid = CryptoEngine.ValidateLicense(Preferences.UserLicenseName, Preferences.UserLicenseKey);

            //If the database file does not exist in the specified location, load default values and rescan rom directories
            if (!LoadDatabase())
            {
                RestoreDefaultConsoles();
                ScanAllConsoles();

                try
                {
                    SaveDatabase();
                }
                catch
                {
                    ShowNotification("Error saving database");
                    return false;
                }
            }
            //Generate folders within the Console directory
            RegenerateMediaFolders();

            return true;
        }

        public static void DeleteAllLocalMedia(){
			var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			foreach (string consoleName in Database.GetConsoleList())
			{
				string consoleDirectory = sdCardPath + ConstPaths.GameImagesPath + consoleName;
				if (!Directory.Exists(consoleDirectory))
				{
                    Directory.Delete(consoleDirectory, true);
				}
			}
            RegenerateMediaFolders();
        }

        #endregion

        #region Helper Methods

        private static void RegenerateMediaFolders(){
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.Path;
			foreach (string consoleName in Database.GetConsoleList())
			{
				string consoleDirectory = sdCardPath + ConstPaths.GameImagesPath + consoleName;
				if (!Directory.Exists(consoleDirectory))
				{
					Directory.CreateDirectory(consoleDirectory);
				}
			}
        }

        /// <summary>
        /// Display a timed popup notification in the lower right corner of the interface
        /// </summary>
        private static void ShowNotification(string body)
        {
            Toast.MakeText(Application.Context, body, ToastLength.Long).Show();
        }

        #endregion
    }
}

