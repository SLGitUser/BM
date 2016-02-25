using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using log4net;

namespace Bm.Modules.Orm
{
    public static class ConnExtension
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConnExtension));

        /// <summary>
        /// 查询单值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="conn"></param>
        /// <param name="sql"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static TModel ExecuteScalarEx<TModel>(this IDbConnection conn, string sql, IDbTransaction trans = null)
        {
            return conn.Query<TModel>(sql, transaction: trans).FirstOrDefault();
        }

        /// <summary>
        /// 是否查询存在
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="conn"></param>
        /// <param name="query"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool Exists<TModel>(this IDbConnection conn, Criteria<TModel> query, IDbTransaction trans = null)
        {
            var sql = query.Limit(1).ToSelectSql();
            Log.Debug(string.Concat("sql: ", sql));
            return conn.Query(sql, transaction:trans).Any();
        }

        /// <summary>
        /// 查询对象集合
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="conn"></param>
        /// <param name="query"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static IList<TModel> Query<TModel>(this IDbConnection conn, Criteria<TModel> query, IDbTransaction trans = null)
        {
            var sql = query.ToSelectSql();
            Log.Debug(string.Concat("sql: ", sql));
            return conn.Query<TModel>(sql, transaction: trans).ToList();
        }

        /// <summary>
        /// 查询对象单值
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="conn"></param>
        /// <param name="query"></param>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static TModel Get<TModel>(this IDbConnection conn, Criteria<TModel> query, IDbTransaction trans = null)
        {
            var sql = query.Limit(1).ToSelectSql();
            Log.Debug(string.Concat("sql: ", sql));
            return conn.Query<TModel>(sql, transaction: trans).FirstOrDefault();
        }
    }
}
