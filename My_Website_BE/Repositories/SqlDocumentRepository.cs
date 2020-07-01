using My_Website_BE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My_Website_BE.Repositories
{
    public class SqlDocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDBContext dBContext;
        public SqlDocumentRepository(ApplicationDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public Document Create(Document document)
        {
            dBContext.Documents.Add(document);
            dBContext.SaveChanges();
            return document;
        }

        public Document Delete(int id)
        {
            var document = dBContext.Documents.Find(id);
            if(document != null)
            {
                dBContext.Documents.Remove(document);
                dBContext.SaveChanges();
                return document;
            }
            return null;
        }

        public Document FindById(int id)
        {
            var document = dBContext.Documents.Find(id);
            return document;
        }

        public IList<Document> GetDocuments()
        {
            var documents = dBContext.Documents
                                      .ToList();

            return documents;
        }

        public Document Update(Document documentChanges)
        {
            var document = dBContext.Documents.Attach(documentChanges);
            document.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return documentChanges;
        }
    }
}
