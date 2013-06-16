using IssueTracker.Data;
using ServiceStack.ServiceInterface.Auth;

namespace IssueTracker.API.Repositories
{
    public interface IPersonRepository
    {
        Person GetById(long id);
        Person GetCurrent();
    }

    public class PersonRepository : IPersonRepository
    {
        private readonly IAuthSession session;
        private readonly IUserAuthRepository repository;
        public PersonRepository(IAuthSession session, IUserAuthRepository repository)
        {
            this.session = session;
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
            return ToPerson(repository.GetUserAuth(session.UserAuthId));
        }

        private static Person ToPerson(UserAuth auth)
        {
            if (auth == null)
            {
                return null;
            }

            return new Person
            {
                Id = auth.Id,
                Name = auth.FullName,

                CustomerId = auth.RefId,
                IsEmployee = auth.Roles.Contains(Global.Constants.EmployeeRoleName)
            };
        }

    }
}