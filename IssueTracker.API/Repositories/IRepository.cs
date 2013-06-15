using System.Collections.Generic;

namespace IssueTracker.API.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(long id);

        T Add(T status);
        T Update(T status);
        bool Delete(long id);
        bool Delete(T status);
    }
}