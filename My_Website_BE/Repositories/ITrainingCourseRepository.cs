using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface ITrainingCourseRepository
    {
        TrainingCourse Create(TrainingCourse trainingCourse);
        TrainingCourse Update(TrainingCourse trainingCourseChanges);
        TrainingCourse Delete(int id);
        TrainingCourse FindById(int id);
        IList<TrainingCourse> GetTrainingCourses();

    }
}
