using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Models
{
    public class CVFile
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Language { get; set; }
        public string FilePath { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}
