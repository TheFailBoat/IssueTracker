using System.Collections.Generic;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes.Internal;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IIssueRepository : IRepository<IssueEntity>
    {
        List<IssueEntity> GetByCustomer(long customerId);
    }

    internal class IssueRepository : BaseRepository, IIssueRepository
    {
        public IssueRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        [RequireUserLoggedIn]
        public virtual List<IssueEntity> GetAll()
        {
            return Db.Select<IssueEntity>("SELECT * FROM Issues ORDER BY CASE WHEN UpdatedAt IS NULL THEN CreatedAt ELSE UpdatedAt END DESC");
        }
        [RequireUserLoggedIn]
        public virtual List<IssueEntity> GetByCustomer(long customerId)
        {
            return Db.Select<IssueEntity>("SELECT * FROM Issues ORDER BY CASE WHEN UpdatedAt IS NULL THEN CreatedAt ELSE UpdatedAt END DESC WHERE CustomerId = {0}", customerId);
        }

        [RequireUserLoggedIn]
        public virtual IssueEntity GetById(long id)
        {
            return Db.IdOrDefault<IssueEntity>(id);
        }

        [RequireUserLoggedIn]
        public virtual IssueEntity Add(IssueEntity issue)
        {
            Db.Insert(issue);
            issue.Id = Db.GetLastInsertId();

            return issue;
        }

        [RequireUserLoggedIn]
        public virtual IssueEntity Update(IssueEntity issue)
        {
            Db.Update(issue);

            //var changes = ChangeDetector.Diff(oldIssue, issue);
            //if (changes.Any(x => x.Column == "UpdatedAt")) changes.Remove(changes.Single(x => x.Column == "UpdatedAt"));

            //if (changes.Any())
            //{
            //    Db.Insert(new Comment
            //    {
            //        Message = "",
            //        IssueId = issue.Id,
            //        //PersonId = personRepository.GetCurrent().Id,
            //        CreatedAt = DateTime.UtcNow
            //    });
            //    var commentId = Db.GetLastInsertId();

            //    foreach (var change in changes)
            //    {
            //        change.CommentId = commentId;
            //        Db.Insert(change);
            //    }
            //}

            return issue;
        }

        [RequireUserLoggedIn]
        public virtual bool Delete(IssueEntity issue)
        {
            issue.Deleted = true;
            Db.Update(issue);

            return true;
        }
    }
}