using System;
using Castle.DynamicProxy;
using Funq;

namespace IssueTracker.API.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class MethodInterceptAttribute : Attribute
    {
        public abstract void Process(MethodInterceptArguments args);
    }

    public class MethodInterceptArguments
    {
        public IInvocation Invocation { get; set; }
        public Container Container { get; set; }

        /// <summary>
        /// If set by the attribute, then the proxy will instantly return (with the value in Invocation.ReturnValue)
        /// </summary>
        public bool Cancel { get; set; }
    }
}