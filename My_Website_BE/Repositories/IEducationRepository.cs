using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IEducationRepository
    {
        Education Create(Education education);
        Education Update(Education educationChanges);
        Education Delete(int id);
        Education FindById(int id);
        IList<Education> GetEducations();

    }
}
