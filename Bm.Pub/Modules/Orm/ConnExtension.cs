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
        /// 是否查询存在
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="conn"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static bool Exists<TModel>(this IDbConnection conn, Criteria<TModel> query)
        {
            var sql = query.Limit(1).ToSelectSql();
            Log.Debug(string.Concat("SQL: ", sql));
            return conn.Query(sql).Any();
        }

        /// <summary>
        /// 是否查询存在
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="conn"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IList<TModel> Query<TModel>(this IDbConnection conn, Criteria<TModel> query)
        {
            var sql = query.ToSelectSql();
            Log.Debug(string.Concat("SQL: ", sql));
            return conn.Query<TModel>(sql).ToList();
        }
    }
}
