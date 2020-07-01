using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class TrainingCourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Establishment { get; set; }
        public string Country_EN { get; set; }
        public string Country_FR { get; set; }
        public string City_EN { get; set; }
        public string City_FR { get; set; }
        public string Type { get; set; }
        public string Duration { get; set; }
        public string CourseUrl { get; set; }
        public DateTime? DateTime { get; set; }

        public IList<Document> Documents { get; set; }
    }
}
