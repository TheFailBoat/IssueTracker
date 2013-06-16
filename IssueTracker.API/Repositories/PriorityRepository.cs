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
        private IDbConnection db;

        public PriorityRepository(IDbConnectionFactory dbFactory)
        {
            this.dbFactory = dbFactory;
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

        public Priority Add(Priority status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public void Update(Priority status)
        {
            Db.Update(status);
        }

        public void Delete(long id)
        {
            Db.DeleteById<Priority>(id);
        }

        public void Delete(Priority status)
        {
            Delete(status.Id);
        }

        public void Move(long id, long amount)
        {
            if (amount == 0) return;

            var priority = GetById(id);

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