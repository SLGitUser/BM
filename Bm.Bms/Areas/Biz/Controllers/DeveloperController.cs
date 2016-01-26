using System;
using System.Web.Mvc;
using Bm.Models.Dp;
using Bm.Modules;
using Bm.Modules.Helper;
using Bm.Services.Common;
using Bm.Services.Dp;

namespace Bm.Areas.Biz.Controllers
{
    public sealed class DeveloperController : BaseAuthController
    {

        private readonly DeveloperService _developerService;

        public DeveloperController()
        {
            _developerService = new DeveloperService(User?.Identity?.Name);
        }

        // GET: Biz/Developer
        public ActionResult Index()
        {
            var models = _developerService.GetAll();
            return View(models);
        }

        // GET: Biz/Developer/Details/5
        public ActionResult Details(int id)
        {
            var model = _developerService.GetById(id);
            return View(model);
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
                    FlashError("模型校验失败");
                    return View(model);
                }
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = "SYSTEM";

                var r = _developerService.Create(model);
                if (r.HasError)
                {
                    FlashMessage(r);
                    return View(model);

                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Biz/Developer/Edit/5
        public ActionResult Edit(int id)
        {
            var model = _developerService.GetById(id);
            return View(model);
        }

        // POST: Biz/Developer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                var model = new Developer();
                TryUpdateModel(model, collection);
                if (!ModelState.IsValid)
                {
                    FlashError("模型校验失败");
                    return View(model);
                }
                model.UpdatedAt = DateTime.Now;
                model.UpdatedBy = "SYSTEM";

                _developerService.Update(model);
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Biz/Developer/Delete/5
        public ActionResult Delete(long[] ids)
        {
            if (ids.IsNullOrEmpty()) return RedirectToAction("Index");

            var models = _developerService.GetByIds(ids);
            return View(models);
        }

        // POST: Biz/Developer/Delete/5
        [HttpPost]
        public ActionResult Delete(long[] ids, FormCollection collection)
        {
            try
            {
                if (ids.IsNullOrEmpty()) return RedirectToAction("Index");

                var r = BaseDbService.Delete<Developer>(ids);

                if (r.HasError)
                {

                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
