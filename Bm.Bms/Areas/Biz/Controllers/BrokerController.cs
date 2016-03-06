using Bm.Modules;
using Bm.Services.Dp;
using System.Web.Mvc;
using Bm.Models.Dp;
using Bm.Modules.Helper;
using System;
using System.Linq;
using System.Web.Routing;
using Bm.Extensions;
using System.Web;
using System.IO;

namespace Bm.Areas.Biz.Controllers
{
    public sealed class BrokerController : BaseAuthController
    {
        private BrokerService _service;
        
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _service = new BrokerService(CurrAccountNo);
        }

        // GET: Biz/Broker
        public ActionResult Index()
        {
            var models = _service.GetAll();
            return View(models);
        }

        // GET: Biz/Broker/Details/5
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

        // GET: Biz/Broker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Biz/Broker/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new Broker();
                TryUpdateModel(model, collection);
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = "SYSTEM"; 
                if (!ModelState.IsValid)
                {
                    FlashError("数据验证未通过，请检查是否存在为空的必填项");
                    return View(model);
                }
                model.CreatedAt = DateTime.Now;
                model.CreatedBy = "SYSTEM";
                var service = new BrokerAccountService();
                var r = service.Create(model.Mobile, string.Concat(model.Mobile, "##"), model.CityNo, model.Name, model.FirmNo);
                
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

        // GET: Biz/Broker/Edit/5
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

        // POST: Biz/Broker/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var model = new Broker();
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

        // GET: Biz/Broker/Delete/5
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

        // POST: Biz/Broker/Delete/5
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
