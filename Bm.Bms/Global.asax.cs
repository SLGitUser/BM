using System.Diagnostics;
using System.Reflection;
using System.Web;
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
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ts.Stop();
            Logger.Info($"BMS started, cost {ts.ElapsedMilliseconds} ms");
        }
    }
}
