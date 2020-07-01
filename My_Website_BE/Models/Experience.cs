using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public string Title_EN { get; set; }
        public string Title_FR { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsCurrentlyWorking { get; set; }
        public string Company { get; set; }
        public string Country_EN { get; set; }
        public string Country_FR { get; set; }
        public string City_EN { get; set; }
        public string City_FR { get; set; }
        public string Responisbilites_EN { get; set; }
        public string Responisbilites_FR { get; set; }
        public string Accomplishments_EN { get; set; }
        public string Accomplishments_FR { get; set; }
        public IList<Document> Documents { get; set; }
    }
}
