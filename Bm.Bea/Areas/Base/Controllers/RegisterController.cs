using System.Linq;
using System.Web.Mvc;
using Bm.Extensions;
using Bm.Services.Common;
using Bm.Services.Dp;

namespace Bm.Areas.Base.Controllers
{
    public class RegisterController : BaseController
    {
        // GET: Base/Register
        public ActionResult Index()
        {
            return View();
        }

        
        public ActionResult Code()
        {
            var phoneNo = GetDbParas("phoneNo");
            if (string.IsNullOrEmpty(phoneNo))
            {
                return View("Index");
            }
            if (!phoneNo.All(char.IsDigit))
            {
                return View("Index");
            }
            var service = new VerificationCodeService();
            var r = service.MakeCode(AlidayuService.CodeType.Register, null, phoneNo);
            if (r.HasError)
            {
                return View("Index");
            }
            ViewData["uuid"] = r.Value.Uuid;
            return View();
        }

        
        public ActionResult Verify()
        {
            var uuid = GetDbParas("uuid");

            var phoneNo = GetDbParas("phoneNo");
            var codeConfirm = GetDbParas("codeConfirm");

            var password = GetDbParas("password");


            var service = new VerificationCodeService();
            var r = service.AuthCode(phoneNo, uuid, codeConfirm);
            if (r.HasError)
            {
                return View("Index");
            }
            var service2 = new BrokerAccountService();
            var r2 = service2.Create(phoneNo, password);
            if (r2.HasError)
            {
                return View("Index");
            }
            return RedirectToAction("Index", "Portal", new { Area = "Base" });
        }
    }
}