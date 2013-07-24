using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IssueTracker.API.Utilities
{
    public static class TypeUtils
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this MethodInfo method)
        {
            var items = method.GetCustomAttributes(typeof(T), true);
            foreach (var item in items)
            {
                yield return (T)item;
            }

            if (method.DeclaringType == null) yield break;

            foreach (var item in method.DeclaringType.GetInterfaces().SelectMany(intf => intf.GetMethods().Where(x => x.Name == method.Name).SelectMany(m => m.GetCustomAttributes<T>())))
            {
                yield return item;
            }
        }

        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo member)
        {
            var items = member.GetCustomAttributes(typeof(T), true);
            foreach (var item in items)
            {
                yield return (T)item;
            }

            if (member.DeclaringType == null) yield break;

            foreach (var item in member.DeclaringType.GetInterfaces().SelectMany(intf => intf.GetMembers().Where(x => x == member).SelectMany(m => m.GetCustomAttributes<T>())))
            {
                yield return item;
            }
        }
    }
}