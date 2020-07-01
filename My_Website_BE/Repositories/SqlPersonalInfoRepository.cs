using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlPersonalInfoRepository : IPersonalInfoRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlPersonalInfoRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public PersonalInfo Create(PersonalInfo personalInfo)
        {
            dBContext.PersonalInfo.Add(personalInfo);
            dBContext.SaveChanges();
            return personalInfo;
        }

        public PersonalInfo GetPersonalInfo()
        {
            var personalInfo = dBContext.PersonalInfo.FirstOrDefault();
            return personalInfo;
        }

        public PersonalInfo Update(PersonalInfo personalInfoChanges)
        {
            var personalInfo = dBContext.PersonalInfo.Attach(personalInfoChanges);
            personalInfo.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return personalInfoChanges;
        }
    }
}
