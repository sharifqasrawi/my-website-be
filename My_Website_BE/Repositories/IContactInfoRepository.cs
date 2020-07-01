using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IContactInfoRepository
    {
        ContactInfo Create(ContactInfo contactInfo);
        ContactInfo Update(ContactInfo contactInfoChanges);
        ContactInfo GetContact();
    }
}
