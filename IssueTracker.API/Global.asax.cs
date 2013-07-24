using System;
using System.Web;
using Funq;
using IssueTracker.API.Config;
using ServiceStack.MiniProfiler;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

namespace IssueTracker.API
{
    public class Global : HttpApplication
    {
        public class IssueTrackerHost : AppHostBase
        {
            // Tell Service Stack the name of your application and where to find your web services
            public IssueTrackerHost() : base("IssueTracker Web Services", typeof(Global).Assembly) { }

            public override void Configure(Container container)
            {
                JsConfig.EmitCamelCaseNames = true;
                JsConfig.DateHandler = JsonDateHandler.ISO8601;

                PluginsConfig.Register(this, container);
                DataConfig.Register(container);
                ServicesConfig.Register(container);
            }
        }
        
        protected void Application_Start(object sender, EventArgs e)
        {
            new IssueTrackerHost().Init();
        }

        protected void Application_BeginRequest(object src, EventArgs e)
        {
            if (Request.IsLocal)
                Profiler.Start();
        }

        protected void Application_EndRequest(object src, EventArgs e)
        {
            Profiler.Stop();
        }
    }
}