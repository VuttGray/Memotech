using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Abstractions.Services;
using Memotech.Core.Application.Helpers;
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
    }
}
