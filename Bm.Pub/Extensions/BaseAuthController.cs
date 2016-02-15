using System.Web.Mvc;
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
        public string CurrAccountNo { get; private set; }
        
        public BaseAuthController() : base()
        {
            CurrAccountNo = User?.Identity?.Name;
            if (string.IsNullOrEmpty(CurrAccountNo))
            {
                CurrAccountNo = "__MISSING__";
            }
        }



    }
}