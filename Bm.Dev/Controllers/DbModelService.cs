using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Bm.Annotations;
using Bm.Models.Base;
using Bm.Modules;
using Bm.Services;
using Bm.Services.common;
using com.senlang.Sdip.Util;
using static System.String;

namespace Bm.Dev.Controllers
{
    public class DbModelService
    {
        private static readonly string Nl = Environment.NewLine;

        private static readonly string packageName = "Bm.Models";

        public static Tuple<List<string>, List<string>> GetClass()
        {
            var assembly = typeof(Account).Assembly;


            var toCreateModels = new List<string>();
            var existModels = new List<string>();

            var service = new DbQuickService();

            var existList = service.Query<string>("show tables");

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
        /// ���ģ�Ͷ�Ӧ�����ݱ���
        /// </summary>
        /// <param name="modelType"></param>
        /// <returns></returns>
        public static string GetTableName(Type modelType)
        {
            var tableAttribute = modelType.GetAttribute<TableAttribute>();
            if (!IsNullOrEmpty(tableAttribute?.Name))
            {
                return tableAttribute.Name;
            }
            var pkgParts = modelType.FullName.Split('.');
            var packegName = pkgParts[Array.IndexOf(pkgParts, "Models") + 1];
            return Concat(packegName, modelType.Name).Underline();
        }

        /// <summary>
        /// ͬ��ģ�ͽṹ�����ݿ�
        /// </summary>
        /// <param name="modelType"></param>
        public static void SyncModelToDb(Type modelType)
        {
            var createSql = GetCreateSql(modelType);
            var dropSql = $"DROP TABLE IF EXISTS `{GetTableName(modelType)}`;";
            var sql = Concat(dropSql, Environment.NewLine, createSql);

            var service = new DbQuickService();
            service.Execute(sql);
        }

        private static string GetCreateSql(Type modelType)
        {

            var tableName = GetTableName(modelType);

            var head = $"CREATE TABLE IF NOT EXISTS `{tableName}` ({Nl}";

            var tableAttribute = modelType.GetAttribute<DisplayNameAttribute>();
            var tableNameDiscribe = tableAttribute?.DisplayName ?? Empty;

            //�õ�BODYͬʱ����β�������������
            var trail = $"){Environment.NewLine}COMMENT='{tableNameDiscribe}'{Nl}COLLATE='utf8_general_ci'{Nl}ENGINE=InnoDB;";

            var body = GetCreateSqlBody(ref trail, modelType);

            return Concat(head, body, trail);
        }

        private static string GetCreateSqlBody(ref string trail, Type modelType)
        {
            var properties = modelType.GetProperties();
            var list = new List<string>();

            //�ֶ�˳�����
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
                //IgnoreMapping ��ӳ�����ݿ�
                if (propertyInfo.GetAttribute<IgnoreMappingAttribute>() != null) continue;


                var name = propertyInfo.Name;
                var stringLengthAttribute = propertyInfo.GetAttribute<StringLengthAttribute>();
                var stringLen = stringLengthAttribute?.MaximumLength ?? 50;

                var required = propertyInfo.GetAttribute<RequiredAttribute>() != null ? "NOT NULL" : "NULL";

                var type = propertyInfo.PropertyType;
                //var primaryKey = propertyInfo.GetAttribute<KeyAttribute>();
                var autoIncrement = propertyInfo.Name.Equals("Id") ? "AUTO_INCREMENT" : Empty;
                //β�������������
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
                    dbType = Format(propertyInfo.Name.LastIndexOf("id", StringComparison.InvariantCultureIgnoreCase) != -1 ? "BIGINT({0}) UNSIGNED" : "BIGINT({0})", stringLen);
                }
                else if (type == typeof(long?))
                {
                    dbType = Format(propertyInfo.Name.LastIndexOf("id", StringComparison.InvariantCultureIgnoreCase) != -1 ? "BIGINT({0}) UNSIGNED" : "BIGINT({0})", stringLen);
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
            return Join("," + Environment.NewLine, list);
        }
    }
}