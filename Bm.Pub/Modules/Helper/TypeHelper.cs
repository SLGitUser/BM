using System;
using System.Linq;
using System.Reflection;

namespace Bm.Modules.Helper
{
    public static class TypeHelper
    {
        #region Type

        public static T GetAttribute<T>(this Type type)
        {
            var types = type.GetCustomAttributes(typeof(T), true);
            return types.Length == 0 ? default(T) : (T)types[0];
        }

        public static T[] GetAttributes<T>(this Type type)
        {
            var types = type.GetCustomAttributes(typeof(T), true);
            return types.Length == 0 ? new T[0] : types.Select(m => (T)m).ToArray();
        }

        #endregion

        #region PropertyInfo

        public static T GetAttribute<T>(this PropertyInfo type)
        {
            var types = type.GetCustomAttributes(typeof(T), true);
            return types.Length == 0 ? default(T) : (T)types[0];
        }

        public static T[] GetAttributes<T>(this PropertyInfo type)
        {
            var types = type.GetCustomAttributes(typeof(T), true);
            return types.Length == 0 ? new T[0] : types.Select(m => (T)m).ToArray();
        }

        #endregion

        #region 类型判断

        private static readonly Type[] NumericTypes = 
        {
            typeof(int), typeof (uint), typeof (int?),
            typeof(long), typeof(ulong), typeof(long?),
            typeof(short), typeof(ushort), typeof(short?),
            typeof(byte), typeof(sbyte), typeof(byte?),
            typeof(decimal), typeof(decimal?),
            typeof(float), typeof(float?),
            typeof(double), typeof(double?)
        };

        private static readonly Type[] DateTimeTypes = 
        {
            typeof(DateTime), typeof (DateTime?)
        };

        private static readonly Type[] BoolTypes = 
        {
            typeof(bool), typeof (bool?)
        };

        private static readonly Type[] StringValueTypes = 
        {
            typeof(string), typeof(DateTime), typeof (DateTime?)
        };

        /// <summary>
        /// 是否数值类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsNumeric(this Type type)
        {
            return NumericTypes.Contains(type);
        }

        /// <summary>
        /// 是否日期类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsDateTime(this Type type)
        {
            return DateTimeTypes.Contains(type);
        }

        /// <summary>
        /// 是否布尔类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsBool(this Type type)
        {
            return BoolTypes.Contains(type);
        }

        /// <summary>
        /// 是否字符类型（用于SQL）
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsStringValue(this Type type)
        {
            return StringValueTypes.Contains(type);
        }

        #endregion
    }
}
