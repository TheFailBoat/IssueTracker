using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Repositories;
using IssueTracker.API.Utilities;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class CommentChangeReturnSecurityAttribute : ReturnInterceptAttribute
    {
        public override object Process(object obj, ReturnInterceptArgs args)
        {
            var sec = args.Container.Resolve<ISecurityService>();
            var user = sec.GetCurrentUser();

            var entity = obj as CommentChangeEntity;
            if (entity != null)
            {
                return ProcessEntity(entity, user, args);
            }

            var entities = obj as List<CommentChangeEntity>;
            if (entities != null)
            {
                return ProcessList(entities, user, args);
            }

            throw new InvalidOperationException();
        }

        private static CommentChangeEntity ProcessEntity(CommentChangeEntity change, UserEntity user, ReturnInterceptArgs args)
        {
            var issueRepository = args.Container.Resolve<IInsecureRepository<IIssueRepository>>();
            var commentRepository = args.Container.Resolve<ICommentRepository>();
            var comment = commentRepository.GetById(change.CommentId);

            return comment.CanUserAccess(user, issueRepository) ? change : null;
        }

        private static List<CommentChangeEntity> ProcessList(IEnumerable<CommentChangeEntity> entities, UserEntity user, ReturnInterceptArgs args)
        {
            return entities.Select(entity => ProcessEntity(entity, user, args)).Where(processed => processed != null).ToList();
        }
    }
}