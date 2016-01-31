using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;

namespace Bm.Modules.Helper
{
    public static class ObjectHelper
    {
        /// <summary>
        /// 获得对象的属性字典
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static IDictionary<string, object> GetKeyValueDict(this object values)
        {
            if (values == null)
            {
                return new Dictionary<string, object>();
            }

            var dict = values as IDictionary<string, object>;
            if (dict != null)
            {
                return dict;
            }

            var map = new Dictionary<string, object>();
            var props = TypeDescriptor.GetProperties(values);
            foreach (PropertyDescriptor descriptor in props)
            {
                map.Add(descriptor.Name, descriptor.GetValue(values));
            }
            return map;
        }

        /// <summary>
        /// 遍历匿名对象
        /// </summary>
        /// <param name="dictStyleObject">匿名对象值</param>
        /// <param name="process">处理函数</param>
        public static void EachKeyValue(this object dictStyleObject, Action<string, object> process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));
            if (dictStyleObject == null) return;

            var dict = GetKeyValueDict(dictStyleObject);
            if (dict == null) return;

            foreach (var pair in dict)
            {
                process(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// 合并对象
        /// </summary>
        /// <param name="object1"></param>
        /// <param name="object2"></param>
        /// <returns></returns>
        public static IDictionary<string, object> CombineKeyValue(object object1, object object2)
        {
            var dict1 = GetKeyValueDict(object1);
            var dict2 = GetKeyValueDict(object2);

            var d = new Dictionary<string, object>(); //work with the Expando as a Dictionary
            foreach (var pair in dict1)
            {
                d[pair.Key] = pair.Value;
            }
            foreach (var pair in dict2)
            {
                if (d.ContainsKey(pair.Key))
                {
                    if (pair.Value == null || pair.Value is string)
                        d[pair.Key] = string.Concat(d[pair.Key], pair.Value);
                    else
                        d[pair.Key] = pair.Value;
                }
                else
                {
                    d[pair.Key] = pair.Value;
                }
            }
            return d;
        }

        //public static dynamic Combine(dynamic item1, dynamic item2)
        //{
        //    var dictionary1 = (IDictionary<string, object>)item1;
        //    var dictionary2 = (IDictionary<string, object>)item2;
        //    var result = new ExpandoObject();
        //    var d = (IDictionary<string, object>) result; //work with the Expando as a Dictionary

        //    foreach (var pair in dictionary1.Concat(dictionary2))
        //    {
        //        d[pair.Key] = pair.Value;
        //    }

        //    return result;
        //}
    }
}
