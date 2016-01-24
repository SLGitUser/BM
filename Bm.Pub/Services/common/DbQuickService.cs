using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Bm.Models.Dp;
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
        //public bool Delete<TModel>(List<TModel> model)
        //    where TModel : class
        //{
        //    var id = _db.Delete(model);
        //    return id;
        //}
        public bool Delete<TModel>(int[] ids)
            where TModel : class
        {
            using (MySqlConnection cn = new MySqlConnection("Server=127.0.0.1;Database=bm;Uid=root;Pwd=password01!"))
            {
                cn.Open();
                var models = cn.Get<TModel>(ids[0]);
                var isDelete = cn.Update(models);
                cn.Close();
                return isDelete;
            }
        }
        public bool Update<TModel>(TModel model)
            where TModel : class
        {
            using (MySqlConnection cn = new MySqlConnection("Server=127.0.0.1;Database=bm;Uid=root;Pwd=password01!"))
            {
                cn.Open();
                var isUpdate =cn.Update(model);
                cn.Close();
                return isUpdate;             
            }
        } 
        public List<TModel> SelectList<TModel>(IFieldPredicate predicate)
            where TModel : class
        {
            using (MySqlConnection cn = new MySqlConnection("Server=127.0.0.1;Database=bm;Uid=root;Pwd=password01!"))
            {
                cn.Open();
                IEnumerable<TModel> list = cn.GetList<TModel>(predicate);
                cn.Close();
                return list.ToList();
            }
        }


        ~DbQuickService()
        {
            _db.Close();
            _db.Dispose();
        }
    }
}
