using Jivar.BO.Enumarate;
using Jivar.BO.Models;
using Jivar.Repository;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.ProjectRole.Request;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jivar.Service.Implements
{
    public class ProjectRoleService : IProjectRoleService
    {
        public async Task<bool> CheckUserRole(int projectId, HttpContext context, List<ProjectRoleType> allowedRoles)
        {
            // Get the current user's AccountId from HttpContext
            var accountId = UserUtil.GetAccountId(context);

            // Fetch the user's role in the project
            var projectRole = await _projectRoleRepository.GetAsync(
                pr => pr.ProjectId == projectId && pr.AccountId == accountId
            );

            if (projectRole == null)
                throw new UnauthorizedAccessException("User does not have access to this project.");

            // Check if the user's role is in the allowedRoles
            if (allowedRoles != null && !allowedRoles.Contains(projectRole.Role))
                throw new UnauthorizedAccessException($"User does not have permission for this operation. Current Role: {projectRole.Role}");

            return true;
        }
        private readonly IProjectRoleRepository _projectRoleRepository;

        public ProjectRoleService(IProjectRoleRepository projectRoleRepository)
        {
            _projectRoleRepository = projectRoleRepository;
        }

        // Get all roles for a specific project
        public async Task<List<ProjectRole>> GetRolesByProjectId(int projectId)
        {
            return (await _projectRoleRepository.GetAllAsync(pr => pr.ProjectId == projectId)).ToList();
        }

        // Get all roles for a specific account
        public async Task<List<ProjectRole>> GetRolesByAccountId(int accountId)
        {
            return (await _projectRoleRepository.GetAllAsync(pr => pr.AccountId == accountId)).ToList();
        }

        // Add a new role to a project
        public async Task<bool> AddRoleToProject(ProjectRole role, HttpContext context)
        {

            // Get the current user's role in the project
            int userId = UserUtil.GetAccountId(context);

            ProjectRole? existedProjectRole = (await _projectRoleRepository.GetAllAsync(ft => ft.ProjectId == role.ProjectId)).FirstOrDefault();
            if (existedProjectRole != null)
            {

                var currentUserRole = await _projectRoleRepository.GetAsync(pr => pr.ProjectId == role.ProjectId && pr.AccountId == userId);

                if (currentUserRole == null)
                    throw new UnauthorizedAccessException("You do not have access to add roles to this project.");

                // Role validation based on current user's role
                if (currentUserRole.Role == ProjectRoleType.Admin)
                {
                    // Admin can only assign specific roles
                    if (role.Role != ProjectRoleType.Manager &&
                        role.Role != ProjectRoleType.Developer &&
                        role.Role != ProjectRoleType.Tester &&
                        role.Role != ProjectRoleType.Viewer)
                    {
                        throw new UnauthorizedAccessException("Admins can only assign roles to Manager, Developer, Tester, or Viewer.");
                    }
                }
                else if (currentUserRole.Role != ProjectRoleType.Owner)
                {
                    // Only Owner or Admin can add roles
                    throw new UnauthorizedAccessException("You must be an Owner or Admin to add roles.");
                }
            }

            var success = await _projectRoleRepository.AddAsync(role);

            if (!success)
                throw new Exception("Failed to add role to the project.");

            return success;
        }

        // Add a new role to a project
        public async Task<bool> AddRoleToProject(CreateProjectRoleRequest request, HttpContext context)
        {
            // Validate input
            if (request == null)
                throw new ArgumentNullException(nameof(request), "ProjectRole cannot be null.");

            // Get the current user's role in the project
            int userId = UserUtil.GetAccountId(context);

            ProjectRole? existedProjectRole = (await _projectRoleRepository.GetAllAsync()).FirstOrDefault();
            if (existedProjectRole != null)
            {

                var currentUserRole = await _projectRoleRepository.GetAsync(pr => pr.ProjectId == request.ProjectId && pr.AccountId == userId);

                if (currentUserRole == null)
                    throw new UnauthorizedAccessException("You do not have access to add roles to this project.");

                // Role validation based on current user's role
                if (currentUserRole.Role == ProjectRoleType.Admin)
                {
                    // Admin can only assign specific roles
                    if (request.Role != ProjectRoleType.Manager &&
                        request.Role != ProjectRoleType.Developer &&
                        request.Role != ProjectRoleType.Tester &&
                        request.Role != ProjectRoleType.Viewer)
                    {
                        throw new UnauthorizedAccessException("Admins can only assign roles to Manager, Developer, Tester, or Viewer.");
                    }
                }
                else if (currentUserRole.Role != ProjectRoleType.Owner)
                {
                    // Only Owner or Admin can add roles
                    throw new UnauthorizedAccessException("You must be an Owner or Admin to add roles.");
                }
            }

            var success = await _projectRoleRepository.AddAsync(new ProjectRole()
            {
                ProjectId = request.ProjectId,
                AccountId = request.AccountId,
                Role = request.Role
            });

            if (!success)
                throw new Exception("Failed to add role to the project.");

            return success;
        }


        // Update an existing project role
        public async Task<bool> UpdateProjectRole(UpdateProjectRoleRequest request, HttpContext context)
        {
            // Fetch the current user's role in the project
            int userId = UserUtil.GetAccountId(context);
            var currentUserRole = await _projectRoleRepository.GetAsync(pr => pr.ProjectId == request.ProjectId && pr.AccountId == userId);

            if (currentUserRole == null)
                throw new UnauthorizedAccessException("You do not have access to update project roles.");

            // Check if the current user has sufficient permissions
            if (currentUserRole.Role == ProjectRoleType.Admin)
            {
                // Admin can only set roles to Manager, Developer, Tester, and Viewer
                if (request.Role != ProjectRoleType.Manager &&
                    request.Role != ProjectRoleType.Developer &&
                    request.Role != ProjectRoleType.Tester &&
                    request.Role != ProjectRoleType.Viewer)
                {
                    throw new UnauthorizedAccessException("Admins can only set roles to Manager, Developer, Tester, or Viewer.");
                }
            }
            else if (currentUserRole.Role != ProjectRoleType.Owner)
            {
                // Only Owner or Admin can update roles
                throw new UnauthorizedAccessException("You must be an Owner or Admin to update project roles.");
            }

            // Fetch the existing role to update
            var projectRole = await _projectRoleRepository.GetAsync(pr => pr.ProjectId == request.ProjectId && pr.AccountId == request.AccountId);
            if (projectRole == null)
                throw new Exception($"No role found for ProjectId {request.ProjectId} and AccountId {request.AccountId}.");

            // Update the role
            projectRole.Role = request.Role;

            // Save the changes
            return await _projectRoleRepository.UpdateAsync(projectRole);
        }


        // Remove a role from a project
        public async Task<bool> RemoveRoleFromProject(int projectId, int accountId, HttpContext context)
        {
            // Fetch the current user's role in the project
            int userId = UserUtil.GetAccountId(context);
            var currentUserRole = await _projectRoleRepository.GetAsync(pr => pr.ProjectId == projectId && pr.AccountId == userId);

            if (currentUserRole == null)
                throw new UnauthorizedAccessException("You do not have access to remove roles from this project.");

            // Fetch the role to be removed
            var projectRole = await _projectRoleRepository.GetAsync(pr => pr.ProjectId == projectId && pr.AccountId == accountId);
            if (projectRole == null)
                throw new Exception($"No role found for ProjectId {projectId} and AccountId {accountId}.");

            // Role validation based on current user's role
            if (currentUserRole.Role == ProjectRoleType.Admin)
            {
                // Admin can only remove specific roles
                if (projectRole.Role != ProjectRoleType.Manager &&
                    projectRole.Role != ProjectRoleType.Developer &&
                    projectRole.Role != ProjectRoleType.Tester &&
                    projectRole.Role != ProjectRoleType.Viewer)
                {
                    throw new UnauthorizedAccessException("Admins can only remove roles Manager, Developer, Tester, or Viewer.");
                }
            }
            else if (currentUserRole.Role != ProjectRoleType.Owner)
            {
                // Only Owner or Admin can remove roles
                throw new UnauthorizedAccessException("You must be an Owner or Admin to remove roles.");
            }

            // Remove the role
            return await _projectRoleRepository.DeleteAsync(projectRole);
        }

        public async Task<ProjectRole> GetRoleByProjectIdAndAccountId(int projectId, int accountId)
        {
            var projectRole = await _projectRoleRepository.GetAsync(ft => ft.ProjectId == projectId && ft.AccountId == accountId);
            if (projectRole == null)
                throw new Exception($"Can not find role for account's id: {accountId} in project with id: {projectId}");
            return projectRole;
        }

        public async Task<List<ProjectRole>> GetProjectRolesByIds(List<int> ids) => (await _projectRoleRepository.GetAllAsync(pj => ids.Contains(pj.ProjectId))).ToList();
    }
}
