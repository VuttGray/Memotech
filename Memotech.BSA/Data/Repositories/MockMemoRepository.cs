﻿using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Domain;

namespace Memotech.BSA.Data.Repositories
{
    public class MockMemoRepository : IRepository<Memo>
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
            if (memo == null)
                throw new ArgumentNullException(nameof(memo));
            _memoList.Add(memo);
        }

        public void Edit(Memo memo)
        {
            if (memo == null)
                throw new ArgumentNullException(nameof(memo));
            var entity = _memoList.FirstOrDefault(u => u.Id == memo.Id);
            if (entity == null)
                throw new KeyNotFoundException($"Memo with Id={memo.Id} is not found");
            entity = memo;
        }

        public IEnumerable<Memo> GetAll()
        {
            return _memoList;
        }

        public Memo? GetById(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Memo> GetByIds(IEnumerable<int> ids)
        {
            return GetAll().Where(u => ids.Contains(u.Id));
        }

        public void MarkAsStudied(Memo memo)
        {
            throw new NotImplementedException();
        }

        public void ResetStatistics(Memo memo)
        {
            throw new NotImplementedException();
        }

        public void AddRange(List<Memo> records)
        {
            throw new NotImplementedException();
        }

        public Memo? GetNext(Memo entity)
        {
            throw new NotImplementedException();
        }

        public Memo? GetPrev(Memo entity)
        {
            throw new NotImplementedException();
        }
    }
}
