using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IDirectoryRepository
    {
        IEnumerable<Directory> GetDirectories();

        Directory Create(Directory directory);
        Directory FindByPath(string path);
        Directory FindById(int id);
        Directory Delete(int id);
    }
}
