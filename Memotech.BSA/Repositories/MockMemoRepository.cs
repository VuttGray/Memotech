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
                new Memo() { Term = "game", Meaning = "игра" },
                new Memo() { Term = "farm", Meaning = "ферма" },
                new Memo() { Term = "human", Meaning = "человек" },
                new Memo() { Term = "witcher", Meaning = "ведьмак" },
            };
        }

        public void Add(Memo memo)
        {
            _memoList.Add(memo);
        }

        public List<Memo> GetAll()
        {
            return _memoList;
        }
    }
}
