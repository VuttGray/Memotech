using Memotech.BSA.Models;

namespace Memotech.BSA.Repositories
{
    public interface IRepository
    {
        List<Memo> GetAll();
        Memo? Get(int id);
        void Add(Memo memo);
        Task AddAsync(Memo memo);
    }
}
