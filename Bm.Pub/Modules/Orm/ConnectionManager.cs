using System;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Bm.Modules.Orm
{
    public static class ConnectionManager
    {
        private static readonly string ConnString;

        static ConnectionManager()
        {
            ConnString = ConfigurationManager.AppSettings["DbConnString"];

            if (string.IsNullOrWhiteSpace(ConnString))
                throw new ConfigurationErrorsException("数据库配置信息为空");
        }

        public static IDbConnection Open()
        {
            var conn = new MySqlConnection(ConnString);
            conn.Open();
            return conn;
        }

        /// <summary>
        /// コネクションプールからOpenした状態のコネクションを取得します
        /// </summary>
        public static void ExecuteAction(Action<IDbConnection> actions)
        {
            if (actions == null) return;
            using (var connection = new MySqlConnection(ConnString))
            {
                connection.Open();
                actions.Invoke(connection);
            }
        }
        
        /// <summary>
        /// コネクションプールからOpenした状態のコネクションを取得します
        /// </summary>
        public static TResult ExecuteResult<TResult>(Func<IDbConnection, TResult> actions)
        {
            if (actions == null) return default(TResult);
            using (var connection = new MySqlConnection(ConnString))
            {
                connection.Open();
                return actions.Invoke(connection);
            }
        }

        
        public static TModel ExecuteScalar<TModel>(string sql)
        {
            if (string.IsNullOrEmpty(sql)) return default(TModel);
            using (var connection = new MySqlConnection(ConnString))
            {
                connection.Open();
                return connection.Query<TModel>(sql).FirstOrDefault();
            }
        }

    }
}
