using System;
using Castle.DynamicProxy;
using Funq;

namespace IssueTracker.API.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class ArgumentInterceptAttribute : Attribute
    {
        public abstract object Process(object obj, IInvocation invocation, Container container);
    }
}