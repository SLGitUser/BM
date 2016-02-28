using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Modules.Annoation;
using Bm.Services.Common;

namespace Bm.Controllers
{
    [ApiAuth]
    public class CodeController : ApiController
    {
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            var t = Request.GetQueryString("t");
            if ("register".Equals(t))
            {
                var service = new VerificationCodeService();
                var r = service.MakeCode(AlidayuService.CodeType.Register, null, m);
                return Ok(this.Output(r, n => n.Uuid));
            }
            var mr = new MessageRecorder<string>();
            return Ok(this.Output(mr.Error("请求类型不正确")));
        }
    }
}
