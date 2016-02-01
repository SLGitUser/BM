using System.Web.Mvc;

namespace Bm.Modules
{
    [Authorize]
    public class BaseAuthController : BaseController
    {
        /// <summary>
        /// 账户名
        /// </summary>
        public string CurrAccountNo { get; private set; }

        public BaseAuthController() : base()
        {
            CurrAccountNo = User?.Identity?.Name;
        }
    }
}