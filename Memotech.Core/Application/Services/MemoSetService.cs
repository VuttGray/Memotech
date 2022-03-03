using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Abstractions.Services;
using Memotech.Core.Domain;

namespace Memotech.Core.Application.Services
{
    public class MemoSetService : BaseService<MemoSet>, IMemoSetService
    {
        public MemoSetService(IRepository<MemoSet> repository) : base(repository) { }
    }
}
