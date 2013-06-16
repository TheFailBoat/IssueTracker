using System.Collections.Generic;

namespace IssueTracker.API.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(long id);

        T Add(T item);
        void Update(T item);
        void Delete(long id);
        void Delete(T item);
    }
}