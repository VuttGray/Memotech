using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Domain;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.IO;

namespace Memotech.BSA.Data.Repositories
{
    public class JsonMemoRepository : IRepository<Memo>
    {
        readonly string _dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Memotech", "memos.json");
        List<Memo> _memoList = new();

        public JsonMemoRepository()
        {
            LoadData();
        }

        private string ReadText(string filePath)
        {
            byte[] result;
            using FileStream SourceStream = File.Open(filePath, FileMode.Open);
            result = new byte[SourceStream.Length];
            SourceStream.Read(result, 0, (int)SourceStream.Length);
            return Encoding.UTF8.GetString(result);
        }

        static async Task<string> ReadTextAsync(string filePath)
        {
            byte[] result;
            using FileStream SourceStream = File.Open(filePath, FileMode.Open);
            result = new byte[SourceStream.Length];
            await SourceStream.ReadAsync(result.AsMemory(0, (int)SourceStream.Length));
            return Encoding.UTF8.GetString(result);
        }

        private string TryReadText()
        {
            var limitTime = DateTime.Now.AddSeconds(3);
            while (DateTime.Now < limitTime)
            {
                try
                {
                    return ReadText(_dbFilePath);
                }
                catch (IOException ex)
                {
                    if (ex.Message != $"The process cannot access the file '{_dbFilePath}' because it is being used by another process.")
                    {
                        throw;
                    }
                }
            }
            throw new TimeoutException("Something is wrong - reading the data took too long time");
        }

        public void LoadData()
        {
            var text = TryReadText();
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _memoList = JsonSerializer.Deserialize<List<Memo>>(text, options) ?? new List<Memo>();
        }

        public async Task LoadDataAsync()
        {
            var text = await ReadTextAsync(_dbFilePath);
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _memoList = JsonSerializer.Deserialize<List<Memo>>(text, options) ?? new List<Memo>();
        }

        static void WriteText(string filePath, string text)
        {
            byte[] result = Encoding.UTF8.GetBytes(text);
            using var sourceStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            sourceStream.Write(result);
        }

        static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] result = Encoding.UTF8.GetBytes(text);
            using var sourceStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            await sourceStream.WriteAsync(result);
        }

        private void SaveData()
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(_memoList, options);
            WriteText(_dbFilePath, json);
        }

        private async Task SaveDataAsync()
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(_memoList, options);
            await WriteTextAsync(_dbFilePath, json);
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
            if (memo.Id < 1)
            {
                memo.Id = _memoList.Count == 0 ? 1 : _memoList.Max(m => m.Id) + 1;
            }
            _memoList.Add(memo);
            SaveData();
        }

        public async Task AddAsync(Memo memo)
        {
            if (memo.Id < 1)
            {
                memo.Id = _memoList.Count == 0 ? 1 : _memoList.Max(m => m.Id) + 1;
            }
            _memoList.Add(memo);
            await SaveDataAsync();
        }

        public void Edit(Memo memo)
        {
            var entity = _memoList.First(u => u.Id == memo.Id);
            SaveData();
        }

        public async Task EditAsync(Memo memo)
        {
            var entity = _memoList.First(u => u.Id == memo.Id);
            await SaveDataAsync();
        }
    }
}
