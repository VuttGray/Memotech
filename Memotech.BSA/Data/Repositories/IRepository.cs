using Memotech.BSA.Data.Models;

namespace Memotech.BSA.Data.Repositories
{
    public interface IRepository
    {
        List<Memo> GetAll();
        Memo? Get(int id);
        void Add(Memo memo);
        void Edit(Memo memo);
        void MarkAsStudied(Memo memo);
        void ResetStatistics(Memo memo);
    }
}
