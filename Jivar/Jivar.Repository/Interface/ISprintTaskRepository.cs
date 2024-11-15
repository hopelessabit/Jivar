﻿using Jivar.BO.Models;

namespace Jivar.Repository.Interface
{
    public interface ISprintTaskRepository : IRepository<SprintTask>
    {
        Task<bool> DeleteAsync(int? id);
    }
}