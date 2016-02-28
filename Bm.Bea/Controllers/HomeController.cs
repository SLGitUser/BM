using System.Web.Mvc;

namespace Bm.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Content("This is Bm.Bea API Service site");
            //if (User.Identity != null && User.Identity.IsAuthenticated)
            //    return RedirectToAction("Index", "Portal", new {Area = "Base"});

            //return RedirectToAction("Login", "Session", new { Area = "Base" });
        }
    }
}