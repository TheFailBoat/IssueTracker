using ServiceStack.Common.Web;
using ServiceStack.ServiceHost;

namespace IssueTracker.API.Utilities
{
    public static class HttpUtils
    {
        public static string GetAuthToken(IHttpRequest request)
        {
            var auth = request.Headers[HttpHeaders.Authorization];

            if (auth != null)
            {
                var parts = auth.Split(' ');
                if (parts.Length != 2) return null;

                return parts[0].ToLower() == "basic" ? parts[1] : null;
            }

            auth = request.QueryString["api_key"];
            if (auth == null)
            {
                // possible add other headers / cookies here
            }

            return auth;
        }
    }
}