using System;
using System.Collections.Generic;
using System.Linq;
using IssueTracker.API.Entities;
using IssueTracker.API.Utilities;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class IssueReturnSecurityAttribute : ReturnInterceptAttribute
    {
        public override object Process(object obj, ReturnInterceptArgs args)
        {
            var sec = args.Container.Resolve<ISecurityService>();
            var user = sec.GetCurrentUser();

            var entity = obj as IssueEntity;
            if (entity != null)
            {
                if (args.MethodType == MethodType.Update || args.MethodType == MethodType.Delete)
                {
                    //TODO log changes
                }

                return ProcessEntity(entity, user);
            }

            var entities = obj as List<IssueEntity>;
            if (entities != null)
            {
                return ProcessList(entities, user);
            }

            throw new InvalidOperationException();
        }

        private static IssueEntity ProcessEntity(IssueEntity issue, UserEntity user)
        {
            return issue.CanUserAccess(user) ? issue : null;
        }

        private static List<IssueEntity> ProcessList(IEnumerable<IssueEntity> entities, UserEntity user)
        {
            return entities.Select(entity => ProcessEntity(entity, user)).Where(processed => processed != null).ToList();
        }
    }
}