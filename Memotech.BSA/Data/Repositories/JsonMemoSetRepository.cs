using Memotech.Core.Domain;

namespace Memotech.BSA.Data.Repositories
{
    public class JsonMemoSetRepository : JsonRepository<Memo>
    {
        public JsonMemoSetRepository() : base("memoSets.json") { }
    }
}
