using System.Collections.Generic;
using System.Web.Mvc;
using Bm.Models.Common;

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
    }
}