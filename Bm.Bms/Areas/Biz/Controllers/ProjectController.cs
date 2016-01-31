using Bm.Modules;
using Bm.Services.Dp;
using System.Web.Mvc;

namespace Bm.Areas.Biz.Controllers
{
    public sealed class ProjectController : BaseAuthController
    {
        private readonly ProjectService _service;

        public ProjectController()
        {
            _service = new ProjectService(User?.Identity?.Name);
        }

        // GET: Biz/Project
        public ActionResult Index()
        {
            var modes = _service.GetAll();
            return View(modes);
        }

        // GET: Biz/Project/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Biz/Project/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Biz/Project/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Biz/Project/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Biz/Project/Edit/5
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

        // GET: Biz/Project/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Biz/Project/Delete/5
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
