using Memotech.BSA.Models;
using System.Text.Json;

namespace Memotech.BSA.Repositories
{
    public class JsonMemoRepository : IRepository
    {
        const string JsonFile = "memos.json";
        readonly IWebHostEnvironment _hostEnvironment;
        readonly List<Memo> _memoList;

        public JsonMemoRepository(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;

            using var fileReader = File.OpenText(Path.Combine(_hostEnvironment.WebRootPath, "data", JsonFile));
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _memoList = JsonSerializer.Deserialize<List<Memo>>(fileReader.ReadToEnd(), options) ?? new List<Memo>();
        }

        public List<Memo> GetAll()
        {
            return _memoList;
        }

        public Memo? Get(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }

        public void Add(Memo memo)
        {
            _memoList.Add(memo);
        }
    }
}
