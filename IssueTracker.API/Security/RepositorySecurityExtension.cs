using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Castle.DynamicProxy;
using Fasterflect;
using Funq;
using IssueTracker.API.Security.Attributes;
using IssueTracker.API.Utilities;
using ServiceStack.MiniProfiler;

namespace IssueTracker.API.Security
{
    public static class RepositorySecurityExtension
    {
        public static IRegistration<IInsecureRepository<TService>> RegisterInsecureRepository<TService>(this Container container, Func<Container, TService> factory)
        {
            return container.Register<IInsecureRepository<TService>>(x => new InsecureRepository<TService>(factory(x)));
        }
        public static TService ResolveInsecure<TService>(this Container container)
        {
            return container.Resolve<IInsecureRepository<TService>>().Repository;
        }

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
                invocation.ReturnValue = invocation.Method.ReturnType.DefaultValue();

                var methodTypeAttr = invocation.MethodInvocationTarget.GetCustomAttributes<MethodTypeAttribute>();
                var methodType = methodTypeAttr.Single().Type;



                if (!ProcessMethod(invocation, methodType)) return;
                if (!ProcessArguments(invocation, methodType)) return;

                invocation.Proceed();

                ProcessReturn(invocation, methodType);
            }
        }

        private bool ProcessMethod(IInvocation invocation, MethodType methodType)
        {
            var methodIntercepts = invocation.MethodInvocationTarget.GetCustomAttributes<MethodInterceptAttribute>();
            foreach (var methodIntercept in methodIntercepts)
            {
                var args = new MethodInterceptArguments
                {
                    Container = container,
                    Invocation = invocation,
                    MethodType = methodType
                };

                methodIntercept.Process(args);

                if (args.Cancel)
                {
                    return false;
                }
            }

            return true;
        }

        private bool ProcessArguments(IInvocation invocation, MethodType methodType)
        {
            for (var index = 0; index < invocation.Arguments.Length; index++)
            {
                var argument = invocation.Arguments[index];
                if (argument == null) continue;

                var argIntercepts = argument.GetType().GetCustomAttributes<ArgumentInterceptAttribute>().ToList();


                var result = argument;
                foreach (var intercept in argIntercepts)
                {
                    var args = new ArgumentInterceptArgs
                    {
                        Container = container,
                        Invocation = invocation,
                        MethodType = methodType
                    };

                    result = intercept.Process(result, args);

                    if (args.Cancel) return false;
                }

                invocation.Arguments[index] = result;
            }

            return true;
        }

        private void ProcessReturn(IInvocation invocation, MethodType methodType)
        {
            if (invocation.ReturnValue == null) return;
            var origReturnType = invocation.ReturnValue.GetType();

            var methodReturnIntercepts = invocation.MethodInvocationTarget.GetCustomAttributes<ReturnInterceptAttribute>();
            var result = invocation.ReturnValue;
            foreach (var intercept in methodReturnIntercepts)
            {
                var args = new ReturnInterceptArgs
                {
                    Container = container,
                    Invocation = invocation,
                    MethodType = methodType
                };

                result = intercept.Process(result, args);
            }
            invocation.ReturnValue = result;

            if (!origReturnType.IsGenericType || origReturnType.GetGenericTypeDefinition() != typeof(List<>))
            {
                invocation.ReturnValue = ProcessReturnValue(invocation.ReturnValue, invocation, methodType);
            }
            else
            {
                invocation.ReturnValue = ProcessListReturn(invocation, methodType);
            }
        }

        private object ProcessReturnValue(object value, IInvocation invocation, MethodType methodType)
        {
            if (value == null) return null;

            var returnIntercepts = value.GetType().GetCustomAttributes<ReturnInterceptAttribute>();
            object result = value;
            foreach (ReturnInterceptAttribute intercept in returnIntercepts)
            {
                var args = new ReturnInterceptArgs
                {
                    Container = container,
                    Invocation = invocation,
                    MethodType = methodType
                };

                result = intercept.Process(result, args);
            }
            return result;
        }

        private object ProcessListReturn(IInvocation invocation, MethodType methodType)
        {
            var listType = invocation.ReturnValue.GetType();
            var listItemType = GetListItemType(listType);

            object processedList = CreateList(listItemType);
            var addMethod = GetListAdd(processedList.GetType(), listItemType);

            foreach (var item in (IEnumerable)invocation.ReturnValue)
            {
                var processedItem = ProcessReturnValue(item, invocation, methodType);

                if (processedItem != null)
                    addMethod(processedList, processedItem);
            }

            return processedList;
        }

        private static Type GetListItemType(Type listType)
        {
            return listType.GetGenericArguments()[0];
        }
        private static object CreateList(Type itemType)
        {
            var list = typeof(List<>).MakeGenericType(itemType);

            return list.CreateInstance();
        }
        private static Action<object, object> GetListAdd(Type listType, Type listItemType)
        {
            return (l, i) => listType.Method("Add", new[] { listItemType }).Call(l, new[] { i });
        }
    }
}