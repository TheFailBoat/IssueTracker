using System.Collections.Generic;
using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IPriorityRepository : IRepository<PriorityEntity>
    {
        void Move(long id, long amount);
    }

    internal class PriorityRepository : BaseRepository, IPriorityRepository
    {
        public PriorityRepository(IDbConnectionFactory dbFactory) : base(dbFactory)
        {
        }

        public List<PriorityEntity> GetAll()
        {
            return Db.Select<PriorityEntity>();
        }

        public PriorityEntity GetById(long id)
        {
            return Db.IdOrDefault<PriorityEntity>(id);
        }

        public PriorityEntity Add(PriorityEntity priority)
        {
            priority.Id = 0;

            Db.Insert(priority);
            priority.Id = Db.GetLastInsertId();

            return priority;
        }

        public PriorityEntity Update(PriorityEntity priority)
        {
            Db.Update(priority);

            return priority;
        }

        public bool Delete(long id)
        {
            Db.DeleteById<PriorityEntity>(id);

            return true;
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
             ? Db.Select<PriorityEntity>(x => x.Order >= newPosition && x.Order < priority.Order)
             : Db.Select<PriorityEntity>(x => x.Order > priority.Order && x.Order <= newPosition);

            foreach (var priorityToShift in prioritiesToUpdate)
            {
                priorityToShift.Order += direction;
                Update(priorityToShift);
            }

            priority.Order = newPosition;
            Update(priority);
        }
    }
}