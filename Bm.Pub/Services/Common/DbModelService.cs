using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Bm.Annotations;
using Bm.Models.Base;
using Bm.Modules.Helper;


namespace Bm.Services.Common
{
    /// <summary>
    /// 数据模型服务
    /// </summary>
    public static class DbModelService
    {
        private static readonly string Nl = Environment.NewLine;

        private static readonly string packageName = "Bm.Models";

        public static Tuple<List<string>, List<string>> GetClass()
        {
            var assembly = typeof(Account).Assembly;


            var toCreateModels = new List<string>();
            var existModels = new List<string>();


            var existList = BaseDbService.Query<string>("show tables");

            foreach (var type in assembly.GetExportedTypes())
            {
                if (type.IsEnum) continue;
                if (type.IsInterface) continue;
                if (!type.FullName.Contains(packageName)) continue;

                var name = GetTableName(type);
                if (existList.Contains(name))
                    existModels.Add(type.FullName);
                else
                    toCreateModels.Add(type.FullName);
            }
            existModels.Sort();
            toCreateModels.Sort();

            return Tuple.Create(existModels, toCreateModels);
        }
        
        /// <summary>
        /// 获得模型对应的数据表名
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public static string GetTableName(Type modelType)
        {
            var tableAttribute = modelType.GetAttribute<TableAttribute>();
            if (!string.IsNullOrEmpty(tableAttribute?.Name))
            {
                return tableAttribute.Name;
            }
            var pkgParts = modelType.FullName.Split('.');
            var packegName = pkgParts[Array.IndexOf(pkgParts, "Models") + 1];
            return string.Concat(packegName, modelType.Name).Underline();
        }

        /// <summary>
        /// 同步模型结构到数据库
        /// </summary>
        /// <param name="modelType"></param>
        public static void SyncModelToDb(Type modelType)
        {
            var createSql = GetCreateSql(modelType);
            var dropSql = $"DROP TABLE IF EXISTS `{GetTableName(modelType)}`;";
            var sql = string.Concat(dropSql, Environment.NewLine, createSql);

            BaseDbService.Execute(sql);
        }

        private static string GetCreateSql(Type modelType)
        {

            var tableName = GetTableName(modelType);

            var head = $"CREATE TABLE IF NOT EXISTS `{tableName}` ({Nl}";

            var tableAttribute = modelType.GetAttribute<DisplayNameAttribute>();
            var tableNameDiscribe = tableAttribute?.DisplayName ?? string.Empty;

            //得到BODY同时加入尾部创建主键语句
            var trail = $"){Environment.NewLine}COMMENT='{tableNameDiscribe}'{Nl}COLLATE='utf8_general_ci'{Nl}ENGINE=InnoDB;";

            var body = GetCreateSqlBody(ref trail, modelType);

            return string.Concat(head, body, trail);
        }

        private static string GetCreateSqlBody(ref string trail, Type modelType)
        {
            var properties = modelType.GetProperties();
            var list = new List<string>();

            //字段顺序调整
            var beginPorps = new[] { "Id" };
            var trailPorps = new[] { "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" };
            var orderedProp = properties.OrderBy(m =>
            {
                if (beginPorps.Contains(m.Name)) return 0;
                if (trailPorps.Contains(m.Name)) return 1;
                return 1;
            });

            foreach (var propertyInfo in orderedProp)
            {
                //IgnoreMapping 不映射数据库
                if (propertyInfo.GetAttribute<IgnoreMappingAttribute>() != null) continue;


                var name = propertyInfo.Name;
                var stringLengthAttribute = propertyInfo.GetAttribute<StringLengthAttribute>();
                var stringLen = stringLengthAttribute?.MaximumLength ?? 50;

                var required = propertyInfo.GetAttribute<RequiredAttribute>() != null ? "NOT NULL" : "NULL";

                var type = propertyInfo.PropertyType;
                //var primaryKey = propertyInfo.GetAttribute<KeyAttribute>();
                var autoIncrement = propertyInfo.Name.Equals("Id") ? "AUTO_INCREMENT" : string.Empty;
                //尾部创建主键语句
                if (propertyInfo.Name.Equals("Id"))
                {
                    trail = $"{Nl},PRIMARY KEY (`{name}`){trail}";
                }
                var displayNameAttribute = propertyInfo.GetAttribute<DisplayNameAttribute>();
                var comment = displayNameAttribute != null ? displayNameAttribute.DisplayName : "";
                string dbType;
                if (type == typeof(string))
                {
                    dbType = $"VARCHAR({stringLen})";
                }
                else if (type == typeof(bool))
                {
                    dbType = "TINYINT(1)";
                }
                else if (type == typeof(bool?))
                {
                    dbType = "TINYINT(1) NULL";
                }
                else if (type == typeof(int))
                {
                    dbType = $"INT({stringLen})";
                }
                else if (type == typeof(int?))
                {
                    dbType = $"INT({stringLen})";
                    required = "NULL DEFAULT NULL";
                }
                else if (type == typeof(long))
                {
                    dbType = string.Format(propertyInfo.Name.LastIndexOf("id", StringComparison.InvariantCultureIgnoreCase) != -1 ? "BIGINT({0}) UNSIGNED" : "BIGINT({0})", stringLen);
                }
                else if (type == typeof(long?))
                {
                    dbType = string.Format(propertyInfo.Name.LastIndexOf("id", StringComparison.InvariantCultureIgnoreCase) != -1 ? "BIGINT({0}) UNSIGNED" : "BIGINT({0})", stringLen);
                    required = "NULL DEFAULT NULL";
                }
                else if (type == typeof(DateTime))
                {
                    dbType = "DATETIME";
                    required = "NOT NULL";
                }
                else if (type == typeof(DateTime?))
                {
                    dbType = "DATETIME ";
                    required = "NULL DEFAULT NULL";
                }
                else if (type == typeof(decimal))
                {
                    dbType = "DECIMAL(15,2)";
                    required = "NOT NULL";
                }
                else if (type == typeof(decimal?))
                {
                    dbType = "DECIMAL(15,2)";
                    required = "NULL DEFAULT NULL";
                }
                else
                {
                    dbType = $"VARCHAR({stringLen})";
                }

                list.Add($"`{name}` {dbType} {required} {autoIncrement} COMMENT '{comment}'");
            }
            return string.Join("," + Environment.NewLine, list);
        }
    }
}