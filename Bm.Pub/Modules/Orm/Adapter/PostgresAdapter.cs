using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;

namespace Bm.Modules.Orm.Adapter
{
    public partial class PostgresAdapter : ISqlAdapter
    {
        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName, string columnList, string parameterList, IEnumerable<PropertyInfo> keyProperties, object entityToInsert)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("insert into {0} ({1}) values ({2})", tableName, columnList, parameterList);

            // If no primary key then safe to assume a join table with not too much data to return
            var propertyInfos = keyProperties as PropertyInfo[] ?? keyProperties.ToArray();
            if (!propertyInfos.Any())
                sb.Append(" RETURNING *");
            else
            {
                sb.Append(" RETURNING ");
                var first = true;
                foreach (var property in propertyInfos)
                {
                    if (!first)
                        sb.Append(", ");
                    first = false;
                    sb.Append(property.Name);
                }
            }

            var results = connection.Query(sb.ToString(), entityToInsert, transaction, commandTimeout: commandTimeout).ToList();

            // Return the key by assinging the corresponding property in the object - by product is that it supports compound primary keys
            var id = 0;
            foreach (var p in propertyInfos)
            {
                var value = ((IDictionary<string, object>)results.First())[p.Name.ToLower()];
                p.SetValue(entityToInsert, value, null);
                if (id == 0)
                    id = Convert.ToInt32(value);
            }
            return id;
        }

        public void AppendColumnName(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("\"{0}\"", columnName);
        }

        public void AppendColumnNameEqualsValue(StringBuilder sb, string columnName)
        {
            sb.AppendFormat("\"{0}\" = @{1}", columnName, columnName);
        }

        public int Insert(IDbConnection connection, IDbTransaction transaction, int? commandTimeout, string tableName,
            string columnSql, string valuesSql, object entityToInsert)
        {
            throw new NotImplementedException();
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