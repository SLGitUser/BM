//using System;
//using System.Linq;
//using System.Collections.Concurrent;
//using System.Collections.Generic;
//using System.Data;
//using System.Reflection;
//using Bm.Modules.Orm.Adapter;
//
//using Dapper;

//namespace Bm.Modules.Orm
//{
//    /// <summary>
//    /// 生のSQLを書くことなくクエリ発行できるようにするユーティリティクラス
//    /// </summary>
//    public static class SqlExtension
//    {
//        static readonly ISqlAdapter Adapter;

//        static readonly ConcurrentDictionary<RuntimeTypeHandle, string> TypeTableName = new ConcurrentDictionary<RuntimeTypeHandle, string>();

//        static ConcurrentDictionary<Type, PropertyInfo[]> paramNameCache = new ConcurrentDictionary<Type, PropertyInfo[]>();

//        static SqlExtension()
//        {
//            Adapter = new MySqlAdapter();
//        }

//        /// <summary>
//        /// implementation from Dapper.Contrib
//        /// キャッシュしていない状態だと、実行時間はローカルPCで50μs程度
//        /// シャーディングなどでテーブル名を変更する必要がある場合は更に上の層でコントロールする
//        /// </summary>
//        static string GetTableName(Type type)
//        {
//            string name;

//            // キャッシュを確認
//            if (TypeTableName.TryGetValue(type.TypeHandle, out name)) return name;

//            //NOTE: This as dynamic trick should be able to handle both our own Table-attribute as well as the one in EntityFramework
//            var tableAttr = type
//                .GetCustomAttributes(false)
//                .SingleOrDefault(attr => attr.GetType().Name == "TableAttribute") as dynamic;

//            // アトリビュートが存在する場合はそれを用いて、存在しない場合はデフォルトの補完
//            name = tableAttr != null ? tableAttr.Name : NamingHelper.MapClassToTable(type.Name);

//            TypeTableName[type.TypeHandle] = name;
//            return name;
//        }



//        /// <summary>
//        /// implementation from Dapper.Rainbow
//        /// </summary>
//        static List<string> GetParamNames(object o)
//        {
//            // 使うならコメントアウト外す
//            //var parameters = o as DynamicParameters;
//            //if (parameters != null)
//            //{
//            //    return parameters.ParameterNames.ToList();
//            //}

//            // キャッシュからクラス情報の取得を試みる
//            PropertyInfo[] propertyInfo;
//            if (!paramNameCache.TryGetValue(o.GetType(), out propertyInfo))
//            {
//                propertyInfo = o.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
//                paramNameCache[o.GetType()] = propertyInfo;
//            }

//            List<string> paramNames = new List<string>();
//            foreach (var prop in propertyInfo)
//            {
//                if (prop.PropertyType.IsClass)
//                {
//                    if(prop.PropertyType != typeof(string)) continue;
//                }
                
//                if(prop.GetSetMethod() == null) continue;

//                // 使うならコメントアウト外す
//                var tableAttr = prop
//                .GetCustomAttributes(false)
//                .SingleOrDefault(attr => attr.GetType().Name == "IgnoreMappingAttribute") as dynamic;

//                if(tableAttr == null) continue;
                
//                paramNames.Add(prop.Name);
//            }
//            return paramNames;
//        }


//        // TODO: 2015/12/11 フィールドを限定してレコード取得。transaction。commandTimeout。


//        /// <summary>
//        /// Grab a record with where clause from the DB 
//        /// </summary>
//        public static T Select<T>(this IDbConnection connection, T where)
//        {
//            return (All<T>(connection, where)).FirstOrDefault();
//        }

//        /// <summary>
//        /// Grab records with where clause from the DB 
//        /// </summary>
//        public static IEnumerable<T> SelectAll<T>(this IDbConnection connection, T where)
//        {
//            return All<T>(connection, where);
//        }

//        /// <summary>
//        /// 指定したentityでUpdateをかける。
//        /// TODO SETとWHEREでカラムがかぶる場合どうする？Dapper.Contrib参照
//        /// </summary>
//        public static bool Update<T>(this IDbConnection connection, T entity, T where) where T : class
//        {
//            var tableName = GetTableName(typeof(T));

//            // SETするパラメータのSQLをビルド
//            var setParams = GetParamNames(entity);
//            var setSql = string.Join(", ", setParams.Select(p => "`" + p + "` = @" + p));

//            // WHERE句のSQLをビルド
//            var whereParams = GetParamNames(where);
//            var whereSql = string.Join(" AND ", setParams.Select(p => "`" + p + "` = @" + p));

//            var sql = $"UPDATE {tableName} SET {setSql} WHERE {whereSql}";
//            return connection.Execute(sql, where) > 0;
//        }


//        /// <summary>
//        /// Dapper.Contrib
//        /// https://github.com/StackExchange/dapper-dot-net/blob/master/Dapper.Contrib/SqlMapperExtensions.cs#L292
//        /// </summary>
//        public static int Insert<T>(this IDbConnection connection, T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null) where T : class
//        {
//            var tableName = GetTableName(typeof(T));
//            var isList = false;
//            var type = typeof(T);

//            if (type.IsArray || type.IsGenericType)
//            {
//                isList = true;
//            }

//            var paramNames = GetParamNames(entityToInsert);
//            // カラム部分のSQL
//            var columnSql = string.Join(", ", paramNames.Select(p => p));
//            // VALUES部分のSQL
//            var valuesSql = string.Join(", ", paramNames.Select(p => "@" + p));

//            int returnVal;

//            if (!isList)
//            {
//                //single entity (get last inserted id)
//                returnVal = Adapter.Insert(connection, transaction, commandTimeout, tableName,
//                    columnSql, valuesSql, entityToInsert);
//            }
//            else
//            {
//                //insert list of entities
//                var cmd = $"INSERT INTO {tableName} ({columnSql}) VALUES ({valuesSql})";
//                returnVal = connection.Execute(cmd, entityToInsert, transaction, commandTimeout);
//            }

//            return returnVal;
//        }

//        /// <summary>
//        /// Return All record
//        /// </summary>
//        static IEnumerable<T> All<T>(this IDbConnection connection, object where = null)
//        {
//            var sql = "SELECT * FROM " + GetTableName(typeof(T));
//            if (where == null) return connection.Query<T>(sql);
//            var paramNames = GetParamNames(where);
//            var w = string.Join(" AND ", paramNames.Select(p => "`" + p + "` = @" + p));
//            return connection.Query<T>(sql + " WHERE " + w, where);
//        }
//    }
//}