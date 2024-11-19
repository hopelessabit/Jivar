using Jivar.BO.Models;

namespace Jivar.Service.Payloads.Project.Response
{
    public class DocumentResponse
    {
        private TaskDocument td;

        public DocumentResponse(TaskDocument td)
        {
            this.td = td;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string FilePath { get; set; }

        public DateTime? UploadDate { get; set; }

        public int? UploadBy { get; set; }
    }
}