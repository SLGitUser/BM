using System.Web.Mvc;
using System.Web.Routing;
using Bm.Modules.Html;

namespace Bm.Extensions
{
    [TimingFilter]
    [Authorize]
    public class BaseAuthController : BaseController
    {
        /// <summary>
        /// 账户名
        /// </summary>
        protected string CurrAccountNo;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            CurrAccountNo = User?.Identity?.Name;
            if (string.IsNullOrEmpty(CurrAccountNo))
            {
                requestContext.HttpContext.Response.Redirect("/base/session/login");
            }
            else
            {
                //Logger.Debug(string.Concat("user ", CurrAccountNo, " visit ", Request.Path));
            }


        }



    }
}