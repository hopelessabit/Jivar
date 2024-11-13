using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<bool> CreateTask(BO.Models.Task task)
        {
            return await _taskRepository.AddAsync(task);
        }
    }
}
