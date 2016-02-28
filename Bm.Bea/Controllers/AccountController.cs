using System.Web.Http;
using Bm.Extensions;
using Bm.Modules.Annoation;
using Bm.Services.Base;

namespace Bm.Controllers
{
    [ApiAuth]
    public class AccountController : ApiController
    {
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
