using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Dtos.Users
{
    public class RegUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public bool? IsAdmin { get; set; }
        public bool? EmailConfirmed { get; set; }
    }
}
