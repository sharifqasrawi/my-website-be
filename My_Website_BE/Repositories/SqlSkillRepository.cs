using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlSkillRepository : ISkillRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlSkillRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Skill Create(Skill skill)
        {
            dBContext.Skills.Add(skill);
            dBContext.SaveChanges();
            return skill;
        }

        public Skill Delete(int id)
        {
            var skill = dBContext.Skills.Find(id);
            if(skill != null)
            {
                dBContext.Skills.Remove(skill);
                dBContext.SaveChanges();
                return skill;
            }
            return null;
        }

        public Skill FindById(int id)
        {
            var skill = dBContext.Skills.Find(id);
            return skill;
        }

        public IList<Skill> GetSkills()
        {
            var Skills = dBContext.Skills
                                      .OrderBy(x => x.Name_EN)
                                      .ToList();

            return Skills;
        }

        public Skill Update(Skill skillChanges)
        {
            var skill = dBContext.Skills.Attach(skillChanges);
            skill.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return skillChanges;
        }
    }
}
