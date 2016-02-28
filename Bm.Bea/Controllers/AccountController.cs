using System.Web.Http;
using System.Web.UI.WebControls;
using Bm.Extensions;
using Bm.Modules.Annoation;
using Bm.Services.Base;
using Bm.Services.Common;
using Bm.Services.Dp;

namespace Bm.Controllers
{
    [ApiAuth]
    public class AccountController : ApiController
    {
        /// <summary>
        /// 验证账户登录是否正确
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            var p = Request.GetQueryString("p");
            var service = new AccountService();
            var r = service.Auth(m, p);
            return Ok(this.Output(r, n => new { No = n?.No ?? "", Name = n?.Name ?? "" }));
        }

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Get(string id)
        {
            var m = Request.GetQueryString("m");
            var p = Request.GetQueryString("p");
            var uuid = Request.GetQueryString("u");
            var code = Request.GetQueryString("c");

            var service = new VerificationCodeService();
            var r = service.AuthCode(m, uuid, code);
            if (r.HasError) return Ok(this.Output(r));
            
            var service2 = new BrokerAccountService();
            var r2 = service2.Create(m, p);
            if (r2.HasError) return Ok(this.Output(r2));
            
            return Ok(this.Output(r2, n => new { n.No }));
        }
    }
}
