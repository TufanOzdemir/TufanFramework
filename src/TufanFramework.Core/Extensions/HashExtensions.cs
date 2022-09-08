using System.Security.Cryptography;
using System.Text;

namespace TufanFramework.Common.Extensions
{
    public static class HashExtensions
    {
        /// <summary>
        /// Verilen bir prefix değerine göre MD5 Hash bilgisini döndürür.
        /// </summary>
        /// <param name="input">Döndürülmek istenilen string</param>
        /// <param name="prefix">Ön ek bilgisi</param>
        /// <returns>Hashlenmiş data</returns>
        public static string MD5Hash(this string input, string prefix)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(prefix, input)));
                StringBuilder sBuilder = new StringBuilder();

                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
        }
    }
}