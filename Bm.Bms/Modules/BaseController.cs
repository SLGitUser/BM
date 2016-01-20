using System.Web.Mvc;

namespace Bm.Modules
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 获得参数值
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public string GetParas(string para)
        {
            return string.IsNullOrWhiteSpace(para) ? null : Request.Params[para];
        }

        /// <summary>
        /// 获得参数值
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public string GetDbParas(string para)
        {
            var value = GetParas(para);
            return string.IsNullOrWhiteSpace(value) ? null : value.Replace("'", "''");
        }
    }
}