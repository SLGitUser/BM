using System;
using System.Linq;
using System.Text;

namespace Bm.Modules.Helper
{
    public static partial class StringHelper
    {
        #region Type

        public static int? TryToInt(this string value)
        {
            int result;
            if (int.TryParse(value, out result)) return result;
            return default(int?);
        }

        public static int TryToInt(this string value, int dfltValue)
        {
            return TryToInt(value) ?? dfltValue;
        }

        public static long? TryToLong(this string value)
        {
            long result;
            if (long.TryParse(value, out result)) return result;
            return default(long?);
        }

        public static long TryToLong(this string value, long dfltValue)
        {
            return TryToLong(value) ?? dfltValue;
        }

        public static DateTime? TryToDateTime(this string value)
        {
            DateTime result;
            if (DateTime.TryParse(value, out result)) return result;
            return default(DateTime?);
        }

        public static DateTime TryToDateTime(this string value, DateTime dfltValue)
        {
            return TryToDateTime(value) ?? dfltValue;
        }

        public static bool? TryToBool(this string value)
        {
            if (string.IsNullOrEmpty(value)) return default(bool?);

            if (value.EqualsIgnoreCase("true")) return true;
            if (value.EqualsIgnoreCase("false")) return false;

            if (value.EqualsIgnoreCase("y")) return true;
            if (value.EqualsIgnoreCase("n")) return false;

            if (value.EqualsIgnoreCase("yes")) return true;
            if (value.EqualsIgnoreCase("no")) return false;

            if (value.EqualsIgnoreCase("t")) return true;
            if (value.EqualsIgnoreCase("f")) return false;

            if (value.EqualsIgnoreCase("1")) return true;
            if (value.EqualsIgnoreCase("0")) return false;

            return default(bool?);
        }

        public static bool TryToBool(this string value, bool dfltValue)
        {
            return TryToBool(value) ?? dfltValue;
        }

        #endregion

        #region to collection

        public static string[] ToStringArray(this string value)
        {
            if (string.IsNullOrEmpty(value)) return new string[0];
            return value.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static int[] ToIntArray(this string value)
        {
            return ToStringArray(value).Select(m => TryToInt(m))
                .Where(m => m.HasValue).Select(m => m.Value).ToArray();
        }

        public static long[] ToLongArray(this string value)
        {
            return ToStringArray(value).Select(m => TryToLong(m))
                .Where(m => m.HasValue).Select(m => m.Value).ToArray();
        }

        #endregion

        public static bool EqualsIgnoreCase(this string value, string another)
        {
            if (value == null) return another == null;
            return value.Equals(another, StringComparison.CurrentCultureIgnoreCase);
        }

        public static string Underline(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;

            var buff = new StringBuilder();
            var isToLower = true;
            var isAny = false;
            foreach (var c in value)
            {
                if (isToLower && char.IsUpper(c))
                {
                    if (isAny) buff.Append('_');
                    buff.Append(char.ToLower(c));
                    isToLower = false;
                }
                else
                {
                    isToLower = true;
                    buff.Append(c);
                }
                isAny = true;
            }
            return buff.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dfltValue"></param>
        /// <returns></returns>
        public static string SetWhenNullOrEmpty(this string value, string dfltValue)
        {
            return string.IsNullOrEmpty(value) ? dfltValue : value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="dfltValue"></param>
        /// <returns></returns>
        public static string SetWhenNullOrWhiteSpace(this string value, string dfltValue)
        {
            return string.IsNullOrWhiteSpace(value) ? dfltValue : value;
        }

        #region enum

        /// <summary>
        /// 将字符串转换为枚举类型
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(string value)
            where TEnum : struct
        {
            return ToEnum(value, default(TEnum));
        }

        /// <summary>
        /// 将字符串转换为枚举类型
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="value"></param>
        /// <param name="dfltValue"></param>
        /// <returns></returns>
        public static TEnum ToEnum<TEnum>(string value, TEnum dfltValue)
            where TEnum : struct
        {
            if (string.IsNullOrEmpty(value)) return dfltValue;
            TEnum t;
            return Enum.TryParse(value, true, out t) ? t : dfltValue;
        }

        #endregion
    }
}
