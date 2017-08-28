namespace UniCadeAndroid.Constants
{
    internal class TextFiles
    {
        #region Raw Text 

        public static string Instructions = @"GUI Operation
o	Use the left and right arrow keys to select your console
o	Press the “I” key to display Console Info. Press I again or escape to close the info window. 
o	Press the Enter key to select the console.
o	Use the Up and Down arrow keys to navigate the game selection interface
o	Press the “I” key to display game Info. Press I again or escape to close the info window. 

o	Press Enter to launch the selected game

o	Press the F4 key while in game to end the game and return to the game selection menu

o	Press either the Escape, Delete or Backspace keys to return to the console selection menu

o	Press Alt + P anywhere in the GUI to launch the Interface Settings Window. 

o	Press Alt + C to close the GUI and return to the command line interface


-	Games (Adding, Removing or Modifying) 
o	To add a new game (ROM). Simply drag the ROM file to the corresponding console folder in the ROM subdirectory.
o	To modify game info, open the preference window, navigate to the Games tab, modify the desired info and then click “Save to database”.
o	To Delete a game, you must delete the game from the ROM folder and then click the rescan button in the settings window
-	Console/Emulator Settings
o	To add a new emulator, open the settings window, navigate to the Emulators tab and click “Add new Emulator”.
o	To delete an emulator, navigate to the emulators tab and click the delete button when the desired console is highlighted.
o	To modify emulator settings or info, modify the fields under the emulator tab and then click “Save Info” before closing.
-	Users
o	To add a new user, navigate to the users tab, click “New User”, fill in the blank fields and click save.
o	To delete a user, navigate to the users tab, select the user and click delete.
o	To restrict specified ESRB ratings for a single user, navigate to the Users tab, select the desired rating limit in the dropdown ESRB menu and click save.
-	PayPerPlay
o	To enable PayPerPlay, navigate to the Global Settings Tab and check the box “Enable PayPerPlay”
o	Then either enter a playtime value or check the per launch box
o	You must also enter a value under “# Coins”
-	Web Scraping
o	To customize web scraping settings, open the settings window, navigate to the Web Options tab and select the desired scrapers/info to scrape.
o	 To rescrape info for a single game, navigate to the Games tab and click “Rescrape Info” when the game is selected in the side menu.
o	To rescrape metadata for all games and consoles, navigate to the Emulators tab and click “Force Global Metadata Rescrape” 
";

        public static string ImportantNotes = @"-	Preinstalled Consoles: Game Boy Advance, NES, SNES, Nintendo 64, PlayStation 1, PlayStation 2, Atari 2600, Dreamcast, PlayStation Portable, Sega Genesis, Nintendo Wii, NDS, GBC and PC Game Support
-	The default working directory for the program is “C:\UniCade” (This directory must have access privileges by the current user)
-	The UniCade directory includes 7 items (Database.txt, Preferences.txt as well as the directories: Media, ROMS, Frontend, Emulators and MAME)
-	Ensure that the structure of the Emulators folder and the media path matches the path in the interface settings exactly. 
-	Each subdirectory of the ROMS folder much match the name of the Console exactly in order to be scanned in properly
-	Incorrect modification of either text file may corrupted the database and cause the program to crash on startup
-	Ensure that the case matches for all custom paths
";

        public static string Description = @"";

        public static string Troubleshooting = @"-	If the program crashes on startup. Delete the Database and Preferences txt files. 
-	Skipping or hanging emulator/logo images when browsing. Ensure that the emulator name in the Preferences file. 
";

        public static string LicenseInfo = @"© Lenington Design ";

        public static string Features = @"UniCade Interface (© Lenington Design) 
- Includes 15 preconfigured consoles
- Command line based interface supports virtually the same feature set as the GUI version (With the exception of displaying game media images)
- Streamlined, modern and intuitive interface that anyone can operate.
- Supports up to 40 custom added game consoles
- Auto-Import feature locates, categorizes and updates the game library
- Advanced web scraping features automatically locate and download high quality media and info for all games in the library.
- Supported online databases include MobyGames.com and Metacritic.com
- Scraped info includes: console, release date, supported players, console name, critic score, developer, publisher, ESRB rating, ESRB descriptors, game description
- Supports box front, box back and screenshot images for all consoles
- Supports multiple users each with their own preferences including favorite games, total login count, game launch stats, personal info and allowed ESRB ratings. 
- Advanced scraping options allow a user to determine exactly which types of info and sites they want to access.
- Password protection option
- Pay per play functionality can monitor and restrict playtime either by game launch count or elapsed playtime
- Configure the amount of coins required to enable operations
- PayPerPlay option to allow interface navigation and only restrict game launching
- Option to restrict games for particular users by ESRB rating (Restrict viewing or launching)
- Restrict ESRB either by user or globally 
- Option to require mandatory login on interface launch (Default user is UniCade)
- Option to automatically rescan all libraries on startup
- Supports manually modifying or updating individual game info for all consoles
- Manually edit game or console info/media
- Monitor and track game launch count for specific users
- Game launch history by user and top played games.
- Optional play per play features (Charge by playtime duration or individual game launch)
- Detect, log and terminate unauthorized processes in real time
- Supports console info including release date, console description, total game count, emulator cmd line arguments, and custom exe location.
- Supports individual, global or console wide metadata scraping 
- Supports filtering files by rom extension
- Interfaces automatically recovers and re launches in event of a crash
- Optional and customizable splash screen
- Preference file includes clear descriptions of each value to allow manual modification.
- Customizable key bindings
- Advanced licensing engine prevent unauthorized use. 
- Customize ROM, media and database file locations
- Supports backup and import functionality for custom settings through a database file.
- Game info feature to display detailed info for the currently selected game including
- Customizable interface background and logos
- Supports custom game console images and logos.
- Security features monitor and terminate unauthorized processes in real time
- System wide keystroke hook can detect coin input and close commands from within any application
- Various system wide hotkeys and key combos such as Alt F4 are intercepted and disabled in real time to prevent circumvention of the interface
- During operation, the windows task bar is completely disabled.
";

        #endregion
    }
}
