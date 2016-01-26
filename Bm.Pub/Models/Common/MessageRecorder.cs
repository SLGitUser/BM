using System;
using System.Collections.Generic;
using System.Linq;

namespace Bm.Models.Common
{
    public sealed class MessageRecorder<TModel>
    {
        public IList<MessageInfo> Messages => _messages;

        public bool HasError => _messages.Any(m => m.Type.Equals(MessageType.Error) || m.Type.Equals(MessageType.Fatal));

        public TModel Value { get; set; }

        public MessageRecorder<TModel> Append<TAnotherModel>(MessageRecorder<TAnotherModel> mr)
        {
            foreach (var message in mr.Messages)
            {
                _messages.Add(message);
            }
            return this;
        }

        public MessageRecorder<TModel> SetValue(TModel value)
        {
            Value = value;
            return this;
        }

        public MessageRecorder<TModel> Trace(string message)
        {
            return Record(MessageType.Trace, message);
        }

        public MessageRecorder<TModel> Debug(string message)
        {
            return Record(MessageType.Debug, message);
        }

        public MessageRecorder<TModel> Warn(string message)
        {
            return Record(MessageType.Warn, message);
        }

        public MessageRecorder<TModel> Info(string message)
        {
            return Record(MessageType.Info, message);
        }

        public MessageRecorder<TModel> Error(string message)
        {
            return Record(MessageType.Error, message);
        }

        public MessageRecorder<TModel> Fatal(string message)
        {
            return Record(MessageType.Fatal, message);
        }

        public MessageRecorder<TModel> Success(string message)
        {
            return Record(MessageType.Success, message);
        }

        public MessageRecorder<TModel> Failure(string message)
        {
            return Record(MessageType.Failure, message);
        }

        private readonly IList<MessageInfo> _messages = new List<MessageInfo>();

        private MessageRecorder<TModel> Record(MessageType type, string message)
        {
            _messages.Add(new MessageInfo(type, message));
            return this;
        }

    }


    public class MessageInfo
    {
        public MessageInfo(MessageType type, string message)
        {
            Type = type;
            At = DateTime.Now;
            Message = message;
        }

        public MessageInfo(MessageType type, DateTime at, string message)
        {
            Type = type;
            At = at;
            Message = message;
        }

        public MessageType Type { get; set; }

        public DateTime At { get; set; }

        public string Message { get; set; }
    }

    public enum MessageType
    {
        Trace,
        Debug,
        Info,
        Warn,
        Error,
        Fatal,
        Success,
        Failure
    }
}
