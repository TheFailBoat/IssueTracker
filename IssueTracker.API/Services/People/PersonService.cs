using IssueTracker.API.Repositories;
using IssueTracker.Data;
using IssueTracker.Data.Requests.People;
using ServiceStack.Common.Web;
using ServiceStack.ServiceInterface;

namespace IssueTracker.API.Services.People
{
    [Authenticate]
    public class PersonService : Service
    {
        public IPersonRepository PersonRepository { get; set; }

        public Person Get(PersonDetails request)
        {
            var person = PersonRepository.GetById(request.Id);
            if (person == null)
                throw HttpError.NotFound("Person does not exist: " + request.Id);

            return person;
        }
    }
}