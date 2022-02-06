using System.ComponentModel.DataAnnotations;

namespace Memotech.BSA.Models
{
    public class Memo
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Term { get; set; } = "";
        [Required]
        public string Meaning { get; set; } = "";
    }
}
