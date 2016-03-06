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
        /// 获取所有房源信息
        /// </summary>
        /// <returns></returns>
        [Route("api/get_house_all")]
        public IHttpActionResult GetHouseAll()
        {
            var service = new ProjectService();
            var r = service.GetAllHouse();
            return Ok(r);
        }
        /// <summary>
        /// 获取我的房源信息
        /// </summary>
        /// <returns></returns>
        [Route("api/get_house_my")]
        public IHttpActionResult GetHouseMy()
        {
            //var m = Request.GetQueryString("m");
            var u = Request.GetQueryString("u");

            var service = new HouseBrokerRefService();
            var r = service.GetHouseByBrokerNo(u);
            return Ok(r);
        }
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
