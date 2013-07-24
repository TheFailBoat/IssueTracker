using Funq;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Common.Web;
using ServiceStack.Plugins.MsgPack;
using ServiceStack.ServiceInterface.Cors;
using ServiceStack.WebHost.Endpoints;

namespace IssueTracker.API.Config
{
    public class PluginsConfig
    {
        public static void Register(IAppHost host, Container container)
        {
            // Enable MessagePack
            host.Plugins.Add(new MsgPackFormat());

            // Enable Dev UI
            host.Plugins.Add(new SwaggerFeature());

            // Enable CORS
            host.Plugins.Add(new CorsFeature(allowedHeaders: "Content-Type,Authorization"));
            host.RequestFilters.Add((request, response, dto) =>
            {
                if (request.HttpMethod == HttpMethods.Options)
                    response.EndRequest();
            });

        }
    }
}