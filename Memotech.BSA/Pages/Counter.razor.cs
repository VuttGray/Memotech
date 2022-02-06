using Memotech.BSA.Models;
using Memotech.BSA.Services;
using Microsoft.AspNetCore.Components;

namespace Memotech.BSA.Pages
{
    public partial class Counter
    {
        [Inject] 
        SingletonMemoService? Singleton { get; set; }
        [Inject] 
        TransientMemoService? Transient { get; set; }
        [CascadingParameter] 
        public AppStyle? AppStyle { get; set; }

        private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
            if (Singleton != null) Singleton.Value = currentCount;
            if (Transient != null) Transient.Value = currentCount;
        }
    }
}
