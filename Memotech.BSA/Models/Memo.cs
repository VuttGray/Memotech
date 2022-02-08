using System.ComponentModel.DataAnnotations;

namespace Memotech.BSA.Models
{
    public class Memo: BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Term { get; set; } = "";
        [Required]
        public string Meaning { get; set; } = "";
    }
}
