using Memotech.Core.Abstractions.Repositories;
using Memotech.Core.Domain;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace Memotech.BSA.Data.Repositories
{
    public class JsonRepository<T> : IRepository<T> where T : BaseEntity
    {
        private string _dbFilePath = "";
        protected List<T> _entities = new();

        public JsonRepository(string dbFileName)
        {
            _dbFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Memotech", dbFileName);
            if (File.Exists(_dbFilePath))
                LoadData();
            else
                SaveData(); // Save empty list to create a default file
        }

        protected string ReadText(string filePath)
        {
            byte[] result;
            using FileStream SourceStream = File.Open(filePath, FileMode.Open);
            result = new byte[SourceStream.Length];
            SourceStream.Read(result, 0, (int)SourceStream.Length);
            return Encoding.UTF8.GetString(result);
        }

        protected static async Task<string> ReadTextAsync(string filePath)
        {
            byte[] result;
            using FileStream SourceStream = File.Open(filePath, FileMode.Open);
            result = new byte[SourceStream.Length];
            await SourceStream.ReadAsync(result.AsMemory(0, (int)SourceStream.Length));
            return Encoding.UTF8.GetString(result);
        }

        protected string TryReadText()
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
            _entities = JsonSerializer.Deserialize<List<T>>(text, options) ?? new List<T>();
        }

        public async Task LoadDataAsync()
        {
            var text = await ReadTextAsync(_dbFilePath);
            var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
            _entities = JsonSerializer.Deserialize<List<T>>(text, options) ?? new List<T>();
        }

        protected static void WriteText(string filePath, string text)
        {
            byte[] result = Encoding.UTF8.GetBytes(text);
            using var sourceStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            sourceStream.Write(result);
        }

        protected static async Task WriteTextAsync(string filePath, string text)
        {
            byte[] result = Encoding.UTF8.GetBytes(text);
            using var sourceStream = File.Open(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);
            await sourceStream.WriteAsync(result);
        }

        protected void SaveData()
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(_entities, options);
            WriteText(_dbFilePath, json);
        }

        protected async Task SaveDataAsync()
        {
            var options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            var json = JsonSerializer.Serialize(_entities, options);
            await WriteTextAsync(_dbFilePath, json);
        }

        public List<T> GetAll()
        {
            return _entities;
        }

        public T? Get(int id)
        {
            return GetAll().FirstOrDefault(u => u.Id == id);
        }

        public void Add(T entity)
        {
            if (entity.Id < 1)
            {
                entity.Id = _entities.Count == 0 ? 1 : _entities.Max(m => m.Id) + 1;
            }
            _entities.Add(entity);
            SaveData();
        }

        public async Task AddAsync(T entity)
        {
            if (entity.Id < 1)
            {
                entity.Id = _entities.Count == 0 ? 1 : _entities.Max(m => m.Id) + 1;
            }
            _entities.Add(entity);
            await SaveDataAsync();
        }

        public void Edit(T entity)
        {
            SaveData();
        }

        public async Task EditAsync(T entity)
        {
            await SaveDataAsync();
        }
    }
}
