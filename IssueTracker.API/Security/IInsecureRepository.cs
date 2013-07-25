using System;
using Funq;

namespace IssueTracker.API.Security
{
    public interface IInsecureRepository<out T>
    {
        T Repository { get; }
    }

    internal class InsecureRepository<T> : IInsecureRepository<T>
    {
        public InsecureRepository(T repository)
        {
            Repository = repository;
        }

        public T Repository { get; private set; }
    }
}