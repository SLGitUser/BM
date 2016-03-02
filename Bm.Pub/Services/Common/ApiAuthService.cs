using System;
using System.Reflection;
using Bm.Modules.Helper;
using log4net;

namespace Bm.Services.Common
{
    public sealed class ApiAuthService
    {
        /// <summary>
        /// 短信签名和模板编号字典
        /// </summary>
        private static readonly string BeaSecret;

        /// <summary>
        ///  日志
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        

        static ApiAuthService()
        {
            BeaSecret = CommonSetService.GetString("Plat.Auth.Bea.Secret");
        }

        /// <summary>
        /// 认证
        /// </summary>
        /// <param name="app">应用键</param>
        /// <param name="time">时间戳</param>
        /// <param name="signature">摘要</param>
        /// <returns></returns>
        public bool Auth(string app, string time, string signature)
        {
            if (string.IsNullOrEmpty(app)) return false;
            if (string.IsNullOrEmpty(time)) return false;
            if (string.IsNullOrEmpty(signature)) return false;

            if (!"BEA".Equals(app)) return false;

            var input = string.Concat(BeaSecret, "-", time).Md5Hash();
            return input.Equals(signature, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
