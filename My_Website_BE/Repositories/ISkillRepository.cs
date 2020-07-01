using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface ISkillRepository
    {
        Skill Create(Skill skill);
        Skill Update(Skill skillChanges);
        Skill Delete(int id);
        Skill FindById(int id);
        IList<Skill> GetSkills();

    }
}
