using System;
using System.Reflection;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Modules.Orm;
using Bm.Modules.Orm.Sql;
using log4net;

namespace Bm.Services.Common
{
    /// <summary>
    /// 验证码服务
    /// </summary>
    public sealed class VerificationCodeService
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 两次请求最小时间间隔
        /// </summary>
        private static readonly int MinInterval = 10;

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <param name="accountNo">账号</param>
        /// <param name="phoneNo">手机号</param>
        /// <returns></returns>
        public MessageRecorder<VerificationCode> MakeCode(AlidayuService.CodeType codeType, string accountNo, string phoneNo)
        {
            var mr = new MessageRecorder<VerificationCode>();
            if (string.IsNullOrEmpty(phoneNo)) return mr.Error("没有设置手机号");

            // 检测是否达到最小时间间隔，10秒
            using (var conn = ConnectionManager.Open())
            {
                var query = new Criteria<VerificationCode>()
                    .Where(m => m.PhoneNo, Op.Eq, phoneNo)
                    .And(m => m.CodeType, Op.Eq, codeType)
                    .And(m => m.ExpiredAt, Op.NotNul, DateTime.Now)
                    .Desc(m => m.CreatedAt);
                var existModel = conn.Get(query);
                if (existModel != null && existModel.CreatedAt.AddSeconds(MinInterval) > DateTime.Now)
                {
                    return mr.Error("发送间隔时间太短，请稍候");
                }
            }

            var now = DateTime.Now;
            var rnd = new Random(now.Millisecond);
            var code = string.Concat(rnd.Next(0, 9), rnd.Next(0, 9), rnd.Next(0, 9), rnd.Next(0, 9), rnd.Next(0, 9));

            var r = new AlidayuService().SendCode(codeType, accountNo, phoneNo, code);

            mr.Append(r);
            if (r.HasError) return mr;

            var vcode = new VerificationCode
            {
                CreatedAt = now,
                CodeType = codeType,
                AccountNo = accountNo,
                PhoneNo = phoneNo,
                AliOrderNo = r.Value,
                Code = code,
                CreatedBy = "SYSTEM",
                ExpiredAt = now.AddSeconds(MinInterval),
                Uuid = Guid.NewGuid().ToString("N")
            };
            using (var conn = ConnectionManager.Open())
            {
                var effectedCount = conn.Insert(vcode);
                if (effectedCount == -1) return mr.Error($"保存验证码失败，流水{r.Value}无效");
            }
            
            // 发送成功后，存储发送记录
            Logger.Info($"验证服务：{codeType}，账户 {accountNo}，号码 {phoneNo}，代码 {code}，流水号 {r.Value}");

            // 返回验证码
            return mr.SetValue(vcode);
        }

        /// <summary>
        /// 代码验证
        /// </summary>
        /// <param name="phoneNo">手机号码</param>
        /// <param name="uuid">业务代码</param>
        /// <param name="code">验证码</param>
        /// <returns></returns>
        public MessageRecorder<bool> AuthCode(string phoneNo, string uuid, string code)
        {
            var mr = new MessageRecorder<bool>();
            using (var conn = ConnectionManager.Open())
            {
                var trans = conn.BeginTransaction();
                var query = new Criteria<VerificationCode>()
                    .Where(m => m.Uuid, Op.Eq, uuid);
                var model = conn.Get(query, trans);
                if (model == null) return mr.Error("业务代码无效");

                if (model.VerifyAt.HasValue) return mr.Error("业务代码已失效");

                if (!Equals(model.PhoneNo, phoneNo)) return mr.Error("手机号码不匹配");
                if (!Equals(model.Code, code)) return mr.Error("验证码不匹配");
                

                model.VerifyAt = DateTime.Now;

                if (!conn.Update(model, trans))
                {
                    trans.Rollback();
                    return mr.Error($"记录 {model.Uuid} 验证时间失败");
                }

                trans.Commit();
            }
            return mr.SetValue(true);
        }
    }
}
