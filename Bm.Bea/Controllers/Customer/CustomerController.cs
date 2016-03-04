using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using System;
using Bm.Modules.Helper;

namespace Bm.Controllers.Customer
{
    public class CustomerController : BaseApiController
    {

        [Route("api/base_Customer")]
        public IHttpActionResult Action()
        {
            var m = Request.GetQueryString("m");

            var mr = new MessageRecorder<bool>();
            //TODO 更新本周推客数量、活跃客户数、成交金额
            var now = DateTime.Now;
            return Ok(mr, n => new
            {
                TuikeAmount = 0 + now.Hour * 100 + now.Minute,
                ActiveCustomer = now.Hour * 100 + +now.Minute * 2 + now.Second,
                TotalSales = now.Hour * 200 + +now.Minute * 50 + now.Second * 10,
                AlertCount = now.Minute % 10
            });
        }
    }
}
