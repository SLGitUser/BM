using System.Web.Http;
using Bm.Extensions;
using Bm.Modules.Annoation;
using Bm.Services.Base;
using Bm.Services.Common;

namespace Bm.Controllers.Base
{
    [ApiAuth]
    public class AccountResetPasswordController : ApiController
    {

        /// <summary>
        /// 验证账户登录是否正确
        /// </summary>
        /// <returns></returns>
        [Route("api/base_account_reset_password")]
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            var p = Request.GetQueryString("p");
            var uuid = Request.GetQueryString("u");
            var code = Request.GetQueryString("c");

            // 先检查验证码是否正确
            var service = new VerificationCodeService();
            var r = service.AuthCode(m, uuid, code);
            if (r.HasError) return Ok(this.Output(r));

            // 更新密码
            var service2 = new AccountService();
            var r2 = service2.UpdatePassword(m, p);
            if (r2.HasError) return Ok(this.Output(r2));

            return Ok(this.Output(r2));
        }
    }
}
