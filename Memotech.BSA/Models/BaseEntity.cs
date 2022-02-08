using System.ComponentModel.DataAnnotations;

namespace Memotech.BSA.Models
{
    public class BaseEntity
    {
        [Required]
        public int Id { get; set; } = -1;
    }
}
