using Memotech.Core.Domain;

namespace Memotech.BSA.Data.Repositories
{
    public class JsonMemoRepository : JsonRepository<Memo>
    {
        public JsonMemoRepository() : base("memos.json") { }
    }
}
