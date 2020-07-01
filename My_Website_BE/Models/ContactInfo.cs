using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public string Emails { get; set; }
        public string Phone { get; set; }
        public string Country_EN { get; set; }
        public string Country_FR { get; set; }
        public string City_EN { get; set; }
        public string City_FR { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string ZipCode { get; set; }
        public string LinkedInUrl { get; set; }
        public string GitHubUrl { get; set; }
        public string FacebookUrl { get; set; }

    }
}
