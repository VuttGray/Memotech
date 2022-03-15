using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Abstractions.Services;
using Memotech.Core.Application.Helpers;
using Memotech.Core.Domain;

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
            entity.StudyPercentage = 0;
            _repository.Edit(entity);
        }

        public void MarkAsStudied(Memo memo)
        {
            var entity = TryGet(memo);
            entity.IsStudied = true;
            _repository.Edit(entity);
        }

        private bool IsReadyToStudy(Memo memo)
        {
            if (memo.IsStudied) 
                return false;
            if (memo.StudyStages.Any()
                && memo.StudyStages.Max(s => s.Date) > DateTime.Now.AddMinutes(-10)) 
                return false;
            return true;
        }

        public Memo? GetMemoToStudy()
        {
            var memos = _repository.GetAll().Where(e => IsReadyToStudy(e)).ToList();
            if (!memos.Any()) return null;

            var rnd = new Random(DateTime.Now.Millisecond);
            var index = rnd.Next(0, memos.Count - 1);
            
            return memos[index];
        }

        public void AddStudyStage(Memo memo, StudyStages stage)
        {
            var entity = TryGet(memo);
            entity.StudyStages.Add(
                new StudyStage
                {
                    Date = DateTime.Now,
                    IsSuccessful = stage == StudyStages.Know
                });
            
            var sucessfulStages = GetLastSucessfulStages(entity);
            entity.StudyPercentage = (int)((100.0 * sucessfulStages) / StudySettings.StudyStagesLimit);
            if (entity.StudyPercentage > 100) entity.StudyPercentage = 100;
            if (entity.StudyPercentage == 100) entity.IsStudied = true;

            _repository.Edit(entity);
        }

        private int GetLastSucessfulStages(Memo memo)
        {
            var counter = 0;
            foreach (var stage in memo.StudyStages)
            {
                if (stage.IsSuccessful) 
                    counter++;
                else
                    counter = counter == 0 ? 0 : counter - 1;
            }
            return counter;
        }

        public void AddRangeFromText(string memosText, MemoSet? memoSet = null)
        {
            if (string.IsNullOrEmpty(memosText)) 
                throw new ArgumentException($"Text provided to import memos is empty");

            var memos = new List<Memo>();

            var counter = 0;
            foreach (var line in memosText.Split("\n"))
            {
                counter++;
                if (string.IsNullOrEmpty(line)) 
                    continue;
                var values = line.Split("\t");
                if (values.Length < 2) 
                    throw new ArgumentException($"Line #{counter} contains wrong data: the values should be separated by tab symbols and the row should have two values (term and meaning) at least");

                var record = new Memo { Term = values[0], Meaning = values[1] };
                if (values.Length > 2)
                    record.Info = values[2];
                if (memoSet != null)
                    record.MemoSetsList.Add(memoSet.Id);
                
                memos.Add(record);
            }

            _repository.AddRange(memos);
        }
    }
}
