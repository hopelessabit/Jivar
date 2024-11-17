using Jivar.BO.Models;
using Microsoft.AspNetCore.Http;

namespace Jivar.Service.Interfaces
{
    public interface IDocumentService
    {
        Task<Document> uploadFileAsync(IFormFile file, int userId);
    }
}
