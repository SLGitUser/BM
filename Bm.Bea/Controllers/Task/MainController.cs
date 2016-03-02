using System;
using System.Web.Http;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Modules.Annoation;

namespace Bm.Controllers.Task
{
    [ApiAuth]
    public class MainController : ApiController
    {

        /// <summary>
        /// 验证账户登录是否正确
        /// </summary>
        /// <returns></returns>
        [Route("api/task_main_count")]
        public IHttpActionResult Get()
        {
            var m = Request.GetQueryString("m");
            
            var mr = new MessageRecorder<bool>();
            //TODO 更新本周推客数量、活跃客户数、成交金额
            var now = DateTime.Now;
            return Ok(this.Output(mr, n => new
            {
                TuikeAmount = 0 + now.Hour * 100 + now.Minute,
                ActiveCustomer = now.Hour * 100 + + now.Minute * 2 + now.Second,
                TotalSales = now.Hour * 200 + +now.Minute * 50 + now.Second * 10,
                AlertCount = now.Minute % 10
            }));
        }
    }
}
