using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Title_EN { get; set; }
        public string Title_FR { get; set; }
        public string Specialization_EN { get; set; }
        public string Specialization_FR { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? GraduateDate { get; set; }
        public int YearsCount { get; set; }
        public string Mention_EN { get; set; }
        public string Mention_FR { get; set; }
        public string Establishment_EN { get; set; }
        public string Establishment_FR { get; set; }
        public string Country_EN { get; set; }
        public string Country_FR { get; set; }
        public string City_EN { get; set; }
        public string City_FR { get; set; }
        public string Note { get; set; }
        public IList<Document> Documents { get; set; }

    }
}
