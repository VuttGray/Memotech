using Memotech.BSA.Data.Models;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Memotech.BSA.Data.Repositories
{
    public class JsonMemoRepository : IRepository
    {
        const string _jsonFile = "memos.json";
        readonly string _dbFilePath;
        IWebHostEnvironment _hostEnvironment;
        List<Memo> _memoList = new();

        public JsonMemoRepository(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _dbFilePath = Path.Combine(_hostEnvironment.WebRootPath, "data", _jsonFile);
            LoadData();
        }

        private void LoadData()
        {
            using var fileStream = File.OpenText(_dbFilePath);
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _memoList = JsonSerializer.Deserialize<List<Memo>>(fileStream.ReadToEnd(), options) ?? new List<Memo>();
        }

        private async Task SaveDataAsync()
        {
            await using var fileStream = File.Create(_dbFilePath);
            var options = new JsonSerializerOptions() { 
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true 
            };
            await JsonSerializer.SerializeAsync(fileStream, _memoList, options);
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
            throw new NotImplementedException();
        }

        public async Task AddAsync(Memo memo)
        {
            if (memo.Id == -1)
            {
                memo.Id = _memoList.Count == 0 ? 0 : _memoList.Max(m => m.Id) + 1;
            }
            _memoList.Add(memo);
            await SaveDataAsync();
        }
    }
}
