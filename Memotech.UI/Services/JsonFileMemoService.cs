using Memotech.UI.Models;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Memotech.UI.Services
{
    public class JsonFileMemoService
    {
        const string JsonFile = "memos.json";
        private readonly IWebHostEnvironment _hostEnvironment;

        public JsonFileMemoService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }
        
        public IEnumerable<Memo> GetAll()
        {
            using var fileReader = File.OpenText(Path.Combine(_hostEnvironment.WebRootPath, "data", JsonFile));
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            var records = JsonSerializer.Deserialize<IEnumerable<Memo>>(fileReader.ReadToEnd(), options);
            if (records == null) 
                return new List<Memo>();
            return records;
        }

        public Memo? Get(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }
    }
}
