using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memotech.Core.Application.Services
{
    public class TraceableService<T> : BaseService<T>, IRepository<T> where T: TraceableEntity
    {
        private string _userId = "Denis"; //TODO: Add service to get current user
        public TraceableService(IRepository<T> repository) : base(repository) { }

        public new void Add(T entity)
        {
            entity.CreatedAt = entity.UpdatedAt = DateTime.Now;
            entity.CreatedBy = entity.UpdatedBy = _userId;
            _repository.Add(entity);
        }

        public new void Edit(T entity)
        {
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = _userId;
            _repository.Edit(entity);
        }
    }
}
