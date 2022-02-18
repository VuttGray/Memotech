using System.ComponentModel.DataAnnotations;

namespace Memotech.Core.Domain
{
    public class TraceableEntity : BaseEntity
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
