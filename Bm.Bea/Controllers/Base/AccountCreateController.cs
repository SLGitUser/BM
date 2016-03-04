using System.Web.Http;
using Bm.Extensions;
using Bm.Modules.Helper;
using Bm.Services.Common;
using Bm.Services.Dp;

namespace Bm.Controllers.Base
{
    public class AccountCreateController : BaseApiController
    {
        /// <summary>
        /// 创建账户
        /// </summary>
        /// <returns></returns>
        [Route("api/base_account_create")]
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            var p = Request.GetQueryString("p");
            var uuid = Request.GetQueryString("u");
            var code = Request.GetQueryString("c");

            // 先检查验证码是否正确
            var service = new VerificationCodeService();
            var r = service.AuthCode(m, uuid, code);
            if (r.HasError) return Ok(r);
            
            // 创建经纪人
            var service2 = new BrokerAccountService();
            var r2 = service2.Create(m, p);
            if (r2.HasError) return Ok(r2);
            
            return Ok(r2, n => new { n.No });
        }
    }
}
