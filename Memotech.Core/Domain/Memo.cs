using Memotech.BSA.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Memotech.Core.Domain
{
    public class Memo: TraceableEntity
    {
        [Required]
        [MaxLength(255)]
        public string Term { get; set; } = "";
        [Required]
        public string Meaning { get; set; } = "";
        public int TypeId { get; set; }
        public string? Info { get; set; }
        public string? Image { get; set; }
        public bool IsStudied { get; set; }
        public int StudyPercentage { get; set; }
        public bool HasFullInfo { get; set; }
        public List<StudyStage> StudyStages { get; set; } = new();
        public List<int> MemoSetsList { get; set; } = new();

        [NotMapped]
        [JsonIgnore]
        public string? StudyStatus => IsStudied ? "Studied" : $"Not studied - {StudyPercentage}%";

        [NotMapped]
        [JsonIgnore]
        public string? FullInfoStatus => HasFullInfo ? "Full info" : "Please fill information";
    }
}
