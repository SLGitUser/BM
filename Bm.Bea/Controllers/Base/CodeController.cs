using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Modules.Helper;
using Bm.Services.Base;
using Bm.Services.Common;

namespace Bm.Controllers.Base
{
    public class CodeController : BaseApiController
    {
        [Route("api/base_code")]
        public IHttpActionResult Get()
        {

            var m = Request.GetQueryString("m");
            var t = Request.GetQueryString("t");
            if ("register".Equals(t))
            {
                var accountService = new AccountService();
                var r1 = accountService.IsExists(m);
                if (r1.HasError) return Ok(r1);
                if (r1.Value) return Ok(r1.Error("账户已经存在"));

                var service = new VerificationCodeService();
                var r2 = service.MakeCode(AlidayuService.CodeType.Register, null, m);
                return Ok(r2, n => n.Uuid);
            }
            if ("reset_password".Equals(t))
            {
                var accountService = new AccountService();
                var r1 = accountService.IsExists(m);
                if (r1.HasError) return Ok(r1);
                if (!r1.Value) return Ok(r1.Error("账户不存在"));

                var service = new VerificationCodeService();
                var r2 = service.MakeCode(AlidayuService.CodeType.ResetPass, null, m);
                return Ok(r2, n => n.Uuid);
            }

            var mr = new MessageRecorder<string>();
            return Ok(mr.Error("请求类型不正确"));
        }
    }
}
