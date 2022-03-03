using Memotech.Core.Domain;

namespace Memotech.BSA.Data.Repositories
{
    public class JsonMemoSetRepository : JsonRepository<MemoSet>
    {
        public JsonMemoSetRepository() : base("memoSets.json") { }
    }
}
