using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class SkillCategory
    {
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_FR { get; set; }
        public IList<Skill> Skills { get; set; }
    }

    public class Skill
    {
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_FR { get; set; }
        public int Level { get; set; }
        public SkillCategory Category { get; set; }
    }
}
