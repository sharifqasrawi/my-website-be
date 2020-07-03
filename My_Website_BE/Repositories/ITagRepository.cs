using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface ITagRepository
    {
        IList<Tag> GetTags();
        Tag Create(Tag tag);
        Tag Update(Tag tagChanges);
        Tag FindById(int id);
        Tag Delete(int id);
    }
}
