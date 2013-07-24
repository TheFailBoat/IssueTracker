using System.Security.Cryptography;
using System.Text;

namespace IssueTracker.API.Utilities
{
    public static class StringUtils
    {
        /// <summary>
        ///     Generate a cryptographically secure random alphanumeric string
        /// </summary>
        /// <param name="len">number of chars in the output</param>
        /// <returns>alphanumeric string of length <paramref name="len" /></returns>
        public static string SecureRandom(int len)
        {
            var result = new StringBuilder();

            using (var rng = new RNGCryptoServiceProvider())
            {
                var buf = new byte[128];
                rng.GetBytes(buf);

                var i = 0;
                while (result.Length < len)
                {
                    result.Append(CharUtils.MapToAlphaNum(buf[i++]));

                    if (i < buf.Length) continue;
                    rng.GetBytes(buf);
                    i = 0;
                }
            }

            return result.ToString();
        }
    }
}