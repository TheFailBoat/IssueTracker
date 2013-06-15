using System.Collections.Generic;

namespace IssueTracker.API.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T GetById(long id);

        T Add(T comment);
        T Update(T comment);
        bool Delete(long id);
        bool Delete(T comment);
    }
}