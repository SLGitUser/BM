using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace Bm.Services
{
    public class DbQuickService
    {

        private readonly IDbConnection _db;

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

        public dynamic Q(string sql)
        {
            return _db.Query(sql).ToList();
        }

        public bool Execute(string sql)
        {
            return _db.Execute(sql) >= 0;
        }



        ~DbQuickService()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
