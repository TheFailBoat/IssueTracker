using System.Collections.Generic;
using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Issues
{
    [Route("/issues", "GET")]
    public class ListIssues : IReturn<ListIssuesResponse>
    {
        [ApiMember(IsRequired = false, DataType = "long", ParameterType = "query")]
        public int? Page { get; set; }
        [ApiMember(IsRequired = false, DataType = "long", ParameterType = "query")]
        public int? PageSize { get; set; }

        [ApiMember(IsRequired = false, DataType = "long", ParameterType = "query")]
        public long? CustomerId { get; set; }
    }

    public class ListIssuesResponse
    {
        public List<Issue> Issues { get; set; }
    }
}
