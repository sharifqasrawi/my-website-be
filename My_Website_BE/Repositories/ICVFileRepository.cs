using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface ICVFileRepository
    {
        CVFile Create(CVFile cVFile);
        CVFile Update(CVFile cVFileChanges);
        CVFile Delete(int id);
        CVFile FindById(int id);
        IList<CVFile> GetCVFiles();
    }
}
