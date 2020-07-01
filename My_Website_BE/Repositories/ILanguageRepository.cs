using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface ILanguageRepository
    {
        Language Create(Language language);
        Language Update(Language languageChanges);
        Language Delete(int id);
        Language FindById(int id);
        IList<Language> GetLanguages();

    }
}
