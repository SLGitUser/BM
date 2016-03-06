using System.Linq;
using System.Web.Http;
using System.Web.Razor.Generator;
using Bm.Extensions;
using Bm.Models.Common;
using Bm.Models.Dp;
using Bm.Modules.Helper;
using Bm.Services.Dp;

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
    }
}
