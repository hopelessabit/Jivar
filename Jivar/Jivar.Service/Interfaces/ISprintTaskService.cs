﻿using Jivar.BO.Models;

namespace Jivar.Service.Interfaces
{
    public interface ISprintTaskService
    {
        Task<bool> AddSprintTask(SprintTask sprintTask);
        Task<bool> deleteSprintTask(int? id);
        Task<bool> updateSprintTask(int id, DateTime? startDateSprintTask, DateTime? endDateSprintTask);
    }
}
