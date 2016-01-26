using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Bm.Models.Common;
using Bm.Modules.Helper;
using Bm.Modules.Orm;
using Dapper;

namespace Bm.Services.Common
{
    public class BaseDbService 
        //: IDisposable
    {
        private static readonly ConcurrentBag<RuntimeTypeHandle> TypeHandles = new ConcurrentBag<RuntimeTypeHandle>();

        //private readonly string _accountNo;

        //private readonly DateTime _now = DateTime.Now;

        //private readonly IDbConnection _conn;

        //private static readonly string ConnString;

        //#region constructor and deconstructor

        //static BaseDbService()
        //{
        //    //ConnString = ConfigurationManager.AppSettings["DbConnString"];
        //}

        //public BaseDbService()
        //{
        //    //_conn = new MySqlConnection(ConnString);
        //    //_conn.Open();
        //}

        //public BaseDbService(string accountNo) : this()
        //{
        //    _accountNo = accountNo;
        //}


        //~BaseDbService()
        //{
        //    _conn.Dispose();
        //    //_conn.Close();
        //}

        //public void Dispose()
        //{
        //    throw new NotImplementedException();
        //}

        //#endregion


        #region static methods

        private static void Register<TModel>()
            where TModel : class
        {
            if (TypeHandles.Contains(typeof(TModel).TypeHandle)) return;

            TypeHandles.Add(typeof(TModel).TypeHandle);
            SetSnakeToPascal<TModel>();
        }


        /// <summary>
        /// カスタムマッピング用のメソッド。ただしdynamicで取得した場合は変換されない
        /// 参考：http://neue.cc/2012/12/11_390.html
        /// </summary>
        private static void SetSnakeToPascal<T>()
        {
            var mapper = new CustomPropertyTypeMap(typeof(T), (type, columnName) =>
            {
                var propName = NamingHelper.MapFieldToProperty(columnName);
                return type.GetProperty(propName);
            });

            SqlMapper.SetTypeMap(typeof(T), mapper);
        }

        public static bool Create<TModel>(TModel model)
            where TModel : class
        {
            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var r = conn.Insert(model, trans);
                trans.Commit();
                return r > -1;
            }
        }

        public static IList<TModel> Query<TModel>()
            where TModel : class
        {
            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var r = conn.GetAll<TModel>().ToList();
                return r;
            }
        }

        public static IList<TModel> Query<TModel>(string sql, object param = null)
            where TModel : class
        {
            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var r = conn.Query<TModel>(sql, param).ToList();
                return r;
            }
        }

        public static IEnumerable<TModel> Find<TModel>(Expression<Func<TModel, bool>> predicate)
        {
            using (var conn = ConnectionManager.Open())
            {
                var r = conn.Find(predicate);
                return r;
            }
        }

        public static bool Update<TModel>(TModel model)
            where TModel : class
        {
            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var r = conn.Update(model, trans);
                trans.Commit();
                return r;
            }
        }


        public static MessageRecorder<TModel> Load<TModel>(long id)
            where TModel : class
        {
            var r = new MessageRecorder<TModel>();

            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var model = conn.Get<TModel>(id);
                //var model = conn.Query<TModel>(
                //    $"SELECT * FROM `{NamingHelper.MapClassToTable(typeof(TModel).FullName)}` WHERE `id` = {id}")
                //    .FirstOrDefault();
                return r.SetValue(model);
            }
        }

        public static MessageRecorder<IList<TModel>> Find<TModel>(long[] ids)
            where TModel : class
        {
            var r = new MessageRecorder<IList<TModel>>();
            if (ids.IsNullOrEmpty())
                return r.Error("ids没有指定值");

            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var models = conn.Query<TModel>(
                    $"SELECT * FROM `{NamingHelper.MapClassToTable(typeof(TModel).FullName)}` WHERE `id` in ({string.Join(",", ids)})")
                    .ToList();
                if (models.IsNullOrEmpty())
                {
                    return r.Info("没有匹配的数据");
                }
                return r.SetValue(models);
            }
        }

        public static MessageRecorder<bool> Delete<TModel>(long[] ids)
            where TModel : class
        {
            var r = new MessageRecorder<bool>();
            if (ids.IsNullOrEmpty())
                return r.Error("ids没有指定值");

            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                
                var models = conn.Query<TModel>(
                    $"SELECT * FROM `{NamingHelper.MapClassToTable(typeof (TModel).FullName)}` WHERE `id` in ({string.Join(",", ids)})");
                if (models.IsNullOrEmpty())
                {
                    return r.SetValue(true).Info("已被删除");
                }

                trans.Commit();
                return r.SetValue(true).Info("删除成功");
            }
        }

        public static bool Delete<TModel>(TModel model)
            where TModel : class
        {
            Register<TModel>();

            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var r = conn.Delete(model, trans);
                trans.Commit();
                return r;
            }
        }

        public static bool Execute(string sql, object param = null)
        {
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var r = conn.Execute(sql, param);
                trans.Commit();
                return r > -1;
            }
        }

        #endregion
    }
}
