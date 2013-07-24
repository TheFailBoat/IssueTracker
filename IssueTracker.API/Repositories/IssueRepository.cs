using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Security.Attributes.Internal;
using IssueTracker.API.Utilities;
using IssueTracker.Data;
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
            issue.Id = 0;
            //issue.ReporterId = person.Id;
            issue.CreatedAt = DateTime.UtcNow;
            issue.UpdatedAt = issue.CreatedAt;

            //if (!person.IsEmployee)
            //{
            //issue.CustomerId = person.CustomerId;
            //}

            Db.Insert(issue);
            issue.Id = Db.GetLastInsertId();

            return issue;
        }

        [RequireUserLoggedIn]
        public virtual IssueEntity Update(IssueEntity issue)
        {
            //var person = personRepository.GetCurrent();
            //if (!person.IsEmployee) return null;

            return UpdateInternal(issue);
        }

        [RequireUserLoggedIn]
        public virtual IssueEntity UpdateInternal(IssueEntity issue)
        {
            var oldIssue = GetById(issue.Id);
            if (oldIssue == null) return null;

            issue.ReporterId = oldIssue.ReporterId;
            issue.CreatedAt = oldIssue.CreatedAt;
            issue.UpdatedAt = DateTime.UtcNow;

            Db.Update(issue);

            var changes = ChangeDetector.Diff(oldIssue, issue);
            if (changes.Any(x => x.Column == "UpdatedAt")) changes.Remove(changes.Single(x => x.Column == "UpdatedAt"));

            if (changes.Any())
            {
                Db.Insert(new Comment
                              {
                                  Message = "",
                                  IssueId = issue.Id,
                                  //PersonId = personRepository.GetCurrent().Id,
                                  CreatedAt = DateTime.UtcNow
                              });
                var commentId = Db.GetLastInsertId();

                foreach (var change in changes)
                {
                    change.CommentId = commentId;
                    Db.Insert(change);
                }
            }

            return issue;
        }

        [RequireUserLoggedIn]
        public virtual bool SetUpdated(long id)
        {
            var issue = GetById(id);
            if (issue == null) return false;

            issue.UpdatedAt = DateTime.UtcNow;

            Db.Update(issue);

            return true;
        }

        [RequireUserLoggedIn]
        public virtual bool Delete(long id)
        {
            var oldIssue = GetById(id);
            if (oldIssue == null) return false;

            //var person = personRepository.GetCurrent();
            //if (!person.IsEmployee) return false;

            Db.DeleteById<IssueEntity>(id);

            return true;
        }
    }
}