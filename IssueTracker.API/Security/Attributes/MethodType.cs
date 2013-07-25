using System;

namespace IssueTracker.API.Security.Attributes
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class MethodTypeAttribute : Attribute
    {
        public MethodType Type { get; private set; }

        public MethodTypeAttribute(MethodType type)
        {
            Type = type;
        }
    }

    public enum MethodType
    {
        Get,
        Insert,
        Update,
        Delete
    }
}