using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlCVFileRepository : ICVFileRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlCVFileRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public CVFile Create(CVFile cVFile)
        {
            dBContext.CVFiles.Add(cVFile);
            dBContext.SaveChanges();
            return cVFile;
        }

        public CVFile Delete(int id)
        {
            var cVFile = dBContext.CVFiles.Find(id);
            if(cVFile != null)
            {
                dBContext.CVFiles.Remove(cVFile);
                dBContext.SaveChanges();
                return cVFile;
            }
            return null;
        }

        public CVFile FindById(int id)
        {
            var cVFile = dBContext.CVFiles.Find(id);
            return cVFile;
        }

        public IList<CVFile> GetCVFiles()
        {
            var cVFiles = dBContext.CVFiles.OrderBy(x => x.Language).ToList();
            return cVFiles;
        }

        public CVFile Update(CVFile cVFileChanges)
        {
            var cVFile = dBContext.CVFiles.Attach(cVFileChanges);
            cVFile.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return cVFileChanges;
        }
    }
}
