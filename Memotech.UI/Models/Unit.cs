using System.ComponentModel.DataAnnotations;

namespace Memotech.UI.Models
{
    public class Unit
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public string Meaning { get; set; }
    }
}
