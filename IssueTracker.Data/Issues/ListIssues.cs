using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues
{
    [Route("/issues", "GET")]
    public class ListIssues : IReturn<ListIssuesResponse>
    {
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }

    public class ListIssuesResponse
    {
        public List<Issue> Issues { get; set; }
    }
}
