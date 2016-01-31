using System;

namespace Bm.Modules.Helper
{
    public static partial class StringHelper
    {

        //private const string BaseChar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        //private const int Len = 62;

        private const string BaseChar = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const int Len = 36;

        //转换为段字符
        private static string GetLongNo(ulong num, int length)
        {
            var str = "";
            while (num > 0)
            {
                var cur = (int)(num % Len);
                str = BaseChar[cur] + str;
                num = num / Len;
            }
            str = str.Length > length ? str.Substring(str.Length - length) : str.PadLeft(length, '0');
            return str;
        }

        //解析段字符
        private static ulong GetLongNum(string strNo)
        {
            ulong num = 0;
            for (var i = 0; i < strNo.Length; i++)
            {
                num += (ulong)BaseChar.IndexOf(strNo[i]) * (ulong)Math.Pow(BaseChar.Length, strNo.Length - i - 1);
            }

            return num;
        }

        /// <summary>
        /// 压缩GUID
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static string GetGuidNo(Guid g)
        {
            var s = g.ToString("N").ToUpper();
            var s1 = s.Substring(0, 16);
            var s2 = s.Substring(16);
            var l1 = ulong.Parse(s1, System.Globalization.NumberStyles.HexNumber);
            var l2 = ulong.Parse(s2, System.Globalization.NumberStyles.HexNumber);
            var str1 = GetLongNo(l1, 13);
            var str2 = GetLongNo(l2, 13);
            return string.Concat(str1, str2);
        }

        /// <summary>
        /// 获取GUID
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static Guid GetGuid(string str)
        {
            if (string.IsNullOrEmpty(str) || str.Length != 26)
            {
                throw new Exception("字符串错误！长度必须是26位！");
            }
            var s1 = str.Substring(0, 13);
            var s2 = str.Substring(13);
            var l1 = GetLongNum(s1);
            var l2 = GetLongNum(s2);
            var str1 = l1.ToString("X");
            var str2 = l2.ToString("X");
            var strGuid = str1.PadLeft(16, '0');
            strGuid += str2.PadLeft(16, '0');
            var g = new Guid(strGuid);
            return g;
        }
    }
}
