using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memotech.Core.Abstractions.Services
{
    public interface IMemoService : IRepository<Memo>
    {
        void MarkAsStudied(Memo memo);
        void ResetStatistics(Memo memo);
    }
}
