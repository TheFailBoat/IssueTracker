using System;
using Castle.DynamicProxy;
using Funq;

namespace IssueTracker.API.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ReturnInterceptAttribute : Attribute
    {
        public abstract object Process(object obj, ReturnInterceptArgs args);
    }

    public class ReturnInterceptArgs
    {
        public IInvocation Invocation { get; set; }
        public Container Container { get; set; }

        public MethodType MethodType { get; set; }
    }
}