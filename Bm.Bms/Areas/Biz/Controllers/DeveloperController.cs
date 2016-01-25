using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Routing.Constraints;
using System.Web.Services.Description;
using Bm.Models.Dp;
using Bm.Modules;
using Bm.Services;
using Bm.Services.common;
using com.senlang.Sdip.Util;
using DapperExtensions;

namespace Bm.Areas.Biz.Controllers
{
    public sealed class DeveloperController : BaseAuthController
    {
        // GET: Biz/Developer
        public ActionResult Index()
        {
            var models = new DbQuickService().Query<Developer>("select * from developer");
            return View(models);
        }

        // GET: Biz/Developer/Details/5
        public ActionResult Details(long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                ViewBag.Message = "请选择一条数据";
                return View();
            }
            var sql = "select * from Developer where id in @Ids";
            var list = DbQuickService.SelectListFormSql<Developer>(sql, new {Ids=ids});
            return View(list);
        }

        // GET: Biz/Developer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Biz/Developer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new Developer();
                TryUpdateModel(model, collection);
                if (!ModelState.IsValid)
                {
                    ViewBag.ErrorInfo = "invalid";
                    return View(model);
                }
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = "SYSTEM";

                var r = new DbQuickService().Create(model);
                if (!r)
                {
                    ViewBag.ErrorInfo = "invalid";
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                return View();
            }
        }

        // GET: Biz/Developer/Edit/5
        public ActionResult Edit(long? id)
        {

            if (id!=null)
            {
                var predicate = Predicates.Field<Developer>(f => f.Id, Operator.Eq, id);
                var list = new DbQuickService().SelectList<Developer>(predicate);
                Developer develop = list[0];
                return View(develop);
            }
            else
            {
                ViewBag.Message = "修改失败";
                return RedirectToAction("Index");
            } 
        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var model = new Developer();
            TryUpdateModel(model, collection);
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorInfo = "invalid";
                return View(model);
            }
            model.UpdatedBy = "SYSTEM";

            bool result = new DbQuickService().Update<Developer>(model);
            if (result)
            { 
                return RedirectToAction("Index");
            }
            return View();
        }
     
        // GET: Biz/Developer/Delete/5
        public ActionResult Delete(long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                ViewBag.Message = "请选择一条数据";
                return View();
            }
            var sql = "select * from Developer where id in @Ids";
            var list = DbQuickService.SelectListFormSql<Developer>(sql, new { Ids = ids });
            return View(list);
        }

        // POST: Biz/Developer/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            var model = new List<Developer>();
            TryUpdateModel(model);
            var ids =model.Select(m => m.Id).ToArray();
            var sql = "select * from Developer where id in @Ids";
            var list = DbQuickService.SelectListFormSql<Developer>(sql, new { Ids = ids });
            try
            {
                var r = new DbQuickService().Delete<Developer>(ids);
                if (r)
                {
                    ViewBag.Message = "删除成功";
                    return RedirectToAction("Index");
                }
                ViewBag.Message = "删除失败";
                return View(list);
            }
            catch(Exception e)
            {
                ViewBag.Message = "删除失败";
                return View(list);
            }
        }
    }
}
