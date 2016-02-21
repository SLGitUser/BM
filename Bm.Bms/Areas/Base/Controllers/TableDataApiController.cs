using System.Web.Mvc;
using Bm.Modules.Orm;
using Dapper;

namespace Bm.Areas.Base.Controllers
{
    public class TableDataApiController : Controller
    {
        /// <summary>
        /// 读取制定表中一条数据
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="no">要显示的编号</param>
        /// <param name="noVal">编号的值</param>
        /// <param name="name">要显示的名称</param>
        /// <returns></returns>
        public object GetDataByTable(string tableName, string no = "", string noVal = "", string name = "")
        {
            using (var conn = ConnectionManager.Open())
            {
                string sql = $"select {name} from {tableName} where {no} = '{noVal}'";
                var datax = conn.ExecuteScalar<string>(name)(sql);
                //var data = conn.Query<string>(name);
                //TODO 获取查询出的值

                return data;
            }
        }
    }
}