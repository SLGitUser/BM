using System;
using System.Linq;
using System.Net.Http;
using System.ServiceModel.Channels;
using System.Web;

namespace Bm.Modules.Helper
{
    public static class RequestHelper
    {

        /// <summary>
        /// 是否调试模式
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsDebug(this HttpRequestMessage request)
        {
            return "yes".Equals(request.GetQueryString("debug"));
        }
        
        ///// <summary>
        ///// 是否本机访问
        ///// </summary>
        ///// <param name="request"></param>
        ///// <returns></returns>
        //public static bool IsLocal(this HttpRequestMessage request)
        //{
        //    var ip = request.GetClientIp();
        //    if (string.IsNullOrEmpty(ip)) return false;
        //    return new[] {"::1"}.Contains(ip);
        //}

        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="request"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQueryString(this HttpRequestMessage request, string key)
        {
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = request.GetQueryNameValuePairs();
            if (queryStrings == null)
                return null;

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, StringComparison.OrdinalIgnoreCase) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }

        /// <summary>
        /// 获取访问路径
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetPath(this HttpRequestMessage request)
        {
            return request.GetRequestContext().VirtualPathRoot;
        }

        /// <summary>
        /// 获取客户端IP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string GetClientIp(this HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                RemoteEndpointMessageProperty prop;
                prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
                return prop.Address;
            }

            return null;
        }
    }
}
