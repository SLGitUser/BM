using System.Web.Mvc;
using System.Web.Security;
using Bm.Extensions;
using Bm.Services.Dp;

namespace Bm.Areas.Base.Controllers
{
    public class SessionController : BaseController
    {
        // GET: Base/Session
        public ActionResult Login()
        {
            return View();
        }

        // GET: Base/Session
        public ActionResult Logout()
        {
            Reset();
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            Reset();

            var phoneNo = GetDbParas("phoneNo");
            var password = GetDbParas("password");
            
            var service = new BrokerAccountService();
            var r = service.Auth(phoneNo, password);
            if (r.HasError)
            {
                return View();
            }
            FormsAuthentication.SetAuthCookie(r.Value.No, true);
            return View();
        }


        private void Reset()
        {
            Request.Cookies.Clear();
            Response.Cookies.Clear();
            FormsAuthentication.SignOut();
            Session.Clear();
        }
    }

}