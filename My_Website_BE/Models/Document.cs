using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_FR { get; set; }
        public string Description_EN { get; set; }
        public string Description_FR { get; set; }

        public string Type { get; set; }
        public int? FileId { get; set; }
        public bool? IsDisplayed { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public string Path { get; set; }

        [JsonIgnore]
        public Experience Experience { get; set; }
        public int? ExperienceId { get; set; }

        [JsonIgnore]
        public Education Education { get; set; }
        public int? EducationId { get; set; }

        [JsonIgnore]
        public Language Language { get; set; }
        public int? LanguageId { get; set; }

        [JsonIgnore]
        public TrainingCourse Course { get; set; }
        public int? CourseId { get; set; }


    }
}
