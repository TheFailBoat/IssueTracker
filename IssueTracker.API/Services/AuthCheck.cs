using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services
{
    [Authenticate]
    public class AuthCheckService : Service
    {
        public IPersonRepository PersonRepository { get; set; }

        public Person Get(AuthCheck request)
        {
            return PersonRepository.GetCurrent();
        }
    }
}