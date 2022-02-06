using Memotech.BSA.Models;

namespace Memotech.BSA.Repositories
{
    public interface IRepository
    {
        List<Memo> GetAll();
        void Add(Memo memo);
    }
}
