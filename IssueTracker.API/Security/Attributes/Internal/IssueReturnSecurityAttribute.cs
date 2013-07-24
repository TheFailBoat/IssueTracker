using System;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Funq;
using IssueTracker.API.Entities;

namespace IssueTracker.API.Security.Attributes.Internal
{
    public class IssueReturnSecurityAttribute : ReturnInterceptAttribute
    {
        public override object Process(object obj, IInvocation invocation, Container container)
        {
            var sec = container.Resolve<ISecurityService>();
            var user = sec.GetCurrentUser();

            var entity = obj as IssueEntity;
            if (entity != null)
            {
                return ProcessEntity(entity, user);
            }

            var entities = obj as List<IssueEntity>;
            if (entities != null)
            {
                return ProcessList(entities, user);
            }

            throw new InvalidOperationException();
        }

        private IssueEntity ProcessEntity(IssueEntity issue, UserEntity user)
        {
            if (user.IsAdmin
                || (user.CustomerId != null && issue.CustomerId == user.CustomerId)
                || issue.ReporterId == user.Id)
            {
                return issue;
            }

            return null;
        }
        private List<IssueEntity> ProcessList(IEnumerable<IssueEntity> entities, UserEntity user)
        {
            return entities.Select(entity => ProcessEntity(entity, user)).Where(processed => processed != null).ToList();
        }
    }
}