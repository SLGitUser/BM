using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Bm.Models.Common
{
    /// <summary>
    /// 消息记录器
    /// </summary>
    /// <typeparam name="TModel">资料类型</typeparam>
    [Serializable]
    [DataContract]
    public sealed class MessageRecorder<TModel>
    {
        /// <summary>
        /// 消息列表
        /// </summary>
        public IList<MessageInfo> Messages => _messages;

        /// <summary>
        /// 错误列表
        /// </summary>
        public IList<MessageInfo> Errors => _messages.Where(m => m.Type.Equals(MessageType.Error) || m.Type.Equals(MessageType.Fatal)).ToList();

        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasError => _messages.Any(m => m.Type.Equals(MessageType.Error) || m.Type.Equals(MessageType.Fatal));

        /// <summary>
        /// 资源实例
        /// </summary>
        [DataMember]
        public TModel Value { get; set; }

        /// <summary>
        /// 追加消息记录器的消息
        /// </summary>
        /// <typeparam name="TAnotherModel"></typeparam>
        /// <param name="mr"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Append<TAnotherModel>(MessageRecorder<TAnotherModel> mr)
        {
            foreach (var message in mr.Messages)
            {
                _messages.Add(message);
            }
            return this;
        }

        /// <summary>
        /// 设置资源实例值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> SetValue(TModel value)
        {
            Value = value;
            return this;
        }

        /// <summary>
        /// 输出跟踪消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Trace(string message)
        {
            return Record(MessageType.Trace, message);
        }

        /// <summary>
        /// 输出调试消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Debug(string message)
        {
            return Record(MessageType.Debug, message);
        }

        /// <summary>
        /// 输出警告消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Warn(string message)
        {
            return Record(MessageType.Warn, message);
        }

        /// <summary>
        /// 输出信息消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Info(string message)
        {
            return Record(MessageType.Info, message);
        }

        /// <summary>
        /// 输出错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Error(string message)
        {
            return Record(MessageType.Error, message);
        }

        /// <summary>
        /// 输出严重错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Fatal(string message)
        {
            return Record(MessageType.Fatal, message);
        }

        /// <summary>
        /// 输出操作成功消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Success(string message)
        {
            return Record(MessageType.Success, message);
        }

        /// <summary>
        /// 输出操作错误消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public MessageRecorder<TModel> Failure(string message)
        {
            return Record(MessageType.Failure, message);
        }

        #region private

        /// <summary>
        /// 消息列表
        /// </summary>
        [DataMember]
        private readonly IList<MessageInfo> _messages = new List<MessageInfo>();

        /// <summary>
        /// 记录消息
        /// </summary>
        /// <param name="type"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private MessageRecorder<TModel> Record(MessageType type, string message)
        {
            _messages.Add(new MessageInfo(type, message));
            return this;
        }

        #endregion

    }

    /// <summary>
    /// 操作消息
    /// </summary>
    [Serializable]
    [DataContract]
    public class MessageInfo
    {
        /// <summary>
        /// 构造函数，生成当前时间下的消息
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="message">消息内容</param>
        public MessageInfo(MessageType type, string message)
        {
            Type = type;
            At = DateTime.Now;
            Message = message;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type">消息类型</param>
        /// <param name="at">消息发生时间</param>
        /// <param name="message">消息内容</param>
        public MessageInfo(MessageType type, DateTime at, string message)
        {
            Type = type;
            At = at;
            Message = message;
        }

        /// <summary>
        /// 消息类型
        /// </summary>
        [DataMember]
        public MessageType Type { get; }

        /// <summary>
        /// 消息发生时间
        /// </summary>
        [DataMember]
        public DateTime At { get; }

        /// <summary>
        /// 消息内容
        /// </summary>
        [DataMember]
        public string Message { get; }
    }

    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// 跟踪（表示跟踪信息，开发人员使用，在发布模式下输出）
        /// </summary>
        /// <example></example>
        Trace,
        /// <summary>
        /// 调试（表示调试信息，开发人员使用，只在调试模式下输出）
        /// </summary>
        /// <example></example>
        Debug,
        /// <summary>
        /// 信息（表示普通信息，最终用户可见）
        /// </summary>
        /// <example>如操作的关键数据记录</example>
        Info,
        /// <summary>
        /// 警告（表示破坏性提示，最终用户可见）
        /// </summary>
        /// <example>如不可逆操作或者资源删除操作</example>
        Warn,
        /// <summary>
        /// 错误（表示一般性错误，最终用户可见，会导致操作无法继续正常进行）
        /// </summary>
        /// <example>如接收用户传值无效，处理时抛出异常</example>
        Error,
        /// <summary>
        /// 严重错误（表示严重错误，最终用户可见，会导致系统无法继续正常运行）
        /// </summary>
        /// <example>如关键性基础数据没有配置，基础运行条件没有满足，重大处理过程异常</example>
        Fatal,
        /// <summary>
        /// 操作成功（表示操作的结果是成功的，仅最终用户可见）
        /// </summary>
        /// <example>如用户输入成功，处理成功，返回布尔值真</example>
        Success,
        /// <summary>
        /// 操作失败（表示操作的结果是失败的，仅最终用户可见）
        /// </summary>
        /// <example>如用户输入成功，处理失败，没有抛出异常，返回布尔值假</example>
        Failure
    }
}
