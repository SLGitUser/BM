using System.Linq;
using System.Web.Http;
using System.Web.Razor.Generator;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Helper;
using Bm.Services.Dp;
using System.Collections.Generic;

namespace Bm.Controllers.Mine
{

    public class MineController : BaseApiController
    {
        /// <summary>
        /// 根据经纪人编号获取公司信息
        /// </summary>
        /// <returns></returns>
        [Route("api/get_mine_detail")]
        public IHttpActionResult GetMineById()
        {
            var u = Request.GetQueryString("u");

            var service = new BrokerService();
            var r = service.GetByNo(u);
            return Ok(r);
        }
        /// <summary>
        /// 获取所有公司信息
        /// </summary>
        /// <returns></returns>
        [Route("api/get_mine_firmall")]
        public IHttpActionResult GetFirmAll()
        {
            var r = new MessageRecorder<IList<BrokerageFirm>>();
            var u = Request.GetQueryString("u");
            var service = new BrokerageFirmService(u);
            r.SetValue(service.GetAll());
            return Ok(r);
        }
        /// <summary>
        /// 绑定公司
        /// </summary>
        /// <returns></returns>
        [Route("api/get_mine_save")]
        [HttpGet]
        public IHttpActionResult SetFirm()
        {
            var r = new MessageRecorder<IList<BrokerageFirm>>();
            var u = Request.GetQueryString("u");
            var n = Request.GetQueryString("n");
            var service = new BrokerService();
            var a = service.UpdateFirm(u,n);
            return Ok(a);
        }
    }
}
