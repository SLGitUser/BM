using Bm.Modules;
using Bm.Services.Dp;
using System.Web.Mvc;
using Bm.Models.Dp;
using Bm.Modules.Helper;
using System;
using System.ComponentModel;
using System.Linq;
using System.Web.Routing;
using Bm.Extensions;
using Bm.Services.Common;

namespace Bm.Areas.Biz.Controllers
{
    [DisplayName("户型信息")]
    public sealed class DevRoomController : BaseAuthController
    {
        private DevRoomService _service;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _service = new DevRoomService(CurrAccountNo);
        }

        // GET: Biz/Project
        public ActionResult Index(string No)
        {
            var models = _service.GetAll(No);
            return View(models);
        }

        // GET: Biz/Project/Details/5
        public ActionResult Details(long[] ids)
        {
            if (ids.IsNullOrEmpty())
            {
                FlashWarn("请选择一条数据");
                return RedirectToAction("Index");
            }
            var list = _service.GetByIds(ids);;
            return View(list);
        }

        // GET: Biz/Project/Create
        public ActionResult Create(string DpNo)
        {
            var dev = new DevRoom{DpNo=DpNo };
            ViewData["No"] = DpNo;
            return View(dev);
        }

        // POST: Biz/Project/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new DevRoom();
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = "SYSTEM";
                TryUpdateModel(model, collection);
                if (!ModelState.IsValid)
                {
                    var a = GetModelErrorMsg();
                    FlashError("数据验证未通过，请检查是否存在为空的必填项");
                    return View(model);
                }
                
                var r = _service.Create(model);
                if (r.HasError)
                {
                    FlashMessage(r);
                    return View(model);
                }
                return RedirectToAction("Index",new { No=model.DpNo});
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Biz/Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id.HasValue)
            {
                var model = _service.GetById(id.Value);
                return View(model);
            }
            FlashWarn("没有指定ID");
            return RedirectToAction("Index");
        }

        // POST: Biz/Project/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = new DevRoom();
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
            return RedirectToAction("Index",new { No=model.DpNo});
        }

        // GET: Biz/Project/Delete/5
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

        // POST: Biz/Project/Delete/5
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
                return RedirectToAction("Index",new {No=list.First(m=>m.Id==ids[0]).DpNo});
            }
            catch (Exception e)
            {
                FlashError("删除失败");
                return View(list);
            }
        }
    }
}
