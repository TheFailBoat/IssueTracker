using System;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Utilities;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class IssueArgumentSecurityAttribute : ArgumentInterceptAttribute
    {
        public override object Process(object obj, ArgumentInterceptArgs args)
        {
            if (!(obj is IssueEntity)) throw new InvalidOperationException();

            var issue = obj as IssueEntity;

            var sec = args.Container.Resolve<ISecurityService>();
            var user = sec.GetCurrentUser();

            switch (args.MethodType)
            {
                case MethodType.Insert:
                    issue = ProcessCreate(issue, user, args);
                    break;
                case MethodType.Update:
                    issue = ProcessEdit(issue, user, args);
                    break;
                case MethodType.Delete:
                    issue = ProcessDelete(issue, user, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("args", "MethodType out of range");
            }

            return issue;
        }

        private static IssueEntity ProcessCreate(IssueEntity issue, UserEntity user, ArgumentInterceptArgs args)
        {
            issue.Id = 0;
            issue.ReporterId = user.Id;
            issue.CreatedAt = DateTime.UtcNow;
            issue.UpdatedAt = null;
            if (!user.IsAdmin)
            {
                issue.CustomerId = user.CustomerId;
            }

            return issue;
        }

        private static IssueEntity ProcessEdit(IssueEntity issue, UserEntity user, ArgumentInterceptArgs args)
        {
            var repository = args.Container.Resolve<IIssueRepository>();
            var original = repository.GetById(issue.Id);

            if (original == null || !original.CanUserAccess(user) || !(user.IsAdmin || user.IsMod))
            {
                args.Cancel = true;
                return null;
            }

            issue.CreatedAt = original.CreatedAt;
            issue.UpdatedAt = DateTime.UtcNow;
            issue.ReporterId = original.ReporterId;

            if (!user.IsAdmin)
            {
                issue.CustomerId = original.CustomerId;
            }

            return issue;
        }

        private static IssueEntity ProcessDelete(IssueEntity issue, UserEntity user, ArgumentInterceptArgs args)
        {
            var repository = args.Container.Resolve<IIssueRepository>();
            var original = repository.GetById(issue.Id);

            if (original == null || !original.CanUserAccess(user) || !(user.IsAdmin || user.IsMod))
            {
                args.Cancel = true;
                return null;
            }

            return issue;
        }

    }
}