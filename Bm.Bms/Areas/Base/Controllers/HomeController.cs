using System.Web.Mvc;
using Bm.Modules;
using Bm.Services.Base;

namespace Bm.Areas.Base.Controllers
{
    public class HomeController : BaseAuthController
    {
        // GET: Base/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}