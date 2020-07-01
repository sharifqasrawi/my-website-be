using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public interface IDocumentRepository
    {
        Document Create(Document document);
        Document Update(Document documentChanges);
        Document Delete(int id);
        Document FindById(int id);
        IList<Document> GetDocuments();

    }
}
