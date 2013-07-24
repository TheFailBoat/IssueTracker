using System.Collections.Generic;
using IssueTracker.API.Entities;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IStatusRepository : IRepository<StatusEntity>
    {
        void Move(long id, long amount);
    }

    internal class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public List<StatusEntity> GetAll()
        {
            return Db.Select<StatusEntity>();
        }

        public StatusEntity GetById(long id)
        {
            return Db.IdOrDefault<StatusEntity>(id);
        }

        public StatusEntity Add(StatusEntity status)
        {
            //if (!personRepository.GetCurrent().IsEmployee)
            //{
            //    return null;
            //}

            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        public StatusEntity Update(StatusEntity status)
        {
            //if (!personRepository.GetCurrent().IsEmployee)
            //{
            //    return null;
            //}

            Db.Update(status);

            return status;
        }

        public bool Delete(long id)
        {
            //if (!personRepository.GetCurrent().IsEmployee)
            //{
            //    return false;
            //}

            Db.DeleteById<StatusEntity>(id);

            return true;
        }

        public void Move(long id, long amount)
        {
            if (amount == 0) return;

            var status = GetById(id);

            // minus the amount because it is amount to move UP
            var newPosition = status.Order - amount;
            var direction = amount > 0 ? 1 : -1;

            var prioritiesToUpdate = amount > 0
             ? Db.Select<StatusEntity>(x => x.Order >= newPosition && x.Order < status.Order)
             : Db.Select<StatusEntity>(x => x.Order > status.Order && x.Order <= newPosition);

            foreach (var statusToShift in prioritiesToUpdate)
            {
                statusToShift.Order += direction;
                Update(statusToShift);
            }

            status.Order = newPosition;
            Update(status);
        }
    }
}