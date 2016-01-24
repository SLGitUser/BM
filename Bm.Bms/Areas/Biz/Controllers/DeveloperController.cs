using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        public ActionResult Details(int[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                ViewBag.Message = "删除失败";
                return RedirectToAction("Index");
            }
            var predicate = Predicates.Field<Developer>(f => f.Id, Operator.Eq, ids[0]);
            ViewBag.ids = ids;
            var list = new DbQuickService().SelectList<Developer>(predicate);
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Biz/Developer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Biz/Developer/Delete/5
        public ActionResult Delete(int[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                ViewBag.Message = "删除失败";
                return RedirectToAction("Index");
            }
            //var developerList = new List<Developer>();
            //var models = new Queue<Developer>();
            var predicate = Predicates.Field<Developer>(f => f.Id, Operator.Eq, ids[0]);
            ViewBag.ids = ids;
            var list = new DbQuickService().SelectList<Developer>(predicate);
            //foreach (var id in ids)
            //{
            //    developerList.Add(models.FirstOrDefault(m=>m.Id.Equals(id)));
            //}
            return View(list);
        }

        // POST: Biz/Developer/Delete/5
        [HttpPost]
        public ActionResult Delete(int[] ids, FormCollection collection)
        {
            if (ids.IsNullOrEmpty())
            {
                ViewBag.Message = "删除失败";
                return RedirectToAction("Index");
            }
            var predicate = Predicates.Field<Developer>(f => f.Id, Operator.Eq, ids[0]);
            var list = new DbQuickService().SelectList<Developer>(predicate);
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
