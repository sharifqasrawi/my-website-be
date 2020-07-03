using Microsoft.EntityFrameworkCore;
using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlProjectRepository : IProjectRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlProjectRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Project Create(Project project)
        {
            dBContext.Projects.Add(project);
            dBContext.SaveChanges();
            return project;
        }

        public Project Delete(int id)
        {
            var project = dBContext.Projects.Find(id);
            if(project != null)
            {
                dBContext.Projects.Remove(project);
                dBContext.SaveChanges();
                return project;
            }
            return null;
        }

        public Project FindById(int id)
        {
            var project = dBContext.Projects
                                    .Include("ProjectTags")
                                    .Include("ProjectTags.Tag")
                                    .SingleOrDefault(x => x.Id == id);
            return project;
        }

        public IList<Project> GetProjects()
        {
            var projects = dBContext.Projects
                                    .Include("ProjectTags")
                                    .Include("ProjectTags.Tag")
                                    .ToList();

            return projects;
        }

        public Project Update(Project projectChanges)
        {
            var project = dBContext.Projects.Attach(projectChanges);
            project.State = EntityState.Modified;
            dBContext.SaveChanges();
            return projectChanges;
        }
    }
}
