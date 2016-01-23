using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bm.Models.Dp;
using Bm.Modules;
using Bm.Services;
using Bm.Services.common;

namespace Bm.Areas.Biz.Controllers
{
    public sealed class DeveloperController : BaseAuthController
    {
        // GET: Biz/Developer
        public ActionResult Index()
        {
            var models = new DbQuickService().Query<Developer>("select * from dp_developer");
            return View(models);
        }

        // GET: Biz/Developer/Details/5
        public ActionResult Details(int id)
        {
            var models = new DbQuickService().Query<Developer>("select * from dp_developer");
            return View(models);
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
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Biz/Developer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
