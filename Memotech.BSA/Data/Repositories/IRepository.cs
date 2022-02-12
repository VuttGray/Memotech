using Memotech.BSA.Data.Models;

namespace Memotech.BSA.Data.Repositories
{
    public interface IRepository
    {
        List<Memo> GetAll();
        Memo? Get(int id);
        void Add(Memo memo);
        Task AddAsync(Memo memo);
    }
}
