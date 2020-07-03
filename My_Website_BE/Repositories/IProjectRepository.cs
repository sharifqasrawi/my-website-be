using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IProjectRepository
    {
        Project Create(Project project);
        Project Update(Project projectChanges);
        Project Delete(int id);
        Project FindById(int id);
        IList<Project> GetProjects();
    }
}
