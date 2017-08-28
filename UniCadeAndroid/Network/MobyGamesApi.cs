using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniCadeAndroid.Constants;
using UniCadeAndroid.Interfaces;
using UniCadeAndroid.Objects;

namespace UniCadeAndroid.Network
{
    internal class MobyGamesApi
    {
        #region Properties

        /// <summary>
        /// The base URL for the MobyGames api
        /// </summary>
        private const string MobygamesApiBaseUrl = "https://api.mobygames.com/v1/games?title=";

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Update the info for the Game object with the MobyGames API
        /// </summary>
        /// <returns>A list of MobyGame objects</returns>
        public static async Task<bool> FetchGameInfo(IGame game)
        {
            //Replace all spaces with underscores for the api request
            string title = game.Title.Replace(' ', '_');
            using (var httpClient = new HttpClient())
            {
                //Attempt to access the Mobygames API
                string mobyUrl = MobygamesApiBaseUrl + title + "&api_key=" + ConstValues.MobyGamesApiKey;
                try
                {
                    var httpResponseMessage = await httpClient.GetAsync(mobyUrl);

                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string result = await httpResponseMessage.Content.ReadAsStringAsync();
                        var rootResult = JsonConvert.DeserializeObject<MobyRootObject>(result);
                        var gameResult = rootResult.Games.First();

                        //If a game was located, populate the game object fields
                        if (gameResult != null)
                        {
                            game.MobygamesApiId = gameResult.Game_id;
                            game.MobyGamesUrl = gameResult.Moby_url;
                            if (WebOps.ParseDescription)
                            {
                                game.Description = gameResult.Description;
                            }
                            if (WebOps.ParseGenres)
                            {
                                game.Genres = ConvertGenreListToString(gameResult.Genres);
                            }
                            if (WebOps.ParseOtherPlatforms)
                            {
                                game.OtherPlatforms = ConvertPlatformListToString(gameResult.Platforms);
                            }
                            if (WebOps.ParseUserScore)
                            {
                                game.UserReviewScore = gameResult.Moby_score.ToString(CultureInfo.InvariantCulture);
                            }
                        }
                        return true;
                        //return rootResult.games;
                    }
                }
                catch (WebException) //HttpRequestException
                {
                    return false;
                }
                catch (HttpRequestException)
                {
                    return false;
                }
                return true;
            }
        }


        #endregion

        #region  Helper Methods

        /// <summary>
        /// Convert a list of MobyGenre objects to a single comma seperated string
        /// </summary>
        /// <param name="genreList">List of MobyGenre objects</param>
        /// <returns>a comma seperated string of genres</returns>
        private static string ConvertGenreListToString(List<MobyGenre> genreList)
        {
            StringBuilder resultString = new StringBuilder();
            genreList.ForEach(g => resultString.Append(g.Genre_name + ", "));

            //Trim the last trailing comma
            resultString.Remove(resultString.Length - 2, 2);
            return resultString.ToString();
        }

        /// <summary>
        /// Convert a list of MobyPlatform objects to a single comma seperated string
        /// </summary>
        /// <param name="platformList">List of MobyPlatform objects</param>
        /// <returns>a comma seperated string of platforms</returns>
        private static string ConvertPlatformListToString(List<MobyPlatform> platformList)
        {
            StringBuilder resultString = new StringBuilder();
            platformList.ForEach(p => resultString.Append(p.Platform_name + ", "));

            //Trim the last trailing comma
            resultString.Remove(resultString.Length - 2, 2);
            return resultString.ToString();
        }
        
        #endregion

    }
}
