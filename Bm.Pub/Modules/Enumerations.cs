using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Bm.Modules
{
    public class Enumerations
    {
        public static string GetEnumDescription(Enum value, bool isDefaultWithValue = false)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes.Length > 0) return attributes[0].Description;

            return isDefaultWithValue ? value.ToString() : null;
        }

        public static IDictionary<TEnum, string> GenDict<TEnum>(bool isDefaultWithValue = false)
            where TEnum : struct
        {
            var dict = new Dictionary<TEnum, string>();
            var enumType = typeof (TEnum);
            foreach (var value in (TEnum[])Enum.GetValues(enumType))
            {
                var fi = enumType.GetField(value.ToString());

                var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

                var description = attributes.Length > 0 ? attributes[0].Description : (isDefaultWithValue ? value.ToString() : null);

                dict.Add(value, description);
            }
            return dict;
        }
    }
}
