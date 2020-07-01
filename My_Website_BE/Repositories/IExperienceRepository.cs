using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IExperienceRepository
    {
        Experience Create(Experience experience);
        Experience Update(Experience experienceChanges);
        Experience Delete(int id);
        Experience FindById(int id);
        IList<Experience> GetExperiences();

    }
}
