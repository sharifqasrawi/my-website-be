using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface ISkillCategoryRepository
    {
        SkillCategory Create(SkillCategory skillCategory);
        SkillCategory Update(SkillCategory skillCategoryChanges);
        SkillCategory Delete(int id);
        SkillCategory FindById(int id);
        IList<SkillCategory> GetSkillCategories();

    }
}
