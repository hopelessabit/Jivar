using Jivar.BO;
using Jivar.BO.Models;

namespace Jivar.DAO.DAOs
{
    public class DocumentDAO : BaseDAO<Document>
    {
        private static DocumentDAO? _instance;
        public static DocumentDAO Instance => _instance ??= new DocumentDAO(new JivarDbContext());
        public DocumentDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
