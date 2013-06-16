using ServiceStack.ServiceHost;

namespace IssueTracker.Data.Requests.People
{
    [Route("/person/{id}", "GET")]
    public class PersonDetails : IReturn<Person>
    {
        public long Id { get; set; }
    }
}
