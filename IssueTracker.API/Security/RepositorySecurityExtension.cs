using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Castle.DynamicProxy;
using Funq;
using IssueTracker.API.Security.Attributes;
using IssueTracker.API.Utilities;
using ServiceStack.MiniProfiler;

namespace IssueTracker.API.Security
{
    public static class RepositorySecurityExtension
    {
        public static IRegistration<TService> RegisterSecureRepository<TService>(this Container container, Func<Container, TService> factory)
        {
            var pg = new ProxyGenerator();
            var options = new ProxyGenerationOptions();

            var interceptors = new List<IInterceptor> { new RepositorySecurityInterceptor(container) };

            var type = typeof(TService);
            return type.IsInterface
                ? container.Register(x => (TService)pg.CreateInterfaceProxyWithTarget(type, factory(x), options, interceptors.ToArray()))
                : container.Register(x => (TService)pg.CreateClassProxyWithTarget(type, factory(x), options, interceptors.ToArray()));
        }
    }

    public class RepositorySecurityInterceptor : IInterceptor
    {
        private readonly Container container;

        public RepositorySecurityInterceptor(Container container)
        {
            this.container = container;
        }

        public void Intercept(IInvocation invocation)
        {
            using (Profiler.Current.Step("Applying Repository Security"))
            {
                if (!ProcessMethod(invocation)) return;
                ProcessArguments(invocation);

                invocation.Proceed();

                ProcessReturn(invocation);
            }
        }

        private bool ProcessMethod(IInvocation invocation)
        {
            var methodIntercepts = invocation.MethodInvocationTarget.GetCustomAttributes<MethodInterceptAttribute>();
            foreach (var methodIntercept in methodIntercepts)
            {
                var args = new MethodInterceptArguments { Container = container, Invocation = invocation };

                methodIntercept.Process(args);

                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private void ProcessArguments(IInvocation invocation)
        {
            for (var index = 0; index < invocation.Arguments.Length; index++)
            {
                var argument = invocation.Arguments[index];
                if (argument == null) continue;

                var argIntercepts = argument.GetType().GetCustomAttributes<ArgumentInterceptAttribute>();

                invocation.Arguments[index] = argIntercepts.Aggregate(argument, (current, argIntercept) => argIntercept.Process(current, invocation, container));
            }
        }

        private void ProcessReturn(IInvocation invocation)
        {
            if (invocation.ReturnValue == null) return;
            var origReturnType = invocation.ReturnValue.GetType();

            var methodReturnIntercepts = invocation.MethodInvocationTarget.GetCustomAttributes<ReturnInterceptAttribute>();
            invocation.ReturnValue = methodReturnIntercepts.Aggregate(invocation.ReturnValue, (current, returnIntercept) => returnIntercept.Process(current, invocation, container));

            if (origReturnType.IsGenericType && origReturnType.GetGenericTypeDefinition() == typeof(List<>))
            {
                invocation.ReturnValue = ProcessListReturn(invocation);
            }
            else
            {
                invocation.ReturnValue = ProcessReturnValue(invocation.ReturnValue, invocation);
            }
        }

        private object ProcessReturnValue(object value, IInvocation invocation)
        {
            if (value == null) return null;

            var returnIntercepts = value.GetType().GetCustomAttributes<ReturnInterceptAttribute>();
            return returnIntercepts.Aggregate(value, (current, returnIntercept) => returnIntercept.Process(current, invocation, container));
        }

        private object ProcessListReturn(IInvocation invocation)
        {
            var listType = invocation.ReturnValue.GetType();
            var listItemType = GetListItemType(listType);

            object processedList = CreateList(listItemType);
            var addMethod = GetListAdd(processedList.GetType(), listItemType);

            //TODO loop 
            foreach (var item in (IEnumerable)invocation.ReturnValue)
            {
                var processedItem = ProcessReturnValue(item, invocation);

                if (processedItem != null)
                    addMethod(processedList, processedItem);
            }

            return processedList;
        }

        private Type GetListItemType(Type listType)
        {
            return listType.GetGenericArguments()[0];
        }
        private object CreateList(Type itemType)
        {
            var list = typeof(List<>).MakeGenericType(itemType);

            return Activator.CreateInstance(list);
        }
        private Action<object, object> GetListAdd(Type listType, Type listItemType)
        {
            var param1 = Expression.Parameter(typeof(object), "list");
            var param2 = Expression.Parameter(typeof(object), "x");

            var tp1 = Expression.Convert(param1, listType);
            var tp2 = Expression.Convert(param2, listItemType);

            return (Action<object, object>)Expression.Lambda(Expression.Call(tp1, "Add", null, tp2), param1, param2).Compile();
        }

    }
}