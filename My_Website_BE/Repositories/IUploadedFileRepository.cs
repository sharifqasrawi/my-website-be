using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IUploadedFileRepository
    {
        IEnumerable<UploadedFile> GetUploadedFiles();
        UploadedFile Create(UploadedFile uploadedFile);
        UploadedFile FindById(int id);
        UploadedFile Delete(int id);


    }
}
