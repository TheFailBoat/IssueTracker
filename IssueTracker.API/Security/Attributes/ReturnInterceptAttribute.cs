using System;
using Castle.DynamicProxy;
using Funq;

namespace IssueTracker.API.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ReturnInterceptAttribute : Attribute
    {
        public abstract object Process(object obj, IInvocation invocation, Container container);
    }
}