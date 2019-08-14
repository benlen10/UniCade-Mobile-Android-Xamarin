using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace UniCadeAndroid.Constants
{
    public static class Enums
    {
        #region Enums

        /// <summary>
        /// Enum Values for the supported APIs
        /// </summary>
        public enum Api
        {
            [StringValue("ThegamesDB")] ThegamesDb,
            [StringValue("IGDB")] Igdb,
            [StringValue("MobyGames")] MobyGames
        }

        /// <summary>
        /// Enum Values for ESRB content ratings
        /// </summary>
        public enum EsrbRatings
        {
            [StringValue("")]
            Null,
            [StringValue("Everyone")]
            Everyone,
            [StringValue("Everyone 10+")]
            Everyone10,
            [StringValue("Teen")]
            Teen,
            [StringValue("Mature")]
            Mature,
            [StringValue("Adults Only (Ao)")]
            Ao
        }

        /// <summary>
        /// Enum Values for ESRB content ratings
        /// </summary>
        public enum EsrbDescriptors
        {
            [StringValue("Mild Violence")] MildViolence,
            [StringValue("Violence")] Violence,
            [StringValue("Animated Violence")] AnimatedViolence,
            [StringValue("Realistic Violence")] RealisticViolence,
            [StringValue("Intense Violence")] IntenseViolence,
            [StringValue("Violent References")] ViolentReferences,
            [StringValue("Mild Fantasy Violence")] MildFantasyViolence,
            [StringValue("Fantasy Violence")] FantasyViolence,
            [StringValue("Mild Cartoon Violence")] MildCartoonViolence,
            [StringValue("Mild Cartoon Violence")] CartoonViolence,
            [StringValue("Comic Mischief")] ComicMischief,
            [StringValue("Animated Blood")] AnimatedBlood,
            [StringValue("Mild Blood")] MildBlood,
            [StringValue("Blood")] Blood,
            [StringValue("Realistic Blood")] RealisticBlood,
            [StringValue("Blood and Gore")] BloodGore,
            [StringValue("Animated Blood and Gore")] AnimatedBloodGore,
            [StringValue("Realstic Blood and Gore")] RealisticBloodGore,
            [StringValue("Edutainment")] Edutainment,
            [StringValue("Informational")] Informational,
            [StringValue("Crude Humor")] CrudeHumor,
            [StringValue("Mild Language")] MildLanguage,
            [StringValue("Language")] Language,
            [StringValue("Strong Language")] StrongLanguage,
            [StringValue("Mild Lyrics")] MildLyrics,
            [StringValue("Lyrics")] Lyrics,
            [StringValue("Strong Lyrics")] StrongLyrics,
            [StringValue("Mild Suggestive Themes")] MildSuggestiveThemes,
            [StringValue("Suggestive Themes")] SuggestiveThemes,
            [StringValue("Sexual Themes")] SexualThemes,
            [StringValue("Mild Sexual Theemes")] MildSexualThemes,
            [StringValue("Mild Sexual Content")] MildSexualContent,
            [StringValue("Sexual Content")] SexualContent,
            [StringValue("Strong Sexual Content")] StrongSexualContent,
            [StringValue("Sexual Violence")] SexualViolence,
            [StringValue("Simulated Gambling")] SimulatedGambline,
            [StringValue("Drug Reference")] DrugReferences,
            [StringValue("Use of Drugs")] UseOfDrugs,
            [StringValue("Alcohol Reference")] AlcoholReference,
            [StringValue("Use of Alcohol")] UseOfAlcohol,
            [StringValue("Use of Drugs and Alcohol")] UseOfDrugsAndAlcohol,
            [StringValue("Tobacco Reference")] UseOfTobacco,
            [StringValue("")] Null
        }

        /// <summary>
        /// Enum Values for Unicade user types
        /// </summary>
        public enum UserType
        {
            LocalAccount,
            CloudAccount
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Return the StringValue atribute from the enum
        /// </summary>
        /// <param name="enumValue">The current enum</param>
        /// <returns>the string value for the current enum</returns>
        [SuppressMessage("ReSharper", "PossibleNullReferenceException")]
        public static string GetStringValue(this Enum enumValue)
        {
            //Fetch the type and field info
            Type type = enumValue.GetType();
            FieldInfo fieldInfo = type.GetField(enumValue.ToString());

            //Fetch the string value attributes 
            StringValueAttribute[] attributes = fieldInfo.GetCustomAttributes(
                typeof(StringValueAttribute), false) as StringValueAttribute[];

            //Return the StringValue attribute
            return attributes.Length > 0 ? attributes[0].StringValue : fieldInfo.Name;
        }

        /// <summary>
        /// Define a StringValue attribute for the Enum class
        /// </summary>
        internal class StringValueAttribute : Attribute
        {
            public string StringValue { get; }

            public StringValueAttribute(string value)
            {
                StringValue = value;
            }
        }

        /// <summary>
        /// Convert a string value into an ESRB enum value
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static EsrbRatings ConvertStringToEsrbEnum(string text)
        {
            if (text.Equals("") || text.Contains("Null") || text.Contains("null") || text.Contains("None") || text.Equals(" "))
            {
                return EsrbRatings.Null;
            }
            if (text.Equals("Everyone"))
            {
                return EsrbRatings.Everyone;
            }
            if (text.Equals("Everyone 10+"))
            {
                return EsrbRatings.Everyone;
            }
            if (text.Equals("Teen"))
            {
                return EsrbRatings.Teen;
            }
            if (text.Equals("Mature"))
            {
                return EsrbRatings.Mature;
            }
            if (text.Contains("Adults Only"))
            {
                return EsrbRatings.Ao;
            }
                throw new ArgumentException("Invalid ESRB Rating");
        }

        #endregion
    }
}
