using Jivar.BO.Models;
using Jivar.Service.Payloads.ProjectRole.Request;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jivar.Service.Interfaces
{
    public interface IProjectRoleService
    {
        // Get all roles for a specific project
        Task<List<ProjectRole>> GetRolesByProjectId(int projectId);

        // Get all roles for a specific account
        Task<List<ProjectRole>> GetRolesByAccountId(int accountId);

        // Add a new role to a project
        Task<bool> AddRoleToProject(CreateProjectRoleRequest projectRole, HttpContext context);
        Task<bool> AddRoleToProject(ProjectRole projectRole, HttpContext context);

        // Update an existing project role
        Task<bool> UpdateProjectRole(UpdateProjectRoleRequest request, HttpContext context);

        // Remove a role from a project
        Task<bool> RemoveRoleFromProject(int projectId, int accountId, HttpContext context);

        Task<ProjectRole> GetRoleByProjectIdAndAccountId(int projectId, int roleId);

        Task<List<ProjectRole>> GetProjectRolesByIds(List<int> ids);
    }
}
