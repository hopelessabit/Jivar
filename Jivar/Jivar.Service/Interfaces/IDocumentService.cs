using Jivar.BO.Models;
using Microsoft.AspNetCore.Http;

namespace Jivar.Service.Interfaces
{
    public interface IDocumentService
    {
        bool deleteDocumentsById(int id);
        List<Document> getDocuments();
        Document getDocumentsById(int documentId);
        Task<Document> uploadFileAsync(IFormFile file, int userId);
    }
}
