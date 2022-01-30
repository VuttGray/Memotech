using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Memotech.UI.Services;
using Memotech.UI.Models;

namespace Memotech.UI.Controllers
{
    [Route("api/v1/[controller]")]
    public class UnitController : Controller
    {
        public JsonFileUnitService UnitService { get; }

        public UnitController(JsonFileUnitService unitService)
        {
            UnitService = unitService;
        }

        [HttpGet]
        // GET: UnitController
        public IEnumerable<Unit> Get()
        {
            return UnitService.GetUnits();
        }
    }
}
