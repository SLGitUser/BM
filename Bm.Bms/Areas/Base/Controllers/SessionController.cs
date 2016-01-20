using System.Web.Mvc;
using System.Web.Security;
using Bm.Modules;

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

            var name = GetDbParas("username");
            var pwd = GetDbParas("password");

            //var result = LoginService.Auth(name, pwd);
            //if (result.HasError)
            //{
            //    return JsonError(string.Join(",", result.Errors.Select(m => m.Content)));
            //}
            //if (result.Value == null)
            //{
            //    return JsonError(string.Join(",", result.Messages.Select(m => m.Content)));
            //}

            FormsAuthentication.SetAuthCookie(name, false);

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
            //return JsonMessage(200, "验证成功，请稍候...", new { url });
            return Redirect("/base/home/index");
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