using System;
using Castle.DynamicProxy;
using Funq;

namespace IssueTracker.API.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class ArgumentInterceptAttribute : Attribute
    {
        public abstract object Process(object obj, ArgumentInterceptArgs args);
    }

    public class ArgumentInterceptArgs
    {
        public IInvocation Invocation { get; set; }
        public Container Container { get; set; }

        public MethodType MethodType { get; set; }

        public bool Cancel { get; set; }
    }
}