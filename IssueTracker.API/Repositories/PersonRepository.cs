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
            return ToPerson(repository.GetUserAuth(id.ToString()));
        }

        public Person GetCurrent()
        {
            return ToPerson(repository.GetUserAuth(session.UserAuthId));
        }

        private static Person ToPerson(UserAuth auth)
        {
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