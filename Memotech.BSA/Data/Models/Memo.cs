using System.ComponentModel.DataAnnotations;

namespace Memotech.BSA.Data.Models
{
    public class Memo: BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Term { get; set; } = "";
        [Required]
        public string Meaning { get; set; } = "";
        [Required]
        [Range (0, 2, ErrorMessage = "Please select value")]
        public int TypeId { get; set; } = -1;
        public string? Info { get; set; }
        public string? Image { get; set; }
        public bool IsStudied { get; set; }
        public int StudyPercentage { get; set; }
        public bool HasFullInfo { get; set; }
        public List<StudyStage> StudyStages { get; set; } = new();
    }
}
