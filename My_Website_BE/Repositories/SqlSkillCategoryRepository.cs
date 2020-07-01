using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlSkillCategoryRepository : ISkillCategoryRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlSkillCategoryRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public SkillCategory Create(SkillCategory skillCategory)
        {
            dBContext.SkillCategories.Add(skillCategory);
            dBContext.SaveChanges();
            return skillCategory;
        }

        public SkillCategory Delete(int id)
        {
            var skillCategory = dBContext.SkillCategories.Find(id);
            if(skillCategory != null)
            {
                dBContext.SkillCategories.Remove(skillCategory);
                dBContext.SaveChanges();
                return skillCategory;
            }
            return null;
        }

        public SkillCategory FindById(int id)
        {
            var skillCategory = dBContext.SkillCategories.Find(id);
            return skillCategory;
        }

        public IList<SkillCategory> GetSkillCategories()
        {
            var skillCategories = dBContext.SkillCategories
                                      .OrderBy(x => x.Name_EN)
                                      .ToList();

            return skillCategories;
        }

        public SkillCategory Update(SkillCategory skillCategoryChanges)
        {
            var skillCategory = dBContext.SkillCategories.Attach(skillCategoryChanges);
            skillCategory.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return skillCategoryChanges;
        }
    }
}
