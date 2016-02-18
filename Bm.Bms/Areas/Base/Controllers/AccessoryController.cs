using System.Linq;
using System.Web.Mvc;
using Bm.Extensions;
using Bm.Services.Common;

namespace Bm.Areas.Base.Controllers
{

    public class AccessoryController : BaseAuthController
    {
        [HttpPost]
        public JsonResult Upload()
        {
            var mr = AccessoryService.Upload(Request);
            var urls = mr.Value.Select(m => AccessoryService.GetUrl(m));
            return Json(string.Join(",", urls));
        }
    }
}