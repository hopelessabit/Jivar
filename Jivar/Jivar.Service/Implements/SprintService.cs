using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;

namespace Jivar.Service.Implements
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;

        public SprintService(ISprintRepository sprintRepository)
        {
            _sprintRepository = sprintRepository;
        }

        public async Task<bool> createSprint(Sprint sprint)
        {
            return await _sprintRepository.AddAsync(sprint);
        }

        public async Task<IEnumerable<Sprint>> listSprints()
        {
            return await _sprintRepository.GetAllAsync();
        }

        public async Task<Sprint> getSprintById(int? sprintId)
        {
            var result = await _sprintRepository.GetAsync(sp => sp.Id == sprintId);
            return result != null ? result : null;
        }

        public async Task<bool> updateSprint(Sprint result)
        {
            return await _sprintRepository.UpdateAsync(result);
        }

        public async Task<bool> deleteSprint(Sprint result)
        {
            return await _sprintRepository.DeleteAsync(result);
        }
    }
}
