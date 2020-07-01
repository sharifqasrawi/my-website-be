using Microsoft.EntityFrameworkCore;
using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlExperienceRepository : IExperienceRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlExperienceRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Experience Create(Experience experience)
        {
            dBContext.Experiences.Add(experience);
            dBContext.SaveChanges();
            return experience;
        }

        public Experience Delete(int id)
        {
            var experience = dBContext.Experiences.Find(id);
            if(experience != null)
            {
                dBContext.Experiences.Remove(experience);
                dBContext.SaveChanges();
                return experience;
            }
            return null;
        }

        public Experience FindById(int id)
        {
            var experience = dBContext.Experiences.Find(id);
            return experience;
        }

        public IList<Experience> GetExperiences()
        {
            var experiences = dBContext.Experiences
                                       .Include(x => x.Documents)
                                      .OrderByDescending(x => x.IsCurrentlyWorking)
                                      .ThenByDescending(x => x.EndDate)
                                      .ToList();

            return experiences;
        }

        public Experience Update(Experience experienceChanges)
        {
            var experience = dBContext.Experiences.Attach(experienceChanges);
            experience.State = EntityState.Modified;
            dBContext.SaveChanges();
            return experienceChanges;
        }
    }
}
