using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Modules.Annoation;
using Bm.Services.Base;
using Bm.Services.Common;

namespace Bm.Controllers.Base
{
    [ApiAuth]
    public class CodeController : ApiController
    {
        [Route("api/base_code")]
        public IHttpActionResult Get()
        {
            var mr = new MessageRecorder<string>();

            var m = Request.GetQueryString("m");
            var t = Request.GetQueryString("t");
            if ("register".Equals(t))
            {
                var accountService = new AccountService();
                var r1 = accountService.IsExists(m);
                if (r1.HasError) return Ok(this.Output(r1));
                if (r1.Value) return Ok(this.Output(mr.Error("账户已经存在")));

                var service = new VerificationCodeService();
                var r2 = service.MakeCode(AlidayuService.CodeType.Register, null, m);
                return Ok(this.Output(r2, n => n.Uuid));
            }
            if ("reset_password".Equals(t))
            {
                var accountService = new AccountService();
                var r1 = accountService.IsExists(m);
                if (r1.HasError) return Ok(this.Output(r1));
                if (!r1.Value) return Ok(this.Output(mr.Error("账户不存在")));

                var service = new VerificationCodeService();
                var r2 = service.MakeCode(AlidayuService.CodeType.ResetPass, null, m);
                return Ok(this.Output(r2, n => n.Uuid));
            }

            return Ok(this.Output(mr.Error("请求类型不正确")));
        }
    }
}
