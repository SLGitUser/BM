using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bm.Models.Common;
using Bm.Models.Dp;

namespace Bm.Services.Dp
{
    public sealed class BrokerAccountService
    {
        public MessageRecorder<Broker> Auth(string phone, string password)
        {
            //TODO 实现经纪人登录认证
            return new MessageRecorder<Broker>();
        }


        public MessageRecorder<Broker> Create(string phone, string password)
        {
            //TODO 实现经纪人登录认证
            return new MessageRecorder<Broker>();
        }
    }
}
