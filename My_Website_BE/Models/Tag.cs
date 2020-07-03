using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<ProjectTag> ProjectTags { get; set; }
    }
}
