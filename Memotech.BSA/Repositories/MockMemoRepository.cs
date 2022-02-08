using Memotech.BSA.Models;

namespace Memotech.BSA.Repositories
{
    public class MockMemoRepository : IRepository
    {
        readonly List<Memo> _memoList;

        public MockMemoRepository()
        {
            _memoList = new List<Memo>
            {
                new Memo() { Id = 1, Term = "game", Meaning = "игра" },
                new Memo() { Id = 2, Term = "farm", Meaning = "ферма" },
                new Memo() { Id = 3, Term = "human", Meaning = "человек" },
                new Memo() { Id = 4, Term = "witcher", Meaning = "ведьмак" },
            };
        }

        public void Add(Memo memo)
        {
            _memoList.Add(memo);
        }

        public Task AddAsync(Memo memo)
        {
            throw new NotImplementedException();
        }

        public List<Memo> GetAll()
        {
            return _memoList;
        }

        public Memo Get(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }
    }
}
