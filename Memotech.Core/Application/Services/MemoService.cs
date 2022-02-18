using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Abstractions.Services;
using Memotech.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memotech.Core.Application.Services
{
    public class MemoService : TraceableService<Memo>, IMemoService
    {
        public MemoService(IRepository<Memo> repository) : base(repository) { }

        public void ResetStatistics(Memo memo)
        {
            var entity = TryGet(memo);
            entity.IsStudied = false;
            entity.StudyStages.Clear();
            _repository.Edit(entity);
        }

        public void MarkAsStudied(Memo memo)
        {
            var entity = TryGet(memo);
            entity.IsStudied = true;
            _repository.Edit(entity);
        }
    }
}
