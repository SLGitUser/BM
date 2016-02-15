using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bm.Models.Setting;
using Bm.Modules.Helper;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using log4net;

namespace Bm.Services.Common
{
    public class CommonSetService
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static IList<CommonSet> GetSettingItems(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));
            return ConnectionManager.ExecuteResult(
                conn =>
                {
                    var query = new Criteria<CommonSet>()
                        .Where(m => m.Key, Op.Eq, key);
                    return conn.Query(query);
                });
        }

        /// <summary>
        /// 根据应用程序和键获取字符串值
        /// </summary>
        /// <param name="app"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string app, string key)
        {
            if (string.IsNullOrEmpty(app))
                throw new ArgumentNullException(nameof(app));

            var list = GetSettingItems(key);
            var item = list.FirstOrDefault(m => m.App.ToStringArray().Contains(app))
                     ?? list.FirstOrDefault(m => m.App.Equals("*"));
            if (item == null)
            {
                Logger.Error($"应用{app}的键{key}不存在");
            }
            return item?.Value;
        }

        /// <summary>
        /// 根据键获取字符串值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(string key)
        {
            var item = GetSettingItems(key).FirstOrDefault();
            if (item == null)
            {
                Logger.Error($"键{key}不存在");
            }
            return item?.Value;
        }

        //public static int GetInt(string key)
        //{
        //    var result = GetString(key).TryToInt();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为整数");
        //}

        //public static int GetInt(string app, string key)
        //{
        //    var result = GetString(app, key).TryToInt();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为整数");
        //}

        //public static long GetLong(string key)
        //{
        //    var result = GetString(key).TryToLong();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为长整数");
        //}

        //public static long GetLong(string app, string key)
        //{
        //    var result = GetString(app, key).TryToLong();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为长整数");
        //}

        //public static decimal GetDecimal(string key)
        //{
        //    var result = GetString(key).TryToDecimal();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为小数");
        //}

        //public static decimal GetDecimal(string app, string key)
        //{
        //    var result = GetString(app, key).TryToDecimal();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为小数");
        //}

        //public static bool GetBool(string key)
        //{
        //    var result = GetString(key).TryToBool();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为布尔值");
        //}

        //public static bool GetBool(string app, string key)
        //{
        //    var result = GetString(app, key).TryToBool();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为布尔值");
        //}

        //public static DateTime GetDateTime(string key)
        //{
        //    var result = GetString(key).TryToDateTime();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为日期时间");
        //}

        //public static DateTime GetDateTime(string app, string key)
        //{
        //    var result = GetString(app, key).TryToDateTime();
        //    if (result.HasValue) return result.Value;
        //    throw new NotSupportedException("字符串不能转换为日期时间");
        //}
    }
}
