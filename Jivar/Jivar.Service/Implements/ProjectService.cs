using Google.Apis.Util;
using Jivar.BO.Enumarate;
using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Jivar.Service.Paging;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Project.Request;
using Jivar.Service.Payloads.Project.Response;
using Jivar.Service.Payloads.ProjectRole.Response;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Numerics;
using System.Text;

namespace Jivar.Service.Implements
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectRoleService _roleService;
        private readonly IAccountService _accountSerivce;

        public ProjectService(IProjectRepository projectRepository,
            IProjectRoleService roleService,
            IAccountService accountService)
        {
            _projectRepository = projectRepository;
            _roleService = roleService; 
            _accountSerivce = accountService;
        }

        // Get a project by ID
        public async Task<Project> GetProjectById(int id)
        {
            var project = await _projectRepository.GetAsync(p => p.Id == id);
            if (project == null)
                throw new Exception($"Project with ID {id} not found.");
            return project;
        }

        public async Task<ProjectResonse> GetProjectResponseById(int id)
        {
            Project project = await GetProjectById(id);
            Account createBy = await _accountSerivce.GetAccountById(project.CreateBy);
            return new ProjectResonse(project, createBy);
        }

        // Get all projects
        public async Task<List<Project>> GetAllProjects()
        {
            return (await _projectRepository.GetAllAsync()).ToList();
        }

        public async Task<PagedResult<ProjectResonse>> GetProjects(PagingAndSortingParams pagingParams, string? searchTerm = null, bool? includeSprint = false, bool? includeRole = false)
        {
            // Define the filter for searching by name
            Expression<Func<Project, bool>>? filter = null;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                string normalizedSearchTerm = RemoveDiacritics(searchTerm.ToLower());
                filter = p => RemoveDiacritics(p.Name.ToLower()).Contains(normalizedSearchTerm);
            }

            // Define the sorting logic
            Func<IQueryable<Project>, IOrderedQueryable<Project>>? orderBy = null;
            if (!string.IsNullOrEmpty(pagingParams.SortBy))
            {
                orderBy = pagingParams.IsDescending
                    ? q => q.OrderByDescending(e => EF.Property<object>(e, pagingParams.SortBy))
                    : q => q.OrderBy(e => EF.Property<object>(e, pagingParams.SortBy));
            }

            // Fetch data using the repository method
            var projects = _projectRepository.GetAllWithPagingAndSorting(
                filter: filter,
                includeProperties: pagingParams.IncludeProperties,
                orderBy: orderBy,
                pageNumber: pagingParams.PageNumber,
                pageSize: pagingParams.PageSize
            );

            List<AccountInfoResponse> accountInfos = (await _accountSerivce.GetAccountsByIds(projects.Select(p => p.CreateBy).Distinct().ToList())).Select(a => new AccountInfoResponse(a)).ToList();

            List<ProjectResonse> result = projects.Select(p => new ProjectResonse(p, accountInfos.Find(a => a.Id == p.CreateBy).ThrowIfNull($"Account with Id: {p.CreateBy} not found"))).ToList();
            
            if (includeRole != null && includeRole.Value)
            {
                List<ProjectRole> roles = await _roleService.GetProjectRolesByIds(projects.Select(p => p.Id).ToList());
                accountInfos.AddRange((await _accountSerivce.GetAccountsByIds(roles.Select(r => r.AccountId).Distinct().ToList())).Select(a => new AccountInfoResponse(a)).ToList());

                List<ProjectRoleResponse> roleResponses = new List<ProjectRoleResponse>();
                roles.ForEach(r => roleResponses.Add(new ProjectRoleResponse(accountInfos.Find(a => a.Id == r.AccountId).ThrowIfNull($"Account with Id: {r.AccountId} not found"), r)));

                foreach (var item in result)
                    item.Roles = roleResponses.FindAll(rr => roles.FindAll(r => r.ProjectId == item.Id).ToList().Select(r => r.AccountId).Contains(rr.AccountId));

            }

            // Calculate the total record count
            var totalRecords = filter == null
                ? _projectRepository.GetAllWithPagingAndSorting().Count()
                : _projectRepository.GetAllWithPagingAndSorting(filter).Count();

            // Return the paginated result
            return new PagedResult<ProjectResonse>
            {
                TotalRecords = totalRecords,
                PageNumber = pagingParams.PageNumber,
                PageSize = pagingParams.PageSize,
                Data = result
            };
        }

        // Utility method to remove diacritics
        private string RemoveDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            var normalizedText = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }


        // Create a new project
        public async Task<bool> CreateProject(CreateProjectRequest request, HttpContext context)
        {
            // Validate the request
            if (request == null)
                throw new ArgumentNullException(nameof(request), "CreateProjectRequest cannot be null.");

            int accountId = UserUtil.GetAccountId(context);
            Account account = await _accountSerivce.GetAccountById(accountId);

            // Create a new project
            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                Budget = request.Budget,
                Status = ProjectStatus.Pending,
                CreateBy = accountId,
                CreateTime = DateTime.UtcNow
            };

            // Add the project to the repository
            var success = await _projectRepository.AddAsync(project);
            if (!success)
                throw new Exception("Failed to create project in the database.");

            // Retrieve the latest project created by the user
            Project? latestProject = (await _projectRepository.GetAllAsync(ft => ft.CreateBy == accountId))
                .OrderByDescending(ft => ft.Id)
                .FirstOrDefault();

            if (latestProject == null)
                throw new Exception("Failed to retrieve the created project from the database.");

            // Create a project role for the user
            ProjectRole role = new ProjectRole
            {
                ProjectId = latestProject.Id,
                AccountId = accountId,
                Role = ProjectRoleType.Owner // Set the user as the Owner
            };

            var roleSuccess = await _roleService.AddRoleToProject(role, context);
            if (!roleSuccess)
            {
                _projectRepository.Delete(project);
                throw new Exception("Failed to add the user's role to the project.");
            }
            return true;
        }


        // Update an existing project
        public async Task<bool> UpdateProject(int projectId, UpdateProjectRequest request, HttpContext context)
        {
            int accountId = UserUtil.GetAccountId(context);
            Account account = await _accountSerivce.GetAccountById(accountId);

            ProjectRole role = await _roleService.GetRoleByProjectIdAndAccountId(projectId, accountId);

            if (role.Role != ProjectRoleType.Owner && role.Role != ProjectRoleType.Manager)
                throw new UnauthorizedAccessException("Only users with Owner or Manager roles can update the project.");

            var existingProject = await _projectRepository.GetAsync(p => p.Id == projectId);
            if (existingProject == null)
                throw new Exception($"Project with ID {projectId} not found.");

            if (!string.IsNullOrEmpty(request.Name))
                existingProject.Name = request.Name;

            if (!string.IsNullOrEmpty(request.Description))
                existingProject.Description = request.Description;

            if (request.Budget.HasValue)
                existingProject.Budget = request.Budget;

            if (request.Status != null)
            {
                ProjectStatus? newStatus = request.Status;

                switch (existingProject.Status)
                {
                    case ProjectStatus.Pending:
                        if (newStatus != ProjectStatus.Active && newStatus != ProjectStatus.Cancelled)
                            throw new Exception("When the current status is Pending, it can only be updated to Active or Cancelled.");
                        break;

                    case ProjectStatus.Active:
                        if (newStatus != ProjectStatus.Completed)
                            throw new Exception("When the current status is Active, it can only be updated to Completed.");
                        break;

                    case ProjectStatus.Completed:
                    case ProjectStatus.Cancelled:
                        throw new Exception("Status updates are not allowed for projects that are already Completed or Cancelled.");

                    default:
                        throw new Exception("Invalid current status.");
                }

                if (newStatus == ProjectStatus.Completed)
                    existingProject.CompleteTime = DateTime.UtcNow;

                // Update the status
                existingProject.Status = newStatus.Value;
            }

            // Update the project in the repository
            return await _projectRepository.UpdateAsync(existingProject);
        }


        // Delete a project
        public async Task<bool> DeleteProject(int projectId)
        {
            var project = await _projectRepository.GetAsync(p => p.Id == projectId);
            if (project == null)
                throw new Exception($"Project with ID {projectId} not found.");
            return await _projectRepository.DeleteAsync(project);
        }

    }
}
