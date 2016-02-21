using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bm.Modules.Orm;
using com.senlang.Sdip.Util;
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
        /// <param name="name">要显示的名称</param>
        /// <returns></returns>
        public SelectList GetDataByTable(string tableName,string no="",string name="")
        {
            using (var conn = ConnectionManager.Open())
            {
                string sql = $"select {no},{name} from {tableName}";
                var data = conn.Query(sql).ToList();
                return new SelectList(data,no,$"[{no}]+{name}");
            }
        }
    }
}