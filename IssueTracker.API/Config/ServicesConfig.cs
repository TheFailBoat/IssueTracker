using Funq;
using IssueTracker.API.Repositories;
using IssueTracker.API.Security;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;

namespace IssueTracker.API.Config
{
    public class ServicesConfig
    {
        public static void Register(Container container)
        {
            container.Register<ICacheClient>(new MemoryCacheClient());

            container.Register<ISecurityService>(x => new SecurityServiceImpl(x.Resolve<IUserRepository>(), x.Resolve<IAuthTokenRepository>())).ReusedWithin(ReuseScope.Request);
        }
    }
}