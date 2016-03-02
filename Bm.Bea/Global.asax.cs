using System;
using System.Diagnostics;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Http;
using Bm.Extensions;
using log4net;

namespace Bm.Bea
{
    public class Global : HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly HttpRequest InitialRequest;

        static Global()
        {
            InitialRequest = HttpContext.Current.Request;
        }

        void Application_Start(object sender, EventArgs e)
        {
            var ts = new Stopwatch();
            ts.Start();

            log4net.Config.XmlConfigurator.Configure();

            Logger.Debug($"BEA starting on {GetVer()}");

            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //GlobalConfiguration.Configuration.EnableCors();
            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonpMediaTypeFormatter());
            
            ts.Stop();
            Logger.Info($"BEA started, cost {ts.ElapsedMilliseconds} ms");
        }


        private string GetVer()
        {
            return string.Concat(InitialRequest.ServerVariables["SERVER_NAME"],
                ":",
                InitialRequest.ServerVariables["SERVER_PORT"],
                " by ",
                InitialRequest.ServerVariables["SERVER_PROTOCOL"]);
        }
    }
}