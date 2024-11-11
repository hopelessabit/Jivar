using Jivar.DAO.DAOs;
using Jivar.BO.Models;
using Jivar.Repository.Interface;

namespace Jivar.Repository
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository() : base(DocumentDAO.Instance)
        {
        }
    }
}
