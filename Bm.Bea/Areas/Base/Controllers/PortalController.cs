using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bm.Areas.Base.Controllers
{
    public class PortalController : Controller
    {
        // GET: Base/Portal
        public ActionResult Index()
        {
            return View();
        }
    }
}