using System;
using System.Collections.Generic;
using System.Reflection;
using Bm.Models.Common;
using log4net;
using Top.Api;
using Top.Api.Request;

namespace Bm.Services.Common
{
    /// <summary>
    /// 阿里大鱼短信发送服务
    /// </summary>
    public class AlidayuService
    {
        /// <summary>
        /// 产品名称
        /// </summary>
        private static readonly string ProductName;

        private static readonly string AccessKeyId;
        private static readonly string AccessKeySecret;
        private static readonly string Endpoint;
        private static readonly string Format = "json";

        /// <summary>
        /// 短信签名和模板编号字典
        /// </summary>
        private static readonly IDictionary<string, string> Dict = new Dictionary<string, string>();
        
        /// <summary>
        ///  日志
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// 服务调用端
        /// </summary>
        private ITopClient _client;

        static AlidayuService()
        {
            ProductName = CommonSetService.GetString("Plat.Product.Name");

            AccessKeyId = CommonSetService.GetString("Alidayu.AccessKeyId");
            AccessKeySecret = CommonSetService.GetString("Alidayu.AccessKeySecret");
            Endpoint = CommonSetService.GetString("Alidayu.EndPoint");

            var keys = new[]
            {
                "Alidayu.Sign.Activity",
                "Alidayu.Sign.Auth",
                "Alidayu.Sign.LoginExcept",
                "Alidayu.Sign.LoginNormal",
                "Alidayu.Sign.Modify",
                "Alidayu.Sign.Register",
                "Alidayu.Sign.ResetPass",

                "Alidayu.Tpl.Activity",
                "Alidayu.Tpl.Auth",
                "Alidayu.Tpl.LoginExcept",
                "Alidayu.Tpl.LoginNormal",
                "Alidayu.Tpl.Modify",
                "Alidayu.Tpl.Register",
                "Alidayu.Tpl.ResetPass"
            };
            foreach (var key in keys)
            {
                var value = CommonSetService.GetString(key);
                if (string.IsNullOrEmpty(value))
                {
                    Logger.Error($"没有配置数据：{key}");
                }
                Dict.Add(key, value);
            }
        }


        public AlidayuService()
        {
            _client = new DefaultTopClient(Endpoint, AccessKeyId, AccessKeySecret, Format);
        }

        /// <summary>
        /// 代码类型
        /// </summary>
        public enum CodeType
        {
            /// <summary>
            /// 活动
            /// </summary>
            Activity,
            /// <summary>
            /// 身份认证
            /// </summary>
            Auth,
            /// <summary>
            /// 异地登录
            /// </summary>
            LoginExcept,
            /// <summary>
            /// 正常登录
            /// </summary>
            LoginNormal,
            /// <summary>
            /// 变更
            /// </summary>
            Modify,
            /// <summary>
            /// 注册
            /// </summary>
            Register,
            /// <summary>
            /// 密码重置
            /// </summary>
            ResetPass
        }
        
        /// <summary>
        /// 发送验证码短信，操作成功返回流水号
        /// </summary>
        /// <param name="codeType">代码类型</param>
        /// <param name="accountNo">用户账号</param>
        /// <param name="phone">手机号码</param>
        /// <param name="code">验证码</param>
        /// <param name="item">活动名称</param>
        /// <returns></returns>
        public MessageRecorder<string> SendCode(CodeType codeType, string accountNo, string phone, string code, string item = null)
        {
            var r = new MessageRecorder<string>();
            try
            {
                var signName = Dict["Alidayu.Sign." + codeType];
                var tplNo = Dict["Alidayu.Tpl." + codeType];

                if (string.IsNullOrEmpty(signName)) r.Error("没有设置签名");
                if (string.IsNullOrEmpty(tplNo)) r.Error("没有设置模板编号");
                if (string.IsNullOrEmpty(phone)) r.Error("没有设置手机号码");
                if (string.IsNullOrEmpty(code)) r.Error("没有设置验证码");
                if (r.HasError) return r;

                var req = new AlibabaAliqinFcSmsNumSendRequest
                {
                    Extend = accountNo,
                    SmsType = "normal",
                    SmsFreeSignName = signName,
                    SmsParam = string.IsNullOrEmpty(item) ?
                    string.Concat("{", $"\"code\": \"{code}\",\"product\": \"{ProductName}\"", "}") :
                    string.Concat("{", $"\"code\": \"{code}\",\"product\": \"{ProductName}\",\"item\": \"{item}\"", "}"),
                    RecNum = phone,
                    SmsTemplateCode = tplNo
                };
                var rsp = _client.Execute(req);
                _client = null;
                if (rsp == null) return r.Error("发送无返回结果");
                if (rsp.IsError) return r.Error(rsp.ErrMsg);

                // {"alibaba_aliqin_fc_sms_num_send_response":{"result":{"err_code":"0","model":"100717449333^1101135565756","success":true},"request_id":"iv15pyenlo5r"}}
                // {"error_response":{"code":15,"msg":"Remote service error","sub_code":"isv.SMS_SIGNATURE_ILLEGAL","sub_msg":"短信签名不合法","request_id":"16ceaw87jeegp"}}
                
                return r.Info("发送短信成功").SetValue(rsp.Result.Model);
            }
            catch (Exception ex)
            {
                return r.Error($"发送短信失败. 原因：{ex.Message}");
            }
        }


    }
}
