using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Utilities;
using IssueTracker.Data;
using ServiceStack.OrmLite;

namespace IssueTracker.API.Repositories
{
    public interface IIssueRepository : IRepository<IssueEntity>
    {
        List<IssueEntity> GetByPage(int page, int pageSize);
    }

    internal class IssueRepository : BaseRepository, IIssueRepository
    {
        public IssueRepository(IDbConnectionFactory dbFactory)
            : base(dbFactory)
        {
        }

        public List<IssueEntity> GetAll()
        {
            return Db.Select<IssueEntity>();
        }

        public IssueEntity GetById(long id)
        {
            return Db.IdOrDefault<IssueEntity>(id);
        }

        public IssueEntity Add(IssueEntity issue)
        {
            issue.Id = 0;
            //issue.ReporterId = person.Id;
            issue.CreatedAt = DateTime.UtcNow;
            issue.UpdatedAt = null;

            //if (!person.IsEmployee)
            //{
            //issue.CustomerId = person.CustomerId;
            //}

            Db.Insert(issue);
            issue.Id = Db.GetLastInsertId();

            return issue;
        }

        public IssueEntity Update(IssueEntity issue)
        {
            //var person = personRepository.GetCurrent();
            //if (!person.IsEmployee) return null;

            return UpdateInternal(issue);
        }

        public IssueEntity UpdateInternal(IssueEntity issue)
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

        public bool SetUpdated(long id)
        {
            var issue = GetById(id);
            if (issue == null) return false;

            issue.UpdatedAt = DateTime.UtcNow;

            Db.Update(issue);

            return true;
        }

        public List<IssueEntity> GetByPage(int page, int pageSize)
        {
            return GetAll().OrderByDescending(x => x.CreatedAt).Skip(page * pageSize).Take(pageSize).ToList();
        }

        public bool Delete(long id)
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