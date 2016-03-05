using System.Linq;
using System.Web.Http;
using System.Web.Razor.Generator;
using Bm.Extensions;
using Bm.Modules.Helper;
using Bm.Services.Dp;

namespace Bm.Controllers.House
{
    
    public class HouseController : BaseApiController
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
    }
}
