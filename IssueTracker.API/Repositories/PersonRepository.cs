﻿using IssueTracker.Data;
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
        private readonly IUserAuthRepository repository;
        private readonly IAuthSession currentUser;

        public PersonRepository(IUserAuthRepository repository, IAuthSession currentUser)
        {
            this.repository = repository;
            this.currentUser = currentUser;
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
            return ToPerson(repository.GetUserAuth(currentUser.UserAuthId));
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