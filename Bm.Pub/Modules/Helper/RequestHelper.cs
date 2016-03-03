using System;
using System.Linq;
using System.Net.Http;

namespace Bm.Modules.Helper
{
    public static class RequestHelper
    {
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
    }
}
