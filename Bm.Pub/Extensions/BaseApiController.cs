using System;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Bm.Models.Common;
using Bm.Modules.Annoation;

namespace Bm.Extensions
{
    /// <summary>
    /// 所有api的父类
    /// </summary>
    [ApiAuth]
    public abstract class BaseApiController : ApiController
    {
        /// <summary>
        /// 获得字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetQueryString(string key)
        {
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = Request.GetQueryNameValuePairs();
            if (queryStrings == null)
                return null;

            var match = queryStrings.FirstOrDefault(kv => string.Compare(kv.Key, key, StringComparison.OrdinalIgnoreCase) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }

        /// <summary>
        /// 返回成功结果
        /// </summary>
        /// <typeparam name="TModel">模型泛型</typeparam>
        /// <param name="model">消息记录模型实例</param>
        /// <param name="func">消息记录模型转换函数</param>
        /// <returns></returns>
        public IHttpActionResult Ok<TModel>(MessageRecorder<TModel> model, Func<TModel, object> func = null)
        {
            var obj = new 
            {
                model.HasError,
                Errors = model.Errors.Select(m => m.Message).ToList(),
                Model = model.Value == null ? null : func?.Invoke(model.Value) ?? model.Value
            };
            return Ok(obj);
        }
    }
}
