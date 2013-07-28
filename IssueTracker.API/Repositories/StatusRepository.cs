using System.Collections.Generic;
using System.Data;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IStatusRepository : IRepository<StatusEntity>
    {
        void Move(long id, long amount);
    }

    internal class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(IDbConnection db)
            : base(db)
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

        [RequirePermission(RequiresAdmin = true)]
        public StatusEntity Add(StatusEntity status)
        {
            status.Id = 0;

            Db.Insert(status);
            status.Id = Db.GetLastInsertId();

            return status;
        }

        [RequirePermission(RequiresAdmin = true)]
        public StatusEntity Update(StatusEntity status)
        {

            Db.Update(status);

            return status;
        }

        [RequirePermission(RequiresAdmin = true)]
        public bool Delete(StatusEntity status)
        {
            Db.DeleteById<StatusEntity>(status.Id);

            return true;
        }

        [RequirePermission(RequiresAdmin = true)]
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