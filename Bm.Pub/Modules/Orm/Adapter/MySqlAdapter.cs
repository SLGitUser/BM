using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;

namespace Bm.Modules.Orm.Adapter
{
    public partial class MySqlAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            var cmd = $"insert into {tableName} ({columnList}) values ({parameterList})";
            connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
            var r = connection.Query("Select LAST_INSERT_ID() id", transaction: transaction, commandTimeout: commandTimeout);

            var id = r.First().id;
            if (id == null) return 0;
            var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            if (!propertyInfos.Any()) return Convert.ToInt32(id);

            var idp = propertyInfos.First();
            idp.SetValue(entityToInsert, Convert.ChangeType(id, idp.PropertyType), null);

            return Convert.ToInt32(id);
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("`{0}`", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("`{0}` = @{1}", columnName, columnName);
        }

        /// https://github.com/StackExchange/dapper-dot-net/blob/master/Dapper.Contrib/SqlMapperExtensions.cs#L745
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName,
            string columnSql, string valuesSql, object entityToInsert)
        {
            var cmd = $"INSERT INTO {tableName} ({columnSql}) VALUES ({valuesSql})";
            connection.Execute(cmd, entityToInsert, transaction, commandTimeout);

            // REVIEW
            var r = connection.Query("SELECT LAST_INSERT_ID() id", transaction: transaction, commandTimeout: commandTimeout);
            var id = r.First().id;
            if (id == null) return 0;

            return Convert.ToInt32(id);
        }
    }
}