using Funq;
using ServiceStack.CacheAccess;
using ServiceStack.CacheAccess.Providers;

namespace IssueTracker.API.Config
{
    public class ServicesConfig
    {
        public static void Register(Container container)
        {
            container.Register<ICacheClient>(new MemoryCacheClient());

            //TODO container.Register<ISecurityService>()
        }
    }
}