using System.Security.Cryptography;
using System.Text;
using UniCadeAndroid.Constants;

namespace UniCadeAndroid.Backend
{
    internal class CryptoEngine
    {
        #region Public Methods

        /// <summary>
        /// Hashes a string using SHA256 algorthim 
        /// </summary>
        /// <param name="data"> The input string to be hashed</param>
        /// <returns>Hashed string using SHA256</returns>
        public static string Sha256Hash(string data)
        {
            if (data == null)
            {
                return null;
            }
            SHA256Managed sha256 = new SHA256Managed();
            byte[] hashData = sha256.ComputeHash(Encoding.Default.GetBytes(data));
            StringBuilder returnValue = new StringBuilder();

            foreach (byte t in hashData)
            {
                returnValue.Append(t.ToString());
            }
            return returnValue.ToString();
        }

        public static bool ValidateLicense(string licenseUserName, string licenseKey)
        {
            return Sha256Hash(licenseUserName + ConstValues.HashKey).Equals(licenseKey);
        }

        #endregion

    }
}
