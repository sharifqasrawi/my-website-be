using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_FR { get; set; }

        public string Description_EN { get; set; }
        public string Description_FR { get; set; }

        public string Type { get; set; }

        public string VideoDemoUrlExt { get; set; }
        public string VideoDemoUrl { get; set; }

        public string LiveDemoUrl { get; set; }

        public bool? IsDisplayed { get; set; }

        public string Size { get; set; }

        public string GitHubUrl { get; set; }

        public string ImagePath { get; set; }

        public IList<ProjectTag> ProjectTags { get; set; }


    }
}
