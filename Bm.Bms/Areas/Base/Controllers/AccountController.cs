using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using Bm.Extensions;
using Bm.Models.Base;
using Bm.Modules;
using Bm.Services.Base;
using Bm.Services.Dp;
using com.senlang.Sdip.Util;

namespace Bm.Areas.Base.Controllers
{
    public sealed class AccountController : BaseAuthController
    {
        private AccountService _service;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            _service = new AccountService(CurrAccountNo);
        }
        
        // GET: Biz/Account
        public ActionResult Index()
        {
            var models = _service.GetAll();
            return View(models);
        }

        // GET: Biz/Account/Details/5
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

        // GET: Biz/Account/Create
        public ActionResult Create()
        {
            var model = new Account
            {
                No = Guid.NewGuid().ToString("N").ToUpper()
            };
            return View(model);
        }

        // POST: Biz/Account/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                var model = new Account();
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

        // GET: Biz/Account/Edit/5
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
            var model = new Account();
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

        // GET: Biz/Account/Delete/5
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

        // POST: Biz/Account/Delete/5
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
