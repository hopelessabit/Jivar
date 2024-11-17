using Firebase.Storage;
using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Jivar.Service.Implements
{
    public class DocumentService : IDocumentService
    {
        private readonly JivarDbContext _dbContext;
        private const string FirebaseStorageBucket = "highschoolvn-dev.appspot.com";

        public DocumentService(JivarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Document> uploadFileAsync(IFormFile file, int userId)
        {
            try
            {
                var stream = file.OpenReadStream();
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                var firebaseStorage = new FirebaseStorage(
                    FirebaseStorageBucket,
                    new FirebaseStorageOptions
                    {
                        ThrowOnCancel = true
                    });
                var downloadUrl = await firebaseStorage
                    .Child("jivar-images")
                    .Child(fileName)
                    .PutAsync(stream);
                Document document = new Document();
                document.Name = fileName;
                document.FilePath = downloadUrl;
                document.UploadDate = DateTime.Now;
                document.UploadBy = userId;
                _dbContext.Documents.Add(document);
                _dbContext.SaveChanges();
                return document;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Document> getDocuments()
        {
            return _dbContext.Documents.ToList();
        }

        public Document getDocumentsById(int documentId)
        {
            return _dbContext.Documents.FirstOrDefault(d => d.Id == documentId);
        }

        public bool deleteDocumentsById(int id)
        {
            TaskDocument document = _dbContext.TaskDocuments.FirstOrDefault(d => d.DocumentId == id);
            if (document != null)
            {
                _dbContext.TaskDocuments.Remove(document);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
