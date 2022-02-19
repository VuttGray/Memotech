using Memotech.BSA.Data.Helpers;
using Memotech.Core.Application.Extentions;
using Memotech.Core.Domain;
using Microsoft.AspNetCore.Components;

namespace Memotech.BSA.Pages.MemoPages
{
    public partial class MemoForm
    {
        [Parameter] public Memo Memo { get; set; } = new();
        [Parameter] public EventCallback OnValidSubmit { get; set; }

        DetModes detMode;
        string? imageUrl;
        readonly Dictionary<int, string> memoTypes = typeof(MemoTypes).GetDictionary();

        protected override void OnInitialized()
        {
            imageUrl = Memo.Image;
            Memo.Image = null;
            detMode = Memo.Id > 0 ? DetModes.Edit : DetModes.AddNew;
        }

        private void InputFileSelectorHandler(string imageBase64)
        {
            Memo.Image = imageBase64;
            imageUrl = null;
        }
    }
}
