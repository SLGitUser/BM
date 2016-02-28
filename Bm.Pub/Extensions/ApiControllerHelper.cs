using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Http;
using Bm.Models.Common;
using Newtonsoft.Json;

namespace Bm.Extensions
{
    public static class ApiControllerHelper
    {
        /// <summary>
        /// 将带有日志的结果转换为正常结果输出
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <param name="model"></param>
        /// <param name="controller"></param>
        /// <returns></returns>
        public static MessageRecordOutputModel Output<TModel>(this ApiController controller, MessageRecorder<TModel> model, Func<TModel, object> func = null)
        {
            var obj = new MessageRecordOutputModel
            {
                HasError = model.HasError,
                Errors = model.Messages.Where(m => m.Type.Equals(MessageType.Error) || m.Type.Equals(MessageType.Fatal)).Select(m => m.Message).ToList(),
                Model = model.Value == null ? null : func?.Invoke(model.Value) ?? model.Value
            };

            return obj;
        }

        public class MessageRecordOutputModel
        {
            public bool HasError { get; set; }

            public IList<string> Errors { get; set; }

            public object Model { get; set; }
        }

        public static string GetQueryString(this HttpRequestMessage request, string key)
        {
            // IEnumerable<KeyValuePair<string,string>> - right!
            var queryStrings = request.GetQueryNameValuePairs();
            if (queryStrings == null)
                return null;

            var match = queryStrings.FirstOrDefault(kv => String.Compare(kv.Key, key, StringComparison.OrdinalIgnoreCase) == 0);
            if (string.IsNullOrEmpty(match.Value))
                return null;

            return match.Value;
        }

    }


    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public string Callback { get; private set; }

        public JsonpMediaTypeFormatter(string callback = null)
        {
            this.Callback = callback;
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
        {
            if (string.IsNullOrEmpty(this.Callback))
            {
                return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            }

            try
            {
                this.WriteToStream(type, value, writeStream, content);
                return Task.FromResult<AsyncVoid>(new AsyncVoid());
            }
            catch (Exception exception)
            {
                TaskCompletionSource<AsyncVoid> source = new TaskCompletionSource<AsyncVoid>();
                source.SetException(exception);
                return source.Task;
            }
        }
        
        private void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            JsonSerializer serializer = JsonSerializer.Create(this.SerializerSettings);
            using (StreamWriter streamWriter = new StreamWriter(writeStream, this.SupportedEncodings.First()))
            using (JsonTextWriter jsonTextWriter = new JsonTextWriter(streamWriter) { CloseOutput = false })
            {
                jsonTextWriter.WriteRaw(this.Callback + "(");
                serializer.Serialize(jsonTextWriter, value);
                jsonTextWriter.WriteRaw(")");
            }
        }
        
        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            if (request.Method != HttpMethod.Get)
            {
                return this;
            }

            string callback;
            if (request.GetQueryNameValuePairs().ToDictionary(pair => pair.Key,
                 pair => pair.Value).TryGetValue("callback", out callback))
            {
                return new JsonpMediaTypeFormatter(callback);
            }
            return this;
        }
        
        [StructLayout(LayoutKind.Sequential, Size = 1)]
        private struct AsyncVoid
        { }

    }
}
