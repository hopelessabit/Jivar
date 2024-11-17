using Jivar.BO.Models;
using Jivar.Service.Payloads.Backlog.Request;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Interfaces
{
    public interface IBacklogService
    {
        // Get a backlog by its ID
        Task<Backlog> GetBacklogById(int id);

        // Get all backlogs by a single Task ID
        Task<List<Backlog>> getBacklogByTaskId(int taskId);

        // Get all backlogs by multiple Task IDs
        Task<List<Backlog>> getBacklogByTaskIds(List<int> taskIds);

        // Get all backlogs by a Project ID
        Task<List<Backlog>> getBacklogByProjectId(int projectId);

        // Create a new backlog
        Task<bool> createBacklog(CreateBacklogRequest request, HttpContext context);

        // Update an existing backlog
        Task<bool> updateBacklog(int backlogId, UpdateBacklogRequest request, HttpContext context);

        // Delete a backlog
        Task<bool> deleteBacklog(int backlogId);
    }
}
