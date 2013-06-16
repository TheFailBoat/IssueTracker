using System;
using System.Collections.Generic;
using System.Data;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IPriorityRepository : IRepository<Priority>
    {
        void Move(long id, long amount);
    }

    internal class PriorityRepository : IPriorityRepository, IDisposable
    {
        private readonly IDbConnectionFactory dbFactory;
        private readonly IPersonRepository personRepository;
        private IDbConnection db;

        public PriorityRepository(IDbConnectionFactory dbFactory, IPersonRepository personRepository)
        {
            this.dbFactory = dbFactory;
            this.personRepository = personRepository;
        }

        private IDbConnection Db
        {
            get
            {
                return db ?? (db = dbFactory.Open());
            }
        }


        public List<Priority> GetAll()
        {
            return Db.Select<Priority>();
        }

        public Priority GetById(long id)
        {
            return Db.IdOrDefault<Priority>(id);
        }

        public Priority Add(Priority priority)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return null;
            }

            priority.Id = 0;

            Db.Insert(priority);
            priority.Id = Db.GetLastInsertId();

            return priority;
        }

        public Priority Update(Priority priority)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return null;
            }

            Db.Update(priority);

            return priority;
        }

        public bool Delete(long id)
        {
            if (!personRepository.GetCurrent().IsEmployee)
            {
                return false;
            }

            Db.DeleteById<Priority>(id);

            return true;
        }

        public void Delete(Priority priority)
        {
            Delete(priority.Id);
        }

        public void Move(long id, long amount)
        {
            if (amount == 0) return;

            var priority = GetById(id);
            if (priority == null) return;

            // minus the amount because it is amount to move UP
            var newPosition = priority.Order - amount;
            var direction = amount > 0 ? 1 : -1;

            var prioritiesToUpdate = amount > 0
             ? Db.Select<Priority>(x => x.Order >= newPosition && x.Order < priority.Order)
             : Db.Select<Priority>(x => x.Order > priority.Order && x.Order <= newPosition);

            foreach (var priorityToShift in prioritiesToUpdate)
            {
                priorityToShift.Order += direction;
                Update(priorityToShift);
            }

            priority.Order = newPosition;
            Update(priority);
        }

        public void Dispose()
        {
            if (db != null)
                db.Dispose();
        }
    }
}