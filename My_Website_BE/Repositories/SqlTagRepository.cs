using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlTagRepository : ITagRepository
    {
        private readonly ApplicationDBContext dBContext;

        public SqlTagRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Tag Create(Tag tag)
        {
            dBContext.Tags.Add(tag);
            dBContext.SaveChanges();

            return tag;
        }

        public Tag Delete(int id)
        {
            var tag = dBContext.Tags.Find(id);
            if (tag != null)
            {
                dBContext.Tags.Remove(tag);
                dBContext.SaveChanges();
                return tag;
            }
            return null;
        }

        public Tag FindById(int id)
        {
            return dBContext.Tags.Find(id);
        }

        public IList<Tag> GetTags()
        {
            return dBContext.Tags.OrderBy(t => t.Name).ToList();
        }

        public Tag Update(Tag tagChanges)
        {
            var tag = dBContext.Tags.Attach(tagChanges);
            tag.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();

            return tagChanges;
        }

    }
}
