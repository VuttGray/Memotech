using Memotech.Core.Domain;

namespace Memotech.Core.Abstractions.Repositories
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetById(int id);
        IEnumerable<T> GetByIds(IEnumerable<int> ids);
        void Add(T entity);
        void AddRange(List<T> records);
        void Edit(T entity);
    }
}
