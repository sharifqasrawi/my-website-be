using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_FR { get; set; }
        public int LevelRead { get; set; }
        public int LevelWrite { get; set; }
        public int LevelSpeak { get; set; }
        public IList<Document> Documents { get; set; }
    }
}
