using Jivar.BO;
using Jivar.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using Jivar.BO.Models;

namespace Jivar.Service.Implements
{
    public class ExcelExportSerivce : IExcelExportSerivce
    {
        private readonly JivarDbContext _context;

        public ExcelExportSerivce(JivarDbContext context)
        {
            _context = context;
        }

        public byte[] ExportProjectById(int id)
        {
            // Set the license context for EPPlus
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var projects = _context.Projects
                .Where(p => p.Id == id)
                .Include(p => p.ProjectRoles)
                .ThenInclude(pr => pr.Account)
                .Include(p => p.ProjectSprints)
                .ThenInclude(ps => ps.Sprint)
                .ThenInclude(s => s.SprintTasks)
                .ThenInclude(st => st.Task)
                .ThenInclude(t => t.Comments)
                .ThenInclude(c => c.Replies)
                .Include(p => p.ProjectSprints)
                .ThenInclude(ps => ps.Sprint)
                .ThenInclude(s => s.SprintTasks)
                .ThenInclude(st => st.Task)
                .ThenInclude(t => t.Backlogs)
                .Include(p => p.ProjectSprints)
                .ThenInclude(ps => ps.Sprint)
                .ThenInclude(s => s.SprintTasks)
                .ThenInclude(st => st.Task)
                .ThenInclude(t => t.GroupTasks)
                .ThenInclude(gt => gt.Subtask)
                .Include(p => p.ProjectSprints)
                .ThenInclude(ps => ps.Sprint)
                .ThenInclude(s => s.SprintTasks)
                .ThenInclude(st => st.Task)
                .ThenInclude(t => t.TaskDocuments)
                .ThenInclude(td => td.Document)
                .ToList();

            using (var package = new ExcelPackage())
            {
                // Define header and alternating row styles dynamically
                Action<ExcelRange> ApplyHeaderStyle = (range) =>
                {
                    range.Style.Font.Bold = true;
                    range.Style.Font.Size = 12;
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    range.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    range.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                    range.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                };

                Action<ExcelRange> ApplyAlternatingRowStyle = (range) =>
                {
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                };

                // Add Project Details Worksheet
                var projectWorksheet = package.Workbook.Worksheets.Add("Project Details");
                projectWorksheet.Cells[1, 1].Value = "Project ID";
                projectWorksheet.Cells[1, 2].Value = "Name";
                projectWorksheet.Cells[1, 3].Value = "Description";
                projectWorksheet.Cells[1, 4].Value = "Status";
                projectWorksheet.Cells[1, 5].Value = "Sprint ID";
                projectWorksheet.Cells[1, 6].Value = "Sprint Name";
                projectWorksheet.Cells[1, 7].Value = "Sprint Start Date";
                projectWorksheet.Cells[1, 8].Value = "Sprint End Date";

                // Apply header style
                ApplyHeaderStyle(projectWorksheet.Cells[1, 1, 1, 8]);

                int projectRow = 2;

                foreach (var project in projects)
                {
                    foreach (var sprint in project.ProjectSprints.Select(ps => ps.Sprint))
                    {
                        if (sprint == null) continue;

                        // Write Project Details
                        projectWorksheet.Cells[projectRow, 1].Value = project.Id;
                        projectWorksheet.Cells[projectRow, 2].Value = project.Name;
                        projectWorksheet.Cells[projectRow, 3].Value = project.Description;
                        projectWorksheet.Cells[projectRow, 4].Value = project.Status;
                        projectWorksheet.Cells[projectRow, 5].Value = sprint.Id;
                        projectWorksheet.Cells[projectRow, 6].Value = sprint.Name;
                        projectWorksheet.Cells[projectRow, 7].Value = sprint.StartDate?.ToString("yyyy-MM-dd");
                        projectWorksheet.Cells[projectRow, 8].Value = sprint.EndDate?.ToString("yyyy-MM-dd");
                        projectRow++;
                    }
                }

                projectWorksheet.Cells[projectWorksheet.Dimension.Address].AutoFitColumns();

                foreach (var project in projects)
                {
                    foreach (var sprint in project.ProjectSprints.Select(ps => ps.Sprint))
                    {
                        if (sprint == null) continue;

                        // Add Sprint Worksheet
                        var sprintWorksheet = package.Workbook.Worksheets.Add($"Sprint {sprint.Id}");
                        sprintWorksheet.Cells[1, 1].Value = "Task ID";
                        sprintWorksheet.Cells[1, 2].Value = "Title";
                        sprintWorksheet.Cells[1, 3].Value = "Start Date";
                        sprintWorksheet.Cells[1, 4].Value = "End Date";
                        sprintWorksheet.Cells[1, 5].Value = "Assign By";
                        sprintWorksheet.Cells[1, 6].Value = "Assign By Role";
                        sprintWorksheet.Cells[1, 7].Value = "Assignee";
                        sprintWorksheet.Cells[1, 8].Value = "Assignee Role";
                        sprintWorksheet.Cells[1, 9].Value = "Document ID";
                        sprintWorksheet.Cells[1, 10].Value = "Document Name";
                        sprintWorksheet.Cells[1, 11].Value = "Document Path";

                        // Apply header style
                        ApplyHeaderStyle(sprintWorksheet.Cells[1, 1, 1, 11]);

                        int taskRow = 2;

                        foreach (var sprintTask in sprint.SprintTasks)
                        {
                            var task = sprintTask.Task;
                            if (task == null) continue;

                            // Assign By and Assignee details
                            var assignBy = project.ProjectRoles.FirstOrDefault(pr => pr.AccountId == task.AssignBy)?.Account;
                            var assignee = project.ProjectRoles.FirstOrDefault(pr => pr.AccountId == task.Assignee)?.Account;

                            // Write Task Details
                            sprintWorksheet.Cells[taskRow, 1].Value = task.Id;
                            sprintWorksheet.Cells[taskRow, 2].Value = task.Title;
                            sprintWorksheet.Cells[taskRow, 3].Value = sprintTask.StartDate?.ToString("yyyy-MM-dd");
                            sprintWorksheet.Cells[taskRow, 4].Value = sprintTask.EndDate?.ToString("yyyy-MM-dd");
                            sprintWorksheet.Cells[taskRow, 5].Value = assignBy?.Name ?? "None";
                            sprintWorksheet.Cells[taskRow, 6].Value = assignBy?.Role ?? "None";
                            sprintWorksheet.Cells[taskRow, 7].Value = assignee?.Name ?? "None";
                            sprintWorksheet.Cells[taskRow, 8].Value = assignee?.Role ?? "None";

                            // Write Document Details
                            if (task.TaskDocuments != null && task.TaskDocuments.Any())
                            {
                                foreach (var document in task.TaskDocuments.Select(td => td.Document))
                                {
                                    sprintWorksheet.Cells[taskRow, 9].Value = document.Id;
                                    sprintWorksheet.Cells[taskRow, 10].Value = document.Name;
                                    sprintWorksheet.Cells[taskRow, 11].Value = document.FilePath;

                                    if (taskRow % 2 == 0)
                                        ApplyAlternatingRowStyle(sprintWorksheet.Cells[taskRow, 1, taskRow, 11]);

                                    taskRow++;
                                }
                            }
                            else
                            {
                                sprintWorksheet.Cells[taskRow, 9].Value = "No Document";
                                sprintWorksheet.Cells[taskRow, 10].Value = "No Document";
                                sprintWorksheet.Cells[taskRow, 11].Value = "No Document";

                                if (taskRow % 2 == 0)
                                    ApplyAlternatingRowStyle(sprintWorksheet.Cells[taskRow, 1, taskRow, 11]);

                                taskRow++;
                            }

                            // Add Task Worksheet
                            if ((task.Backlogs != null && task.Backlogs.Any()) ||
                                (task.Comments != null && task.Comments.Any()) ||
                                (task.GroupTasks != null && task.GroupTasks.Any()))
                            {
                                var taskWorksheet = package.Workbook.Worksheets.Add($"Task {task.Id}");

                                // Task Worksheet Headers
                                taskWorksheet.Cells[1, 1].Value = "SubTask ID";
                                taskWorksheet.Cells[1, 2].Value = "SubTask Title";
                                taskWorksheet.Cells[1, 3].Value = "SubTask Description";
                                taskWorksheet.Cells[1, 4].Value = "SubTask Status";
                                taskWorksheet.Cells[1, 5].Value = "Backlog ID";
                                taskWorksheet.Cells[1, 6].Value = "Content";
                                taskWorksheet.Cells[1, 7].Value = "Created By";
                                taskWorksheet.Cells[1, 8].Value = "Created By Role";
                                taskWorksheet.Cells[1, 9].Value = "Assignee";
                                taskWorksheet.Cells[1, 10].Value = "Assignee Role";
                                taskWorksheet.Cells[1, 11].Value = "Comments (Hierarchy)";

                                // Apply header style
                                ApplyHeaderStyle(taskWorksheet.Cells[1, 1, 1, 11]);

                                int currentRow = 2; // Start writing data directly under the header

                                // Add Subtasks
                                foreach (var groupTask in task.GroupTasks)
                                {
                                    var subTask = groupTask.Subtask;
                                    if (subTask != null)
                                    {
                                        taskWorksheet.Cells[currentRow, 1].Value = subTask.Id;
                                        taskWorksheet.Cells[currentRow, 2].Value = subTask.Title;
                                        taskWorksheet.Cells[currentRow, 3].Value = subTask.Description;
                                        taskWorksheet.Cells[currentRow, 4].Value = subTask.Status;

                                        // Apply alternating row style
                                        if (currentRow % 2 == 0)
                                            ApplyAlternatingRowStyle(taskWorksheet.Cells[currentRow, 1, currentRow, 4]);

                                        currentRow++;
                                    }
                                }

                                currentRow = 2; // Start writing data directly under the header
                                // Add Backlogs immediately below Subtasks
                                foreach (var backlog in task.Backlogs)
                                {
                                    var backlogCreatedBy = project.ProjectRoles.FirstOrDefault(pr => pr.AccountId == backlog.CreateBy)?.Account;
                                    var backlogAssignee = project.ProjectRoles.FirstOrDefault(pr => pr.AccountId == backlog.Assignee)?.Account;

                                    taskWorksheet.Cells[currentRow, 5].Value = backlog.Id;
                                    taskWorksheet.Cells[currentRow, 6].Value = backlog.Content;
                                    taskWorksheet.Cells[currentRow, 7].Value = backlogCreatedBy?.Name ?? "Unknown";
                                    taskWorksheet.Cells[currentRow, 8].Value = backlogCreatedBy?.Role ?? "Unknown";
                                    taskWorksheet.Cells[currentRow, 9].Value = backlogAssignee?.Name ?? "None";
                                    taskWorksheet.Cells[currentRow, 10].Value = backlogAssignee?.Role ?? "None";

                                    // Apply alternating row style
                                    if (currentRow % 2 == 0)
                                        ApplyAlternatingRowStyle(taskWorksheet.Cells[currentRow, 5, currentRow, 10]);

                                    currentRow++;
                                }

                                currentRow = 2; // Start writing data directly under the header
                                // Add Comments immediately below Backlogs
                                foreach (var comment in task.Comments.Where(c => c.ParentId == null))
                                {
                                    AddCommentHierarchyWithRole(taskWorksheet, comment, currentRow++, 11, project.ProjectRoles);
                                }

                                // Auto-fit columns for better readability
                                taskWorksheet.Cells[taskWorksheet.Dimension.Address].AutoFitColumns();
                            }
                        }

                        sprintWorksheet.Cells[sprintWorksheet.Dimension.Address].AutoFitColumns();
                    }
                }

                return package.GetAsByteArray();
            }
        }



        /// <summary>
        /// Adds a comment and its child comments recursively into the worksheet,
        /// starting directly under the "Comments (Hierarchy)" title column.
        /// </summary>
        private void AddCommentHierarchyWithRole(ExcelWorksheet worksheet, Comment comment, int row, int indentColumn, ICollection<ProjectRole> projectRoles)
        {
            var author = projectRoles.FirstOrDefault(pr => pr.AccountId == comment.CreateBy)?.Account;
            var authorName = author?.Name ?? "Unknown";
            var authorRole = author?.Role ?? "Unknown";

            worksheet.Cells[row, indentColumn].Value = $"{comment.Content} (By {authorName}, Role: {authorRole}, Status: {comment.Status})";

            int childRow = row + 1;
            foreach (var childComment in comment.Replies)
            {
                AddCommentHierarchyWithRole(worksheet, childComment, childRow, indentColumn + 1, projectRoles);
                childRow += CountCommentHierarchy(childComment);
            }
        }

        /// <summary>
        /// Counts the total number of comments in a hierarchy, including the parent and all descendants.
        /// </summary>
        private int CountCommentHierarchy(Comment comment)
        {
            return 1 + comment.Replies.Sum(CountCommentHierarchy);
        }
    }
}
