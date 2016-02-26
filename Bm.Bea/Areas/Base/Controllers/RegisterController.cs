using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bm.Services.Common;

namespace Bm.Areas.Base.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Base/Register
        public ActionResult Index()
        {
            return QrCodeService.QrCodeResult("hello");
            return View();
        }
    }
}