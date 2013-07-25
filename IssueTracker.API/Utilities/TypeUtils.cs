using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Fasterflect;

namespace IssueTracker.API.Utilities
{
    public static class TypeUtils
    {
        public static IEnumerable<T> GetCustomAttributes<T>(this MethodInfo method) where T : Attribute
        {
            if (method == null) yield break;

            var items = method.GetCustomAttributes(typeof(T), true);
            foreach (var item in items)
            {
                yield return (T)item;
            }

            if (method.DeclaringType == null) yield break;

            foreach (var item in method.DeclaringType.GetInterfaces().SelectMany(intf => intf.GetMethods().Where(x => x.CompareMethod(method)).SelectMany(m => m.GetCustomAttributes<T>())))
            {
                yield return item;
            }
        }
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo member)
        {
            if (member == null) yield break;

            var items = member.GetCustomAttributes(typeof(T), true);
            foreach (var item in items)
            {
                yield return (T)item;
            }

            if (member.DeclaringType == null) yield break;

            foreach (var item in member.DeclaringType.GetInterfaces().SelectMany(intf => intf.GetMembers().Where(x => x.Name == member.Name).SelectMany(m => m.GetCustomAttributes<T>())))
            {
                yield return item;
            }
        }


        public static bool CompareMethod(this MethodInfo m1, MethodInfo m2)
        {
            if (m1.Name != m2.Name) return false;

            var m1P = m1.Parameters();
            var m2P = m2.Parameters();

            return m1P.Count == m2P.Count && m1P.Zip(m2P, (a, b) => a.ParameterType == b.ParameterType).All(x => x);
        }

        public static object DefaultValue(this Type type)
        {
            return type.IsValueType ? type.CreateInstance() : null;
        }
    }
}