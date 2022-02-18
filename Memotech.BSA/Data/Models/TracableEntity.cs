using System.ComponentModel.DataAnnotations;

namespace Memotech.BSA.Data.Models
{
    public class TracableEntity : BaseEntity
    {
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public string? CreatedBy { get; set; } = "Denis";
        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        [Required]
        public string? UpdatedBy { get; set; } = "Denis";
    }
}
