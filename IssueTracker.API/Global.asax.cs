using System;
using System.Web;
using ServiceStack.WebHost.Endpoints;

namespace IssueTracker.API
{
    public class Global : HttpApplication
    {
        public class IssueTrackerHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public IssueTrackerHost() : base("IssueTracker Web Services", typeof(Global).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                //register any dependencies your services use, e.g:
                //container.Register<ICacheClient>(new MemoryCacheClient());
            }
        }


        protected void Application_Start(object sender, EventArgs e)
        {
            new IssueTrackerHost().Init();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}