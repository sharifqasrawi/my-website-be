using Microsoft.EntityFrameworkCore;
using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlTrainingCourseRepository : ITrainingCourseRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlTrainingCourseRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public TrainingCourse Create(TrainingCourse trainingCourse)
        {
            dBContext.TrainingCourses.Add(trainingCourse);
            dBContext.SaveChanges();
            return trainingCourse;
        }

        public TrainingCourse Delete(int id)
        {
            var trainingCourse = dBContext.TrainingCourses.Find(id);
            if(trainingCourse != null)
            {
                dBContext.TrainingCourses.Remove(trainingCourse);
                dBContext.SaveChanges();
                return trainingCourse;
            }
            return null;
        }

        public TrainingCourse FindById(int id)
        {
            var trainingCourse = dBContext.TrainingCourses.Find(id);
            return trainingCourse;
        }

        public IList<TrainingCourse> GetTrainingCourses()
        {
            var trainingCourses = dBContext.TrainingCourses
                                       .Include(x => x.Documents)
                                      .OrderByDescending(x => x.DateTime)
                                      .ToList();

            return trainingCourses;
        }

        public TrainingCourse Update(TrainingCourse trainingCourseChanges)
        {
            var trainingCourse = dBContext.TrainingCourses.Attach(trainingCourseChanges);
            trainingCourse.State = EntityState.Modified;
            dBContext.SaveChanges();
            return trainingCourseChanges;
        }
    }
}
