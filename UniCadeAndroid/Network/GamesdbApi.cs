using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using UniCadeAndroid.Backend;
using UniCadeAndroid.Interfaces;
using UniCadeAndroid.Objects;
using Console = UniCadeAndroid.Objects.Console;

namespace UniCadeAndroid.Network
{
    /// <summary>
    /// Fetches information from TheGamesDB.
    /// </summary>
    public static class GamesdbApi
    {

        #region Properties 

        /// <summary>
        /// The base image path that should be prepended to all the relative image paths to get the full paths to the images.
        /// </summary>
        public const String BaseImgUrl = @"http://thegamesdb.net/banners/";

        #endregion

        #region  Public Methods


        /// <summary>
        /// Gets a collection of games matched up with loose search terms.
        /// </summary>
        /// <returns>A collection of games that matched the search terms</returns>
        public static bool UpdateGameInfo(IGame game, bool scrapeImages = true)
        {
            //Ensure the console name is correct for TheGamesDb database
            string consoleName = ConvertConsoleName(game.ConsoleName);

            //Attempt to load the XML document
            XmlDocument xmlDocument = new XmlDocument();
            try
            {
                xmlDocument.Load(@"http://thegamesdb.net/api/GetGamesList.php?name=" + game.Title + @"&platform=" +
                                 consoleName);
            }
            catch (WebException)
            {
                return false;
            }

            //Set the root node and fetch the enumerator
            XmlNode rootNode = xmlDocument.DocumentElement;

            if (rootNode == null)
            {
                return false;
            }

            IEnumerator enumerator = rootNode.GetEnumerator();

            // Iterate through all games
            while (enumerator.MoveNext())
            {
                int gameId = -1;
                var gameNode = (XmlNode)enumerator.Current;

                IEnumerator ienumGame = gameNode.GetEnumerator();
                while (ienumGame.MoveNext())
                {
                    var attributeNode = (XmlNode)ienumGame.Current;

                    //Locate the game id attribute
                    if (attributeNode.Name == "id")
                    {
                        int.TryParse(attributeNode.InnerText, out gameId);
                        break;
                    }
                }

                if (gameId < 0)
                {
                    return false;
                }

                xmlDocument = new XmlDocument();
                xmlDocument.Load(@"http://thegamesdb.net/api/GetGame.php?id=" + gameId);

                rootNode = xmlDocument.DocumentElement;

                XmlNode platformNode = rootNode?.FirstChild.NextSibling;

                if (platformNode == null)
                {
                    return false;
                }

                ienumGame = platformNode.GetEnumerator();
                while (ienumGame.MoveNext())
                {
                    var attributeNode = (XmlNode)ienumGame.Current;

                    // Iterate through all platform attributes
                    if ((attributeNode.Name == "Overview") || (attributeNode.Name == "overview") && WebOps.ParseDescription)
                    {
                        game.Description = attributeNode.InnerText;
                    }
                    else if (attributeNode.Name == "ESRB" && WebOps.ParseEsrbRating)
                    {
                        game.EsrbRating = Utilties.ParseEsrbRating(attributeNode.InnerText);
                    }
                    else if (attributeNode.Name == "Players" && WebOps.ParsePlayerCount)
                    {
                        game.SupportedPlayerCount = attributeNode.InnerText;
                    }
                    else if (attributeNode.Name == "Publisher" && WebOps.ParsePublisher)
                    {
                        game.PublisherName = attributeNode.InnerText;
                    }
                    else if (attributeNode.Name == "Developer" && WebOps.ParseDeveloper)
                    {
                        game.DeveloperName = attributeNode.InnerText;
                    }
                    else if (attributeNode.Name == "Rating" && WebOps.ParseCriticScore)
                    {
                        game.CriticReviewScore = attributeNode.InnerText;
                    }
                    else if (attributeNode.Name == "Genres" && WebOps.ParseGenres)
                    {
                        IEnumerator ienumGenres = attributeNode.GetEnumerator();
                        game.Genres = "";
                        while (ienumGenres.MoveNext())
                        {
                            game.Genres += ((XmlNode)ienumGenres.Current).InnerText + ", ";
                        }
                        game.Genres = game.Genres.Substring(0, game.Genres.Length - 2);
                    }
                    else if (attributeNode.Name == "Images")
                    {
                        if (scrapeImages)
                        {
                            GameImages gameImages = new GameImages();
                            gameImages.LoadFromNode(attributeNode);

                            //If the media directory does not exist, create it
          
                            string directoryPath = Android.OS.Environment.ExternalStorageDirectory.Path + @"/UniCade/Media/Games/" + game.ConsoleName + "//";
                            if (!Directory.Exists(directoryPath))
                            {
                                Directory.CreateDirectory(directoryPath);
                            }

                            using (WebClient client = new WebClient())
                            {
                                string boxBackImagePath = directoryPath + game.Title + "_BoxBack.jpg";
                                string boxfrontImagePath = directoryPath + game.Title + "_BoxFront.jpg";
                                string screenshotImagePath =
                                    directoryPath + game.Title + "_Screenshot.jpg";

                                if (gameImages.BoxartBack != null && !File.Exists(boxBackImagePath) && WebOps.ParseBoxBackImage)
                                {   
                                    string boxbackImageUrl = BaseImgUrl + gameImages.BoxartBack;
                                    client.DownloadFile(boxbackImageUrl, boxBackImagePath);
                                }

                                if (gameImages.BoxartFront != null && !File.Exists(boxfrontImagePath) && WebOps.ParseBoxFrontImage)
                                {  
                                    string boxfrontImageUrl = BaseImgUrl + gameImages.BoxartFront;
                                    client.DownloadFile(boxfrontImageUrl, boxfrontImagePath);
                                }

                                if (gameImages.Screenshots.Count > 0 && !File.Exists(screenshotImagePath) && WebOps.ParseScreenshot)
                                {  
                                    string screenshotImageUrl = BaseImgUrl + gameImages.Screenshots.First();
                                    client.DownloadFile(screenshotImageUrl, screenshotImagePath);
                                }
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }


        /// <summary>
        /// Gets all data for a specific platform.
        /// </summary>
        internal static bool UpdateConsoleInfo(Console console)
        {
            int consoleId = -1;
            XmlDocument consoleListDoc = new XmlDocument();

            string validConsoleName = ConvertConsoleName(console.ConsoleName);

            consoleListDoc.Load(@"http://thegamesdb.net/api/GetPlatformsList.php");
            XmlNode root = consoleListDoc.DocumentElement;
            if (root != null)
            {
                root.GetEnumerator();

                XmlNode platformNode = root.FirstChild.NextSibling;

                if (platformNode != null)
                {
                    IEnumerator ienumPlatform = platformNode.GetEnumerator();
                    while (ienumPlatform.MoveNext())
                    {
                        var attributeNode = (XmlNode) ienumPlatform.Current;

                        IEnumerator ienumAttr = attributeNode.GetEnumerator();
                        int tempId = -1;
                        while (ienumAttr.MoveNext())
                        {
                            var fieldNode = (XmlNode)ienumAttr.Current;

                            if (fieldNode.Name == "id")
                            {
                                tempId = int.Parse(fieldNode.InnerText);
                            }
                            if (fieldNode.Name == "name")
                            {
                                if (validConsoleName.Equals(fieldNode.InnerText))
                                {
                                    consoleId = tempId;
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(@"http://thegamesdb.net/api/GetPlatform.php?id=" + consoleId);

            root = doc.DocumentElement;
            if (root != null)
            {
                root.GetEnumerator();

                XmlNode platformNode = root.FirstChild.NextSibling;

                if (platformNode != null)
                {
                    IEnumerator ienumPlatform = platformNode.GetEnumerator();
                    while (ienumPlatform.MoveNext())
                    {
                        var attributeNode = (XmlNode)ienumPlatform.Current;

                        // Iterate through all platform attributes
                        if (attributeNode.Name == "overview" && WebOps.ParseConsoleDescription)
                        {
                            console.ConsoleInfo = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "developer" && WebOps.ParseConsoleDeveloper)
                        {
                            console.Developer = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "cpu" && WebOps.ParseConsoleCpu)
                        {
                            console.Cpu = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "memory" && WebOps.ParseConsoleRam)
                        {
                            console.Ram = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "graphics" && WebOps.ParseConsoleGraphics)
                        {
                            console.Graphics = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "display" && WebOps.ParseConsoleNativeResolution)
                        {
                            console.DisplayResolution = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "Rating" && WebOps.ParseConsoleUserReviews)
                        {
                            console.ConsoleRating = attributeNode.InnerText;
                        }
                        else if (attributeNode.Name == "Images")
                        {
                            // platform.Images.FromXmlNode(attributeNode);
                        }
                    }
                }

                return true;
            }
            return false;
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Convert console names into valid platform
        /// </summary>
        /// <param name="consoleName"></param>
        /// <returns></returns>
        private static string ConvertConsoleName(string consoleName)
        {
            if (consoleName.Contains("PS1") || consoleName.Contains("PSX") || consoleName.Equals("Sony Playstation 1"))
            {
                return "Sony Playstation";
            }
            if (consoleName.Contains("PS2"))
            {
                return "Sony Playstation 2";
            }
            if (consoleName.Contains("PSP"))
            {
                return "Sony Playstation Portable";
            }
            if (consoleName.Contains("GBA"))
            {
                return "Nintendo N64";
            }
            if (consoleName.Contains("GBA"))
            {
                return "Nintendo Game Boy Advance";
            }
            if (consoleName.Contains("GBC"))
            {
                return "Nintendo Game Boy Color";
            }
            if (consoleName.Equals("DS"))
            {
                return "Nintendo DS";
            }
            if (consoleName.Contains("3DS"))
            {
                return "Nintendo 3DS";
            }
            if (consoleName.Contains("SNES"))
            {
                return "Super Nintendo (SNES)";
            }
            if (consoleName.Equals("NES"))
            {
                return " Nintendo Entertainment System (NES)";
            }
            if (consoleName.Contains("Gamecube"))
            {
                return "Nintendo Gamecube";
            }
            if (consoleName.Contains("Wii U"))
            {
                return "Nintendo Wii U";
            }
            if (consoleName.Equals("Wii"))
            {
                return "Nintendo Wii";
            }
            if (consoleName.Contains("Windows")|| consoleName.Contains("Steam"))
            {
                return "PC";
            }
            if (consoleName.Contains("Genisis"))
            {
                return "Sega Genesis";
            }
            if (consoleName.Contains("Dreamcast"))
            {
                return "Sega Dreamcast";
            }
            if (consoleName.Contains("Saturn"))
            {
                return "Sega Saturn";
            }
            return consoleName;
        }
        #endregion
    }
}

