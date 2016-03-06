using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Bm.Extensions;
using Bm.Modules;
using Bm.Services.Base;
using com.senlang.Sdip.Util;

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
            //return JsonMessage(200, "验证成功，请稍候...", new { url = "/base/home/index" });

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
                Logger.Warn($"user {username} login failed");
                return JsonError("用户名或密码错误");
            }

            if (!r.Value.RoleRefs.Any(m => m.RoleNo.StartsWith("Branch")))
            {
                Logger.Warn($"user {r.Value.Name}/{r.Value.No.Right(5)} login forbidden");
                return JsonError("角色类型不正确，拒绝登录");
            }

            Logger.Info($"user {r.Value.Name}/{r.Value.No.Right(5)} login success");

            FormsAuthentication.SetAuthCookie(r.Value.No, true);

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
            Logger.Info($"user /{User?.Identity?.Name?.Right(5)} logout success");
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