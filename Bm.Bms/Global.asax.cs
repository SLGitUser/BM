using System.Diagnostics;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using log4net;

namespace Bm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        
        protected void Application_Start()
        {
            var ts = new Stopwatch();
            ts.Start();

            log4net.Config.XmlConfigurator.Configure();

            Logger.Debug("BMS starting");
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ts.Stop();
            Logger.Info($"BMS started, cost {ts.ElapsedMilliseconds} ms");
        }
    }
}
