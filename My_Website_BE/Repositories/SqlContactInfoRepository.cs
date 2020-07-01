using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlContactInfoRepository : IContactInfoRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlContactInfoRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public ContactInfo Create(ContactInfo contactInfo)
        {
            dBContext.ContactInfo.Add(contactInfo);
            dBContext.SaveChanges();
            return contactInfo;
        }

        public ContactInfo GetContact()
        {
            var contactInfo = dBContext.ContactInfo.FirstOrDefault();
            return contactInfo;
        }

        public ContactInfo Update(ContactInfo contactInfoChanges)
        {
            var contactInfo = dBContext.ContactInfo.Attach(contactInfoChanges);
            contactInfo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return contactInfoChanges;
        }
    }
}
