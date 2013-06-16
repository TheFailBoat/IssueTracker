using System.Collections.Generic;

namespace IssueTracker.API.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(long id);

        T Add(T item);
        T Update(T item);
        bool Delete(long id);
    }
}