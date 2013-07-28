using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Utilities;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class CommentReturnSecurityAttribute : ReturnInterceptAttribute
    {
        public override object Process(object obj, ReturnInterceptArgs args)
        {
            var sec = args.Container.Resolve<ISecurityService>();
            var user = sec.GetCurrentUser();

            var entity = obj as CommentEntity;
            if (entity != null)
            {
                var comment = ProcessEntity(entity, user, args);

                if (comment != null && args.MethodType != MethodType.Get)
                {
                    // update issue UpdatedAt
                    var issueRepository = args.Container.Resolve<IInsecureRepository<IIssueRepository>>();
                    var issue = issueRepository.Repository.GetById(comment.IssueId);

                    issue.UpdatedAt = DateTime.UtcNow;

                    issueRepository.Repository.Update(issue);
                }

                return comment;
            }

            var entities = obj as List<CommentEntity>;
            if (entities != null)
            {
                return ProcessList(entities, user, args);
            }

            throw new InvalidOperationException();
        }

        private static CommentEntity ProcessEntity(CommentEntity comment, UserEntity user, ReturnInterceptArgs args)
        {
            var repository = args.Container.Resolve<IInsecureRepository<IIssueRepository>>();

            return comment.CanUserAccess(user, repository) ? comment : null;
        }

        private static List<CommentEntity> ProcessList(IEnumerable<CommentEntity> entities, UserEntity user, ReturnInterceptArgs args)
        {
            return entities.Select(entity => ProcessEntity(entity, user, args)).Where(processed => processed != null).ToList();
        }
    }
}