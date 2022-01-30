using Memotech.UI.Models;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Memotech.UI.Services
{
    public class JsonFileUnitService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public JsonFileUnitService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        
        public IEnumerable<Unit> GetUnits()
        {
            using var fileReader = File.OpenText(Path.Combine(_hostEnvironment.WebRootPath, "data","units.json"));
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var units = JsonSerializer.Deserialize<IEnumerable<Unit>>(fileReader.ReadToEnd(), options);
            if (units == null) 
                return new List<Unit>();
            return units;
        }

        public Unit? Get(int id)
        {
            return GetUnits().FirstOrDefault(u => u.ID == id);
        }
    }
}
