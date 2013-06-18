using IssueTracker.Data;
using ServiceStack.ServiceHost;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;

namespace IssueTracker.API.Repositories
{
    public interface IPersonRepository
    {
        Person GetById(long id);
        Person GetCurrent();
    }

    public class PersonRepository : IPersonRepository, IRequiresRequestContext
    {
        public IRequestContext RequestContext { get; set; }

        private readonly IUserAuthRepository repository;
        public PersonRepository(IUserAuthRepository repository)
        {
            this.repository = repository;
        }

        public Person GetById(long id)
        {
            var person = ToPerson(repository.GetUserAuth(id.ToString()));

            if (person.IsEmployee) return person;

            var current = GetCurrent();
            if (current.Id == person.Id) return person;

            if (!current.CustomerId.HasValue || !person.CustomerId.HasValue) return null;
            if (current.CustomerId.Value != person.CustomerId.Value) return null;

            return person;
        }

        public Person GetCurrent()
        {
            var httpRequest = RequestContext.Get<IHttpRequest>();
            if (httpRequest == null) return null;

            var session = httpRequest.GetSession();
            if (session == null) return null;

            return ToPerson(repository.GetUserAuth(session.UserAuthId));
        }

        private static Person ToPerson(UserAuth auth)
        {
            if (auth == null)
            {
                return null;
            }

            var name = auth.FullName;
            if (string.IsNullOrEmpty(name))
            {
                if (!string.IsNullOrEmpty(auth.FirstName) || !string.IsNullOrEmpty(auth.LastName))
                {
                    name = auth.FirstName + " " + auth.LastName;
                }
                else
                {
                    name = auth.UserName;
                }
            }

            return new Person
            {
                Id = auth.Id,
                Name = name,

                CustomerId = auth.RefId,
                IsEmployee = auth.Roles.Contains(Global.Constants.EmployeeRoleName)
            };
        }
    }
}