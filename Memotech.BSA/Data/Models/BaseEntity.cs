using System.ComponentModel.DataAnnotations;

namespace Memotech.BSA.Data.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; } = -1;
    }
}
