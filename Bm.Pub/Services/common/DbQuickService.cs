using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;
using DapperExtensions;
using DapperExtensions.Sql;
using MySql.Data.MySqlClient;

namespace Bm.Services.common
{
    public class DbQuickService
    {

        private readonly IDbConnection _db;

        static DbQuickService()
        {
            DapperExtensions.DapperExtensions.SqlDialect = new MySqlDialect();
        }

        public DbQuickService()
        {
            var conn = ConfigurationManager.AppSettings["DbConnString"];
            _db = new MySqlConnection(conn);
            _db.Open();
        }

        public T ExecuteScalar<T>(string sql)
        {
            return _db.ExecuteScalar<T>(sql);
        }

        public IList<T> Query<T>(string sql)
        {
            return _db.Query<T>(sql).ToList();
        }


        public bool Execute(string sql, object para = null)
        {
            return _db.Execute(sql, para) >= 0;
        }

        public bool Create<TModel>(TModel model)
            where TModel : class
        {
            int id = _db.Insert(model);
            return id >= 1;
        }


        ~DbQuickService()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
