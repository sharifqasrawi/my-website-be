using Microsoft.EntityFrameworkCore;
using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlEducationRepository : IEducationRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlEducationRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Education Create(Education education)
        {
            dBContext.Educations.Add(education);
            dBContext.SaveChanges();
            return education;
        }

        public Education Delete(int id)
        {
            var education = dBContext.Educations.Find(id);
            if(education != null)
            {
                dBContext.Educations.Remove(education);
                dBContext.SaveChanges();
                return education;
            }
            return null;
        }

        public Education FindById(int id)
        {
            var education = dBContext.Educations.Find(id);
            return education;
        }

        public IList<Education> GetEducations()
        {
            var educations = dBContext.Educations
                                      .Include(x => x.Documents)
                                      .OrderByDescending(x => x.GraduateDate)
                                      .ToList();

            return educations;
        }

        public Education Update(Education educationChanges)
        {
            var education = dBContext.Educations.Attach(educationChanges);
            education.State = EntityState.Modified;
            dBContext.SaveChanges();
            return educationChanges;
        }
    }
}
