using Memotech.Core.Domain;

namespace Memotech.Core.Abstractions.Repositories
{
    public interface IRepository<T>
    {
        List<T> GetAll();
        T? Get(int id);
        void Add(T entity);
        void AddRange(List<T> records);
        void Edit(T entity);
    }
}
