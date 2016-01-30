using System.Web.Mvc;
using System.Web.Security;
using Bm.Modules;
using Bm.Services.Base;

namespace Bm.Areas.Base.Controllers
{
    public class SessionController : BaseController
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            Reset();
            FormsAuthentication.SetAuthCookie("SYSTEM", false);

            return JsonMessage(200, "验证成功，请稍候...", new { url = "/base/home/index" });

            var username = GetDbParas("username");
            var password = GetDbParas("password");
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return JsonError("请输入用户名密码");
            }

            var service = new AccountService();
            var r = service.Auth(username, password);
            if (r.HasError)
            {
                FlashMessage(r);
                return JsonError("用户名或密码错误");
            }

            FormsAuthentication.SetAuthCookie(r.Value.No, false);

            //// 未初始化的用户，使用验证码登录
            //if (AccountStatus.Type.Inactive.Equals(result.Value.Status))
            //{
            //    var resetPasswordUrl = "/base/profile/resetpassword?ReturnUrl=" + Request.Params["ReturnUrl"];
            //    WriteSuccessLog("初次登录，账户初始化！");
            //    return JsonMessage(200, "账户初始化，请稍候...", new { url = resetPasswordUrl });
            //}

            //WriteSuccessLog("登录", result.Value.Name);

            var url = Request.Params["ReturnUrl"];
            if (string.IsNullOrEmpty(url))
            {
                url = FormsAuthentication.DefaultUrl;
            }
            return JsonMessage(200, "验证成功，请稍候...", new { url });
        }

        public ActionResult Logout()
        {
            Reset();
            return RedirectToAction("Login");
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