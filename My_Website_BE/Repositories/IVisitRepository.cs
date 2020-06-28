using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IVisitRepository
    {
        Visit Create(Visit visit);
        Visit Update(Visit visitChanges);
        IList<Visit> GetVisits();
        Visit FindByIP(string IP);
    }
}
