using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace Bm
{
    public class MvcApplication : HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly HttpRequest InitialRequest;

        static MvcApplication()
        {
            InitialRequest = HttpContext.Current.Request;
        }

        private string GetVer()
        {
            return string.Concat(InitialRequest.ServerVariables["SERVER_NAME"],
                ":",
                InitialRequest.ServerVariables["SERVER_PORT"],
                " by ",
                InitialRequest.ServerVariables["SERVER_PROTOCOL"]);
        }

        protected void Application_Start()
        {
            var ts = new Stopwatch();
            ts.Start();

            log4net.Config.XmlConfigurator.Configure();

            Logger.Debug($"BMS starting on {GetVer()}");

            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ts.Stop();
            Logger.Info($"BMS started, cost {ts.ElapsedMilliseconds} ms");
        }


        /// <summary>
        /// 全局异常处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            try
            {
                OnHandlerError(sender, e);
            }
            catch (Exception ex)
            {
                Logger.Error($"调用OnHandlerError失败，原因：{ex.Message}，堆栈：{ex.StackTrace}");
            }
        }

        void OnHandlerError(object sender, EventArgs e)
        {
            //获得最后一个Exception
            var lastError = Context.Server.GetLastError();
            var user = HttpContext.Current.User?.Identity?.Name;
            var buff = new StringBuilder();
            buff.Append("User: ");
            buff.Append(user);
            buff.Append("，遭遇Global.asax捕捉错误，报告信息如下：");
            buff.Append(Environment.NewLine);
            buff.Append("IP：");
            buff.Append(GetIPAddress());
            buff.Append(Environment.NewLine);
            buff.Append("路径：");
            buff.Append(Request.Path);
            buff.Append(Environment.NewLine);
            buff.Append("错误：");
            buff.Append(lastError.Message);
            buff.Append(Environment.NewLine);
            buff.Append("堆栈：");
            buff.Append(lastError.StackTrace);
            buff.Append(Environment.NewLine);
            buff.Append("内在异常：");
            buff.Append(lastError.InnerException?.Message ?? string.Empty);
            Logger.Error(buff.ToString());

            //清除掉Exception
            Context.Server.ClearError();
        }

        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
