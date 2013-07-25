using System;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Utilities;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class CommentArgumentSecurityAttribute : ArgumentInterceptAttribute
    {
        public override object Process(object obj, ArgumentInterceptArgs args)
        {
            if (!(obj is CommentEntity)) throw new InvalidOperationException();

            var comment = obj as CommentEntity;

            var sec = args.Container.Resolve<ISecurityService>();
            var user = sec.GetCurrentUser();

            switch (args.MethodType)
            {
                case MethodType.Insert:
                    comment = ProcessCreate(comment, user, args);
                    break;
                case MethodType.Update:
                    comment = ProcessEdit(comment, user, args);
                    break;
                case MethodType.Delete:
                    comment = ProcessDelete(comment, user, args);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("args", "MethodType out of range");
            }

            return comment;
        }

        private static CommentEntity ProcessCreate(CommentEntity comment, UserEntity user, ArgumentInterceptArgs args)
        {
            var issueRepository = args.Container.Resolve<IIssueRepository>();
            var issue = issueRepository.GetById(comment.IssueId);

            if (!issue.CanUserAccess(user))
            {
                args.Cancel = true;
                return null;
            }

            comment.Id = 0;
            comment.PersonId = user.Id;
            comment.CreatedAt = DateTime.UtcNow;
            comment.UpdatedAt = null;

            return comment;
        }

        private static CommentEntity ProcessEdit(CommentEntity comment, UserEntity user, ArgumentInterceptArgs args)
        {
            args.Cancel = true;
            return null;
        }

        private static CommentEntity ProcessDelete(CommentEntity comment, UserEntity user, ArgumentInterceptArgs args)
        {
            var issueRepository = args.Container.Resolve<IInsecureRepository<IIssueRepository>>();
            var repository = args.Container.Resolve<ICommentRepository>();
            var original = repository.GetById(comment.Id);

            if (original == null || !original.CanUserAccess(user, issueRepository) || !(user.IsAdmin || user.IsMod))
            {
                args.Cancel = true;
                return null;
            }

            return comment;
        }

    }
}