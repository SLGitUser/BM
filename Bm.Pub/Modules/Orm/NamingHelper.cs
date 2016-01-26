using System.Text.RegularExpressions;

namespace Bm.Modules.Orm
{
    public static class NamingHelper
    {
        #region naming transfer
        
        public static string MapUnderlineToPascal(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName)) return columnName;
            return Regex.Replace(columnName, @"^(.)|_(\w)",
                x => string.Concat(x.Groups[1].Value.ToUpper(), x.Groups[2].Value.ToUpper()));
        }

        public static string MapPascalToUnderline(string columnName)
        {
            if (string.IsNullOrWhiteSpace(columnName)) return columnName;
            return Regex.Replace(columnName, @"([A-Z])",
                x => "_" + x.Groups[1].Value.ToLower()).Remove(0, 1);
        }

        #endregion

        #region orm mapping
        
        public static string MapFieldToProperty(string columnName) => MapUnderlineToPascal(columnName);

        public static string MapClassToTable(string fullClassName)
        {
            if (string.IsNullOrWhiteSpace(fullClassName)) return fullClassName;
            return Regex.Replace(fullClassName, @"(([^.]*)\.)*([^.]+)$",
                x => MapPascalToUnderline(string.Concat(x.Groups[2].Value, x.Groups[3].Value)));
        }
        
        #endregion

    }
}
