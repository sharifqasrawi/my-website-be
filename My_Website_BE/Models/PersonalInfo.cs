using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class PersonalInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title_EN { get; set; }
        public string Title_FR { get; set; }
        public string About_EN { get; set; }
        public string About_FR { get; set; }
        public string ImagePath { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public string DriversLicense { get; set; }


    }
}
