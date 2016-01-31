using System;
using System.Linq;
using System.Web.Mvc;
using Bm.Models.Dp;
using Bm.Modules;
using Bm.Services.Dp;
using com.senlang.Sdip.Util;

namespace Bm.Areas.Biz.Controllers
{
    public sealed class DeveloperController : BaseAuthController
    {
        private readonly DeveloperService _service;

        public DeveloperController()
        {
            _service = new DeveloperService(User?.Identity?.Name);
        }

        // GET: Biz/Developer
        public ActionResult Index()
        {
            var models = _service.GetAll();
            return View(models);
        }

        // GET: Biz/Developer/Details/5
        public ActionResult Details(long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                FlashWarn("请选择一条数据");
                return RedirectToAction("Index");
            }
            var list = _service.GetByIds(ids);
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
                    FlashError("数据验证未通过，请检查是否存在为空的必填项");
                    return View(model);
                }
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = "SYSTEM";

                var r = _service.Create(model);
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
        public ActionResult Edit(long? id)
        {
            if (id.HasValue)
            {
                var model = _service.GetById(id.Value);
                return View(model);
            }
            FlashWarn("没有指定ID");
            return RedirectToAction("Index");

        }
        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            var model = new Developer();
            TryUpdateModel(model, collection);
            if (!ModelState.IsValid)
            {
                FlashError("数据验证未通过，请检查是否存在为空的必填项");
                return View(model);
            }
            model.UpdatedBy = "SYSTEM";
            model.UpdatedAt = DateTime.Now;

            var r = _service.Update(model);
            if (r.HasError)
            {
                FlashMessage(r);
                return View(model);
            }
            return RedirectToAction("Index");
        }

        // GET: Biz/Developer/Delete/5
        public ActionResult Delete(long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                FlashWarn("请选择一条数据");
                return RedirectToAction("Index");
            }
            var list = _service.GetByIds(ids);
            return View(list);
        }

        // POST: Biz/Developer/Delete/5
        [HttpPost]
        public ActionResult Delete(FormCollection collection, long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                FlashWarn("请选择一条数据");
                return View();
            }
            var list = _service.GetByIds(ids);
            try
            {
                var r = _service.Delete(list.ToArray());
                if (r.HasError)
                {
                    FlashMessage(r);
                    return View(list);
                }
                FlashSuccess("删除成功");
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                FlashError("删除失败");
                return View(list);
            }
        }
    }
}
