using Task = Jivar.BO.Models.Task;

namespace Jivar.DAO.DAOs
{
    public class TaskDAO : BaseDAO<Task>
    {
        private static TaskDAO? _instance;
        public static TaskDAO Instance => _instance ??= new TaskDAO(new JivarDbContext());
        public TaskDAO(JivarDbContext context) : base(context)
        {
        }
    }
}
