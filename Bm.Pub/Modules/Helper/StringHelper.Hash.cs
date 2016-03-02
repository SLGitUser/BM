using System.Security.Cryptography;
using System.Text;

namespace Bm.Modules.Helper
{
	/// <summary>
    /// 字符串Hash算法
    /// </summary>
    public partial class StringHelper
    {
		/// <summary>
        /// 计算字符串的MD5摘要
        /// </summary>
        /// <param name="input">输入值</param>
        /// <returns></returns>
		public static string Md5Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            using (var md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                var sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                foreach (var t in data)
                {
                    sBuilder.Append(t.ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }

        }
    }
}
