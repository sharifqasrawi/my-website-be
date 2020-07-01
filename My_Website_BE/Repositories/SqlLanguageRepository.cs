using Microsoft.EntityFrameworkCore;
using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlLanguageRepository : ILanguageRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlLanguageRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Language Create(Language language)
        {
            dBContext.Languages.Add(language);
            dBContext.SaveChanges();
            return language;
        }

        public Language Delete(int id)
        {
            var language = dBContext.Languages.Find(id);
            if(language != null)
            {
                dBContext.Languages.Remove(language);
                dBContext.SaveChanges();
                return language;
            }
            return null;
        }

        public Language FindById(int id)
        {
            var language = dBContext.Languages.Find(id);
            return language;
        }

        public IList<Language> GetLanguages()
        {
            var languages = dBContext.Languages
                                     .Include(x => x.Documents)
                                      .ToList();

            return languages;
        }

        public Language Update(Language languageChanges)
        {
            var language = dBContext.Languages.Attach(languageChanges);
            language.State = EntityState.Modified;
            dBContext.SaveChanges();
            return languageChanges;
        }
    }
}
