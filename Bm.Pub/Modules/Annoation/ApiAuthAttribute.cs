using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Bm.Extensions;
using Bm.Services.Common;
using log4net;

namespace Bm.Modules.Annoation
{
    /// <summary>
    /// API服务认证属性
    /// </summary>
    public sealed class ApiAuthAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Stopwatch _stopwatch = new Stopwatch();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            _stopwatch.Start();

            var request = actionContext.Request;
            var app = request.GetQueryString("app");
            var time = request.GetQueryString("time");
            var sign = request.GetQueryString("sign");

            var isAuth = new ApiAuthService().Auth(app, time, sign);
            if (!isAuth)
            {
                var url = actionContext.Request.RequestUri.AbsolutePath;
                var method = actionContext.Request.Method;
                Logger.Error($"{app} {method} {url}, parameters error");

                var obj = new ApiControllerHelper.MessageRecordOutputModel
                {
                    HasError = true,
                    Errors = new List<string> { "parameters error" },
                    Model = null
                };
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, obj);
            }
            else
            {
                base.OnActionExecuting(actionContext);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);
            _stopwatch.Stop();

            var t = _stopwatch.ElapsedMilliseconds;
            var app = actionExecutedContext.Request.GetQueryString("app");
            var url = actionExecutedContext.Request.RequestUri.AbsolutePath;
            var method = actionExecutedContext.Request.Method;
            Logger.Info($"{app} {method} {url}, cost {t} ms");
            if (t >= 2000)
            {
                Logger.Warn($"!SLOW {app} {method} {url}, cost {t} ms");
            }
            _stopwatch.Reset();
        }
        

    }
}
