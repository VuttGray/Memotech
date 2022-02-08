using Memotech.BSA.Models;
using Memotech.BSA.Repositories;
using Microsoft.AspNetCore.Components;

namespace Memotech.BSA.Pages
{
    public partial class Index
    {
        [Inject] IRepository? Repository { get; set; }

        List<Memo>? memos;
        readonly Random rnd = new(DateTime.Now.Millisecond);
        Memo? randomMemo;

        protected override void OnInitialized()
        {
            memos = Repository?.GetAll() ?? new List<Memo>();

            if (memos.Count > 0)
            {
                randomMemo = memos[rnd.Next(0, memos.Count)];
            }
        }

        void SwitchRandomMemo()
        {
            randomMemo = GetRandomMemo();
        }

        Memo GetRandomMemo()
        {
            if (memos != null && memos.Count > 0)
            {
                return memos[rnd.Next(0, memos.Count)];
            }

            return new Memo() { Term = "No memo to display", Meaning = "" };
        }
    }
}
