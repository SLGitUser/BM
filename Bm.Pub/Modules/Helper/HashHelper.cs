using System.Security.Cryptography;
using System.Text;

namespace Bm.Modules.Helper
{
    public static class HashHelper
    {
        public static string Md5Hash(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;

            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(value);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}
