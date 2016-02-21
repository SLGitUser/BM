using System.Web.Mvc;
using Bm.Modules.Orm;

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
        public ActionResult GetDataByTable(string tableName, string no = "", string noVal = "", string name = "")
        {
            string sql = $"select {name} from {tableName} where {no} = '{noVal}'";
            var result = ConnectionManager.ExecuteScalar<string>(sql);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}