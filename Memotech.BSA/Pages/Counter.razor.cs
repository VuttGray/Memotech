using Microsoft.AspNetCore.Components;
using Memotech.BSA.Data.Services;
using Memotech.BSA.Data.Models;

namespace Memotech.BSA.Pages
{
    public partial class Counter
    {
        [Inject] 
        SingletonMemoService? Singleton { get; set; }
        [Inject] 
        TransientMemoService? Transient { get; set; }
        [CascadingParameter]
        public AppStyle AppStyle { get; set; } = new();

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
            if (Singleton != null) Singleton.Value = currentCount;
            if (Transient != null) Transient.Value = currentCount;
        }
    }
}
