using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Bm.Models.Base;
using Bm.Modules.Helper;
using Bm.Services.Common;


namespace Bm.Dev.Controllers
{
    public class HomeController : Controller
    {
        [OutputCache(Duration = 6000)]
        public ActionResult Index()
        {
            return View();
        }

        #region 同步模型到数据库

        public string packageName = "Bm.Models";

        public ActionResult SyncModelToDb()
        {
            var r = DbModelService.GetClass();
            ViewData["modelNames"] = r.Item2;
            ViewData["existModelNames"] = r.Item1;

            return View();
        }

        [HttpPost]
        public ActionResult SyncModelToDb(FormCollection collection)
        {
            var modelName = Request.Params["modelName"];
            //Log.Debug("同步模型 {0} 到数据库", modelName);
            var modelNameList = modelName.ToStringArray();

            var assembly = typeof(Account).Assembly;
            var genTypes = new List<string>();
            foreach (var type in assembly.GetExportedTypes())
            {
                var fullName = type.FullName;
                if (!fullName.Contains(packageName)) continue;
                if (!modelNameList.Contains(fullName)) continue;
                DbModelService.SyncModelToDb(type);
                genTypes.Add(fullName);
            }
            if (genTypes.Any())
            {
                ViewBag.Message = $"同步成功！更新模型{genTypes.Count}个，包括：{string.Join(",", genTypes)}";
            }
            else
            {
                ViewBag.Message = "没有找到匹配的模型";
            }
            return RedirectToAction("Index");
        }

        #endregion

        #region 清理缓存

        public ActionResult ClearCache()
        {
            foreach (DictionaryEntry dict in HttpContext.Cache)
            {
                HttpContext.Cache.Remove((string)dict.Key);
            }
            return RedirectToAction("Index");
        }

        #endregion
    }
}