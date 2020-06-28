using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Dtos.Users
{
    public class UserActivatedDeactivateDto
    {
        public string UserId { get; set; }
        public string Option { get; set; }
    }
}
