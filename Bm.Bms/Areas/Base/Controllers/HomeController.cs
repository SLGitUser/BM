using System.Web.Mvc;
using Bm.Extensions;

namespace Bm.Areas.Base.Controllers
{
    public class HomeController : BaseAuthController
    {
        // GET: Base/Home
        public ActionResult Index()
        {
            return View("~/Areas/Base/Views/Home/Index.cshtml");
        }
    }
}