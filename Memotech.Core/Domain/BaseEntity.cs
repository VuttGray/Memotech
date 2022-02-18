using System.ComponentModel.DataAnnotations;

namespace Memotech.Core.Domain
{
    public class BaseEntity
    {
        [Required] 
        public int Id { get; set; } = 0;
    }
}
