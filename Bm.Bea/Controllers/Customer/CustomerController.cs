using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using System;
using Bm.Modules.Helper;
using Bm.Services.Common;

namespace Bm.Controllers.Customer
{
    public class CustomerController : BaseApiController
    {
        private CustomerService _service;

        [Route("api/base_customer_details")]
        public IHttpActionResult GetAll()
        {
            var m = Request.GetQueryString("m");

            var mr = new MessageRecorder<bool>();
            //TODO 更新本周推客数量、活跃客户数、成交金额
            var now = DateTime.Now;
            var r2 = _service.GetById(int.Parse(m.ToString()));
            r2.Name = "张三";
            return Ok(mr, n => new
            {
                TuikeAmount = r2.Name,
                ActiveCustomer = now.Hour * 100 + +now.Minute * 2 + now.Second,
                TotalSales = now.Hour * 200 + +now.Minute * 50 + now.Second * 10,
                AlertCount = now.Minute % 10
            });
        }
        public IHttpActionResult GetCustomer()
        {
            
            var r = _service.GetAllCustomer();
            return Ok(r);
        }
    }
}
