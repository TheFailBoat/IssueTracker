using System.Collections.Generic;

namespace IssueTracker.API.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(long id);

        T Add(T status);
        void Update(T status);
        void Delete(long id);
        void Delete(T status);
    }
}