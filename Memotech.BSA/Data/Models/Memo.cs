﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Memotech.BSA.Data.Models
{
    public class Memo: TracableEntity
    {
        [Required]
        [MaxLength(255)]
        public string Term { get; set; } = "";
        [Required]
        public string Meaning { get; set; } = "";
        [Required]
        [Range (0, 2, ErrorMessage = "Please select value")]
        public int TypeId { get; set; } = 0;
        public string? Info { get; set; }
        public string? Image { get; set; }
        public bool IsStudied { get; set; }
        public int StudyPercentage { get; set; }
        public bool HasFullInfo { get; set; }
        public List<StudyStage> StudyStages { get; set; } = new();

        [NotMapped]
        [JsonIgnore]
        public string? StudyStatus => IsStudied ? "Studied" : $"Not studied - {StudyPercentage}%";

        [NotMapped]
        [JsonIgnore]
        public string? FullInfoStatus => HasFullInfo ? "Full info" : "Please fill information";
    }
}
