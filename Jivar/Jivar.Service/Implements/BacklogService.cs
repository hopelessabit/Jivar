using Jivar.BO.Models;
using Jivar.Repository.Interface;
using Jivar.Service.Interfaces;
using Jivar.Service.Payloads.Backlog.Request;
using Jivar.Service.Util;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Jivar.Service.Constant.APIEndPointConstant;

namespace Jivar.Service.Implements
{
    public class BacklogService : IBacklogService
    {
        private readonly IBacklogRepository _backlogRepository;
        public BacklogService(IBacklogRepository backlogRepository)
        {
            _backlogRepository = backlogRepository;
        }

        public async Task<List<Backlog>> getBacklogByTaskId(int taskId)
        {
            return (await _backlogRepository.GetAllAsync(ft => ft.TaskId == taskId)).ToList();
        }


        public async Task<List<Backlog>> getBacklogByTaskIds(List<int> taskIds)
        {
            return (await _backlogRepository.GetAllAsync(ft => ft.TaskId != null && taskIds.Contains(ft.TaskId.Value))).ToList();
        }

        
        public async Task<List<Backlog>> getBacklogByProjectId(int projectId)
        {
            return (await _backlogRepository.GetAllAsync(ft => ft.ProjectId == projectId)).ToList();
        }

        public async Task<bool> createBacklog(CreateBacklogRequest request, HttpContext context){
            Backlog backlog = new Backlog()
            {
                TaskId = request.TaskId,
                ProjectId = request.ProjectId,
                Content = request.Content,
                Assignee = request.Assignee,
                CreateTime = DateTime.UtcNow,
                CreateBy = UserUtil.GetAccountId(context)
            };

            return await _backlogRepository.AddAsync(backlog);
        }

        public async Task<bool> updateBacklog(int backlogId,UpdateBacklogRequest request, HttpContext context)
        {
            if (backlogId < 0)
                throw new Exception("Backlog's Id must be bigger than 0");
            if (request.Assignee != null && request.Assignee < 0)
                throw new Exception("Assignee's Id must be bigger than 0");

            Backlog? backlog = await _backlogRepository.GetAsync(ft => ft.Id == backlogId);

            if (backlog == null)
                throw new Exception($"Not found backlog with id: {backlogId}"); 

            if (request.ProjectId != null) backlog.ProjectId = request.ProjectId;
            if (request.Content != null) backlog.Content = request.Content;
            if (request.TaskId != null) backlog.TaskId = request.TaskId;
            if (request.Assignee != null) backlog.Assignee = request.Assignee;

            return await _backlogRepository.AddAsync(backlog);
        }

        public async Task<Backlog> GetBacklogById(int id)
        {
            Backlog? backlog = await _backlogRepository.GetAsync(ft => ft.Id == id);
            if (backlog == null)
                throw new Exception("Backlog with id {id} not found.");
            return backlog;
        }

        public async Task<bool> deleteBacklog(int backlogId)
        {
            Backlog backlog = GetBacklogById(backlogId).Result;
            return await _backlogRepository.DeleteAsync(backlog);
        }
    }
}
