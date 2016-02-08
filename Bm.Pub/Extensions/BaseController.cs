using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Bm.Models.Common;

namespace Bm.Extensions
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