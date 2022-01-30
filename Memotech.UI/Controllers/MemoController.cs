using Microsoft.AspNetCore.Mvc;
using Memotech.UI.Services;
using Memotech.UI.Models;

namespace Memotech.UI.Controllers
{
    [Route("api/v1/[controller]")]
    public class MemoController : Controller
    {
        public JsonFileMemoService MemoService { get; }

        public MemoController(JsonFileMemoService memoService)
        {
            MemoService = memoService;
        }

        [HttpGet]
        public IEnumerable<Memo> Get()
        {
            return MemoService.GetAll();
        }
    }
}
