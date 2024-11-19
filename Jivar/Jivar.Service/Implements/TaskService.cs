﻿using Jivar.BO;
using Jivar.BO.Models;
using Jivar.Service.Enums;
using Jivar.Service.Exceptions;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Account.Response;
using Jivar.Service.Payloads.Comment.Response;
using Jivar.Service.Payloads.Tasks.Request;
using Jivar.Service.Payloads.Tasks.Response;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Jivar.Service.Implements
{
    public class TaskService : ITaskService
    {
        private readonly JivarDbContext _dbContext;
        private readonly ICommentService _commentService;
        private readonly IAccountService _accountService;

        public TaskService(JivarDbContext dbContext, ICommentService commentService, IAccountService account)
        {
            _dbContext = dbContext;
            _commentService = commentService;
            _accountService = account;
        }

        public BO.Models.Task CreateTask(BO.Models.Task task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            var result = _dbContext.Tasks.Where(t => t.CreateBy == task.CreateBy).OrderByDescending(t => t.Id).First();
            return result;
        }

        public IEnumerable<BO.Models.Task> getTasks()
        {
            return _dbContext.Tasks.ToList();
        }

        public TaskResponse getTasksById(int taskId)
        {
            BO.Models.Task task = _dbContext.Tasks.Include(c => c.SprintTask).FirstOrDefault(task => task.Id.Equals(taskId));
            if (task.Assignee == null)
            {
                return new TaskResponse(task);
            }
            Account account = _dbContext.Accounts.FirstOrDefault(account => account.Id == (task.Assignee.Value));
            if (account == null)
            {
                throw new Exception("Assignee account not found.");
            }
            return new TaskResponse(task, account);
        }
        public async Task<TaskResponse> GetTasksById(int taskId, int projectId, HttpContext httpContext)
        {
            List<int> accountIds = _dbContext.ProjectRoles.Where(p => p.ProjectId == projectId).Select(p => p.AccountId).Distinct().ToList();
            List<AccountInfoResponse> accounts = (await _accountService.GetAccountsByIds(accountIds)).Select(a => new AccountInfoResponse(a)).ToList();
            BO.Models.Task task = _dbContext.Tasks.Include(c => c.SprintTask).FirstOrDefault(task => task.Id.Equals(taskId));
            if (task.Assignee == null)
            {
                return new TaskResponse(task);
            }

            List<CommentResponse> comments = _commentService.GetCommentByTaskId(taskId, accounts);
            return new TaskResponse(task, accounts.Find(a => a.Id == task.CreateBy), comments);
        }

        public BO.Models.Task updateStatus(int id, string status)
        {
            BO.Models.Task task = _dbContext.Tasks.Include(c=> c.SprintTask).FirstOrDefault(task => task.Id.Equals(id));
            if (task != null)
            {
                // Ensure task.Status is not null before comparing
                if (string.IsNullOrEmpty(task.Status))
                {
                    // Handle case where task.Status is null or empty (optional logic here)
                    task.CompleteTime = null; // or any other default behavior
                }
                else if (task.Status.Equals(TaskEnum.DONE.ToString()) || task.Status.Equals(TaskEnum.TO_DO.ToString()))
                {
                    task.CompleteTime = DateTime.UtcNow;
                }
                else if (task.Status.Equals(TaskEnum.IN_PROGRESS.ToString()))
                {
                    task.CompleteTime = null;
                }

                // Update status
                task.Status = status;

                // Save changes to the database
                _dbContext.SaveChanges();
            }
            return task;
        }

        public BO.Models.Task updateTask(int id, UpdateTaskRequest request)
        {
            BO.Models.Task task = null;
            task = _dbContext.Tasks.FirstOrDefault(task => task.Id.Equals(id));
            SprintTask sprintTask = _dbContext.SprintTasks.FirstOrDefault(st => st.TaskId == task.Id);
            if (sprintTask != null)
            {
                if (request.endDateSprintTask != null && sprintTask.StartDate == null && request.startDateSprintTask == null)
                    throw new BadRequestException("Task must have Start date");
                else if (request.endDateSprintTask != null && sprintTask.StartDate != null && sprintTask.StartDate > request.endDateSprintTask)
                    throw new Exception("Start date must smaller than End date");
            }
            else
            {
                if (request.endDateSprintTask != null && request.startDateSprintTask == null)
                    throw new BadRequestException("Task must have Start date");
            }
            sprintTask.StartDate = request.startDateSprintTask;
            sprintTask.EndDate = request.endDateSprintTask;
            _dbContext.Update(sprintTask);
            if (task != null)
            {
                task.Title = request.Title == null ? task.Title : request.Title;
                task.Description = request.Description == null ? task.Description : request.Description;
                task.AssignBy = request.AssignBy == null ? task.AssignBy : request.AssignBy;
                task.Assignee = request.Assignee == null ? task.Assignee : request.Assignee;
                task.DocumentId = request.DocumentId == null ? task.DocumentId : request.DocumentId;
                _dbContext.Update(task);
                _dbContext.SaveChanges();
            }
            return task;
        }
    }
}
