using System.Web.Mvc;
using Bm.Modules;

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