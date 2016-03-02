using System.Web.Http;
using Bm.Extensions;
using Bm.Modules.Annoation;
using Bm.Services.Base;

namespace Bm.Controllers.Base
{
    [ApiAuth]
    public class AccountAuthController : ApiController
    {

        /// <summary>
        /// 验证账户登录是否正确
        /// </summary>
        /// <returns></returns>
        [Route("api/base_account_auth")]
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            var p = Request.GetQueryString("p");
            var service = new AccountService();
            var r = service.Auth(m, p);
            return Ok(this.Output(r, n => new { No = n?.No ?? "", Name = n?.Name ?? "" }));
        }
    }
}
