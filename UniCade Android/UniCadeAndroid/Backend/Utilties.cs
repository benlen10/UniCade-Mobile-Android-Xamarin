using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Backend
{
    internal class Utilties
    {
        /// <summary>
        /// Verify that a string contains only numeric chars
        /// </summary>
        public static bool IsAllDigits(string str)
        {
            return str.All(c => Char.IsDigit(c));
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
        internal static Uri GetEsrbLogoImage(Enums.EsrbRatings rating)
        {
            //Choose between classic and modern logos
            string logoType = Program.UseModernEsrbLogos ? "ModernEsrbLogos" : "EsrbLogos";

            if (rating.Equals(Enums.EsrbRatings.Everyone))
            {
                return new Uri("pack://application:,,,/UniCade;component/Resources/EsrbLogos/E.png".Replace("EsrbLogos", logoType));
            }
            if (rating.Equals(Enums.EsrbRatings.Everyone10))
            {
                return new  Uri("pack://application:,,,/UniCade;component/Resources/EsrbLogos/E10.png".Replace("EsrbLogos", logoType));
            }
            if (rating.Equals(Enums.EsrbRatings.Teen))
            {
                return new  Uri("pack://application:,,,/UniCade;component/Resources/EsrbLogos/T.png".Replace("EsrbLogos", logoType));
            }
            if (rating.Equals(Enums.EsrbRatings.Mature))
            {
                return new Uri("pack://application:,,,/UniCade;component/Resources/EsrbLogos/M.png".Replace("EsrbLogos", logoType));
            }
            return rating.Equals(Enums.EsrbRatings.Ao) ? new Uri("pack://application:,,,/UniCade;component/Resources/EsrbLogos/AO.png".Replace("EsrbLogos", logoType)) : null;
        }

        /// <summary>
        /// Return true if the specified folder has write access
        /// </summary>
        /// <param name="folderPath">The folder path to test</param>
        /// <returns>true if the specified folder has write access </returns>
        internal static bool HasWriteAccessToFolder(string folderPath)
        {
            try
            {
                Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }
    }
}
