using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Domain;

namespace Memotech.Core.Application.Services
{
    public class BaseService<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<T> GetByIds(IEnumerable<int> ids)
        {
            return _repository.GetByIds(ids);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        public void AddRange(List<T> records)
        {
            _repository.AddRange(records);
        }

        public void Edit(T entity)
        {
            _repository.Edit(entity);
        }

        protected T TryGet(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var foundEntity = _repository.GetById(entity.Id);
            if (foundEntity == null)
                throw new KeyNotFoundException($"{typeof(T)} with Id={entity.Id} is not found");
            return foundEntity;
        }
    }
}
