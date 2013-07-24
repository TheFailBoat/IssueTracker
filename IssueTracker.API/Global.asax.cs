using System;
using System.Configuration;
using System.Web;
using Funq;
using IssueTracker.API.Config;
using IssueTracker.API.Entities;
using IssueTracker.API.Seeding;
using ServiceStack.Common.Web;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;
using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.SqlServer;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.ServiceInterface.Validation;
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
    }
}