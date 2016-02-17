using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;
using log4net;

namespace Bm.Modules.Html
{
    /// <summary>
    /// 计时器
    /// </summary>
    public sealed class TimingFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Stopwatch _stopwatch = new Stopwatch();

        private long _t1;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User?.Identity?.Name;
            Logger.Debug($"user {user}  enter {filterContext.HttpContext.Request.Path}");
            _stopwatch.Start();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            _stopwatch.Stop();
            var user = filterContext.HttpContext.User?.Identity?.Name;
            _t1 = _stopwatch.ElapsedMilliseconds;
            Logger.Debug($"user {user} handle {filterContext.HttpContext.Request.Path}, cost {_t1} ms");
            if (_t1 >= 2000)
            {
                Logger.Warn($"!SLOW user {user} handle {filterContext.HttpContext.Request.Path}, cost {_t1} ms");
            }
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            //var user = filterContext.HttpContext.User?.Identity?.Name;
            _stopwatch.Start();
            base.OnResultExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            base.OnResultExecuted(filterContext);
            _stopwatch.Stop();
            var user = filterContext.HttpContext.User?.Identity?.Name;
            var t2 = _stopwatch.ElapsedMilliseconds - _t1;
            Logger.Debug($"user {user} render {filterContext.HttpContext.Request.Path}, cost {t2} ms");
            if (t2 >= 2000)
            {
                Logger.Warn($"!SLOW user {user} render {filterContext.HttpContext.Request.Path}, cost {t2} ms");
            }
            //Logger.Debug($"user {user}  visit {filterContext.HttpContext.Request.Path}, total cost {_stopwatch.ElapsedMilliseconds} ms");
            _stopwatch.Reset();
        }
    }
}
