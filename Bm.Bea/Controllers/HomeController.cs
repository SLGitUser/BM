using System.Web.Mvc;

namespace Bm.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Content("This is Bm.Bea API Service site");
        }
    }
}