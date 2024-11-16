using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class SprintTaskService : ISprintTaskService
    {
        private readonly ISprintTaskRepository _sprintTaskRepository;

        public SprintTaskService(ISprintTaskRepository sprintTaskRepository)
        {
            _sprintTaskRepository = sprintTaskRepository;
        }

        public async Task<bool> deleteSprintTask(int? id)
        {
            var resultSprintTask = _sprintTaskRepository.Get(sp => sp.SprintId == id);
            return await _sprintTaskRepository.DeleteAsync(resultSprintTask);
        }

        public async Task<bool> AddSprintTask(SprintTask sprintTask)
        {
            return await _sprintTaskRepository.AddAsync(sprintTask);
        }

    }
}
