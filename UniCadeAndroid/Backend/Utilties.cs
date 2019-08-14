using System;
using System.Linq;
using System.Text.RegularExpressions;
using Android.Content.Res;
using Java.IO;
using UniCadeAndroid.Activities;
using UniCadeAndroid.Constants;
using Uri = Android.Net.Uri;

namespace UniCadeAndroid.Backend
{
    internal class Utilties
    {
        /// <summary>
        /// Verify that a string contains only numeric chars
        /// </summary>
        public static bool IsAllDigits(string str)
        {
            return str.All(char.IsDigit);
        }

        /// <summary>
        /// Check if a string contains any invalid chars
        /// </summary>
        /// <param name="str">The string to validate</param>
        /// <returns>false if the string contains any invalid characters</returns>
        public static bool CheckForInvalidChars(string str)
        {
            return (str.IndexOfAny(ConstValues.InvalidChars) != -1);
        }


        /// <summary>
        /// Check if a string contains any invalid chars
        /// </summary>
        /// <param name="str">The string to validate</param>
        /// <returns>false if the string contains any invalid characters</returns>
        public static bool CheckForInvalidSplitChars(string str)
        {
            return (str.IndexOfAny(new[] { '|', '<', '>' }) != -1);
        }

        /// <summary>
        /// Remove and replace all invalid chars from the input string
        /// </summary>
        internal static string RemoveInvalidChars(string str)
        {
            str = Regex.Replace(str, @"\t|\n|\r", " ");
            return str.Replace("\"", "");
        }

        /// <summary>
        /// Attempt to parse an ESRB descriptor enum from a string
        /// Return Null enum is not found
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static Enums.EsrbDescriptors ParseEsrbDescriptor(string str)
        {
            //Trim leading and trailing spaces before comparing the string values
            str = str.Trim();
            foreach (Enums.EsrbDescriptors descriptor in Enum.GetValues(typeof(Enums.EsrbDescriptors)))
            {
                if (string.Equals(str, descriptor.GetStringValue(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return descriptor;
                }
            }
            return Enums.EsrbDescriptors.Null;
        }

        /// <summary>
        /// Attempt to parse an ESRB descriptor enum from a string
        /// Return Null enum is not found
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static Enums.EsrbRatings ParseEsrbRating(string str)
        {
            if (str != null)
            {
                if (str.IndexOf("Everyone 10", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return Enums.EsrbRatings.Everyone10;
                }
                if (str.IndexOf("Everyone", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return Enums.EsrbRatings.Everyone;
                }
                if (str.IndexOf("Teen", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return Enums.EsrbRatings.Teen;
                }
                if (str.IndexOf("Mature", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return Enums.EsrbRatings.Mature;
                }
                if (str.IndexOf("Adult", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return Enums.EsrbRatings.Ao;
                }
            }
            return Enums.EsrbRatings.Null;
        }

        /// <summary>
        /// Return the coresponding ESRB logo for the rating
        /// </summary>
        /// <param name="rating">An EsrbRating enum</param>
        /// <returns>The BitMap image corresponding to the rating enum</returns>
        internal static int GetEsrbLogoImage(Enums.EsrbRatings rating)
        {
            if (rating.Equals(Enums.EsrbRatings.Everyone))
            {
                return Resource.Drawable.Esrb_E; 
            }
            if (rating.Equals(Enums.EsrbRatings.Everyone10))
            {
                return Resource.Drawable.Esrb_ET; 
            }
            if (rating.Equals(Enums.EsrbRatings.Teen))
            {
                return Resource.Drawable.Esrb_T; 
            }
            if (rating.Equals(Enums.EsrbRatings.Mature))
            {
                return Resource.Drawable.Esrb_M; 
            }
            if (rating.Equals(Enums.EsrbRatings.Ao))
			{
                return Resource.Drawable.Esrb_AO; 
			}
            return 0;
        }
    }
}
