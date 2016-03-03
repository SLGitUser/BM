using System;
using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Modules.Annoation;
using Bm.Services.Dp;

namespace Bm.Controllers.Task
{
    [ApiAuth]
    public class HouseController : ApiController
    {

        /// <summary>
        /// 验证账户登录是否正确
        /// </summary>
        /// <returns></returns>
        [Route("api/get_house_all")]
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            var u = Request.GetQueryString("u");
            
            var service = new ProjectService();
            var r = service.GetAllHouse();
            return Ok(this.Output(r, n => new {n}));
        }
    }
}
