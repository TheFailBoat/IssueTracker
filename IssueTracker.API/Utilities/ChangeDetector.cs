using System.Collections.Generic;
using System.Reflection;
using IssueTracker.Data;
using IssueTracker.Data.Comments;

namespace IssueTracker.API.Utilities
{
    public static class ChangeDetector
    {
        public static List<CommentChange> Diff<T>(T oldObj, T newObj)
        {
            var changes = new List<CommentChange>();

            foreach (var member in typeof(T).GetProperties())
            {
                var oldValue = member.GetValue(oldObj);
                var newValue = member.GetValue(newObj);

                var change = new CommentChange { Column = member.Name, OldValue = oldValue, NewValue = newValue };

                if (oldValue != null && !oldValue.Equals(newValue))
                {
                    changes.Add(change);
                }
                else if (newValue != null && !newValue.Equals(oldValue))
                {
                    changes.Add(change);
                }
            }

            return changes;
        }
    }
}