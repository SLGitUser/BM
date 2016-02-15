using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using Bm.Models.Common;
using log4net;

namespace Bm.Extensions
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        protected static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 获得参数值
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public string GetParas(string para)
        {
            if(string.IsNullOrWhiteSpace(para)) return null;
            var item = Request.Form[para];
            if (string.IsNullOrEmpty(item))
                item = Request.QueryString[string.Concat("amp;", para)];
            if (string.IsNullOrEmpty(item))
                item = Request.QueryString[para];
            return string.IsNullOrEmpty(item) ? item : item.Trim();

        }

        /// <summary>
        /// 获得参数值
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>
        public string GetDbParas(string para)
        {
            var value = GetParas(para);
            return string.IsNullOrWhiteSpace(value) ? null : value.Replace("'", "''").Replace("*", "%");
        }

        /// <summary>
        /// 获得模型中的错误信息
        /// </summary>
        /// <returns></returns>
        public string GetModelErrorMsg()
        {
            var errors = ModelState.Values.Where(m => m.Errors.Any()).Select(m => m.Errors);
            var list = (from error in errors from item in error select item.ErrorMessage).ToList();
            return string.Join("; ", list);
        }

        #region Message

        public void FlashSuccess(string message)
        {
            _messageInfos.Add(new MessageInfo(MessageType.Success, message));
            ViewBag.Messages = _messageInfos;
        }

        public void FlashFailure(string message)
        {
            _messageInfos.Add(new MessageInfo(MessageType.Failure, message));
            ViewBag.Messages = _messageInfos;
        }
        
        public void FlashInfo(string message)
        {
            _messageInfos.Add(new MessageInfo(MessageType.Info, message));
            ViewBag.Messages = _messageInfos;
        }

        public void FlashError(string message)
        {
            _messageInfos.Add(new MessageInfo(MessageType.Error, message));
            ViewBag.Messages = _messageInfos;
        }

        public void FlashWarn(string message)
        {
            _messageInfos.Add(new MessageInfo(MessageType.Warn, message));
            ViewBag.Messages = _messageInfos;
        }

        public void FlashMessage<T>(MessageRecorder<T> mr)
        {
            _messageInfos.AddRange(mr.Messages);
            ViewBag.Messages = _messageInfos;
        }

        private readonly List<MessageInfo> _messageInfos = new List<MessageInfo>();

        #endregion
        
        #region JSON消息

        /// <summary>
        /// 以JSON格式返回操作错误信息
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ActionResult JsonError(string message, object data = null)
        {
            return JsonMessage(500, message, data);
        }

        /// <summary>
        /// 以JSON格式返回操作拒绝信息
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ActionResult JsonRefuse(string message, object data = null)
        {
            return JsonMessage(100, message, data);
        }

        /// <summary>
        /// 以JSON格式返回操作成功信息
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ActionResult JsonSuccess(string message, object data = null)
        {
            return JsonMessage(200, message, data);
        }

        /// <summary>
        /// 以JSON格式返回操作成功信息
        /// </summary>
        /// <returns></returns>
        public ActionResult JsonSuccess()
        {
            return JsonMessage(200, null, null);
        }

        /// <summary>
        /// 以JSON格式返回请求数据
        /// </summary>
        /// <param name="objectArr"></param>
        /// <returns></returns>
        public ActionResult JsonData(object objectArr)
        {
            return JsonMessage(200, null, objectArr);
        }

        /// <summary>
        /// Jsons the message.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="message">The message.</param>
        /// <param name="data">The data.</param>
        /// <returns></returns>
        public ActionResult JsonMessage(int status, string message, object data)
        {
            return new JsonResult
            {
                Data = new
                {
                    status,
                    message,
                    callback = Request.Params["callback"],
                    flag = Request.Params["flag"],
                    data
                },
                ContentEncoding = Encoding.UTF8
            };
        }

        #endregion
    }
}