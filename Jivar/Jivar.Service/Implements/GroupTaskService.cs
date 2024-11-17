//using Jivar.BO.Models;
//using Jivar.DAO;
//using Jivar.Repository.Interface;
//using Jivar.Service.Interfaces;

//namespace Jivar.Service.Implements
//{
//    public class GroupTaskService : IGroupTaskService
//    {

//        private readonly IGroupTaskRepository _groupTaskRepository;
//        private readonly JivarDbContext _dbContext;

//        public GroupTaskService(IGroupTaskRepository groupTaskRepository, JivarDbContext dbContext)
//        {
//            _groupTaskRepository = groupTaskRepository;
//            _dbContext = dbContext;
//        }

//        public async Task<bool> createGroupTask(int taskId, int subTaskId)
//        {
//            GroupTask groupTask = new GroupTask
//            {
//                SubTaskId = subTaskId,
//                TaskId = taskId
//            };
//            if ((_dbContext.Tasks.FirstOrDefault(t => t.Id.Equals(taskId)) != null))
//            {
//                await _dbContext.GroupTasks.AddAsync(groupTask);
//                _dbContext.SaveChanges();
//                return true;
//            }
//            return false;
//        }
//    }
//}
