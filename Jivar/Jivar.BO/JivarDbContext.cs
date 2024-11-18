using Jivar.BO.Enumarate;
using Jivar.BO.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Task = Jivar.BO.Models.Task;

namespace Jivar.BO;

public partial class JivarDbContext : DbContext
{
    public JivarDbContext()
    {
    }

    public JivarDbContext(DbContextOptions<JivarDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountToken> AccountTokens { get; set; }

    public virtual DbSet<Backlog> Backlogs { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<GroupTask> GroupTasks { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectRole> ProjectRoles { get; set; }

    public virtual DbSet<ProjectSprint> ProjectSprints { get; set; }

    public virtual DbSet<Sprint> Sprints { get; set; }

    public virtual DbSet<SprintTask> SprintTasks { get; set; }

    public virtual DbSet<SubTask> SubTasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskDocument> TaskDocuments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(GetConnectionString());

    private string GetConnectionString()
    {
        IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", true, true)
            .Build();
        var strConn = config["ConnectionStrings:DefaultConnection"];
        return strConn;
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account__3213E83F32BA9A0D");

            entity.ToTable("account");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday)
                .HasColumnType("datetime")
                .HasColumnName("birthday");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(true)
                .HasColumnName("name");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.Verify)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("verify");
        });

        modelBuilder.Entity<AccountToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__account___3213E83F9D1FC0CE");

            entity.ToTable("account_token");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccessToken)
                .IsUnicode(false)
                .HasColumnName("accessToken");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Expired).HasColumnName("expired");
            entity.Property(e => e.RefreshToken)
                .IsUnicode(false)
                .HasColumnName("refreshToken");
            entity.Property(e => e.Revoked).HasColumnName("revoked");
            entity.Property(e => e.TokenType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("token_type");

            entity.HasOne(d => d.Account).WithMany(p => p.AccountTokens)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__account_t__accou__5CD6CB2B");
        });

        modelBuilder.Entity<Backlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__backlog__3213E83F171ED9B7");

            entity.ToTable("backlog");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Assignee).HasColumnName("assignee");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreateBy).HasColumnName("create_by");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Task).WithMany(p => p.Backlogs)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__backlog__task_id__5BE2A6F2");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__comment__3213E83F9989EBED");

            entity.ToTable("comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.CreateBy).HasColumnName("create_by");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TaskId).HasColumnName("task_id");

            entity.HasOne(d => d.Parent).WithMany(p => p.Replies)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__comment__parent___5629CD9C");

            entity.HasOne(d => d.Task).WithMany(p => p.Comments)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__comment__task_id__5AEE82B9");
        });

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__document__3213E83F1A81A4AC");

            entity.ToTable("document");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FilePath).HasColumnName("file_path");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.UploadBy).HasColumnName("upload_by");
            entity.Property(e => e.UploadDate)
                .HasColumnType("datetime")
                .HasColumnName("upload_date");
        });

        modelBuilder.Entity<GroupTask>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.SubtaskId }).HasName("PK__group_ta__48B8D17D3FC89BB3");

            entity.ToTable("group_task");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.SubtaskId).HasColumnName("subtask_id");

            entity.HasOne(d => d.Subtask).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.SubtaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__group_tas__subta__5165187F");

            entity.HasOne(d => d.Task).WithMany(p => p.GroupTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__group_tas__task___5070F446");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__project__3213E83FA7385C0F");

            entity.ToTable("project");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Budget)
                .HasColumnType("money")
                .HasColumnName("budget");
            entity.Property(e => e.CompleteTime)
                .HasColumnType("datetime")
                .HasColumnName("complete_time");
            entity.Property(e => e.CreateBy).HasColumnName("create_by");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasConversion(new ValueConverter<ProjectStatus, string>(
                    v => v.ToString(),
                    v => Enum.Parse<ProjectStatus>(v, true)
                ))
                .IsUnicode(false)
                .HasColumnName("status");
        });

        modelBuilder.Entity<ProjectRole>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.AccountId }).HasName("PK__project___7813BC333D6D1444");

            entity.ToTable("project_role");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .HasConversion(new ValueConverter<ProjectRoleType, string>(
                    v => v.ToString(),
                    v => Enum.Parse<ProjectRoleType>(v, true)
                ))
                .IsUnicode(false)
                .HasColumnName("role");

            entity.HasOne(d => d.Account).WithMany(p => p.ProjectRoles)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__project_r__accou__02084FDA");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectRoles)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__project_r__proje__01142BA1");
        });

        modelBuilder.Entity<ProjectSprint>(entity =>
        {
            entity.HasKey(e => new { e.ProjectId, e.SprintId }).HasName("PK__project___8FEF5F9F3C936FD2");

            entity.ToTable("project_sprint");

            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.SprintId).HasColumnName("sprint_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectSprints)
                .HasForeignKey(d => d.ProjectId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__project_s__proje__52593CB8");

            entity.HasOne(d => d.Sprint).WithMany(p => p.ProjectSprints)
                .HasForeignKey(d => d.SprintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__project_s__sprin__534D60F1");
        });

        modelBuilder.Entity<Sprint>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sprint__3213E83FB3EA1AC4");

            entity.ToTable("sprint");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");
        });

        modelBuilder.Entity<SprintTask>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.SprintId }).HasName("PK__sprint_t__3704D50D22CEFEF0");

            entity.ToTable("sprint_task");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.SprintId).HasColumnName("sprint_id");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("end_date");
            entity.Property(e => e.StartDate)
                .HasColumnType("datetime")
                .HasColumnName("start_date");

            entity.HasOne(d => d.Sprint).WithMany(p => p.SprintTasks)
                .HasForeignKey(d => d.SprintId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sprint_ta__sprin__59063A47");

            entity.HasOne(d => d.Task).WithMany(p => p.SprintTasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__sprint_ta__task___59FA5E80");

            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<SubTask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sub_task__3213E83F88BB52E3");

            entity.ToTable("sub_task");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__task__3213E83FFA2E15E3");

            entity.ToTable("task");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignBy).HasColumnName("assign_by");
            entity.Property(e => e.Assignee).HasColumnName("assignee");
            entity.Property(e => e.CompleteTime)
                .HasColumnType("datetime")
                .HasColumnName("complete_time");
            entity.Property(e => e.CreateBy).HasColumnName("create_by");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<TaskDocument>(entity =>
        {
            entity.HasKey(e => new { e.TaskId, e.DocumentId }).HasName("PK__task_doc__DDF47A079D026BA5");

            entity.ToTable("task_document");

            entity.Property(e => e.TaskId).HasColumnName("task_id");
            entity.Property(e => e.DocumentId).HasColumnName("document_id");


            entity.HasOne(d => d.Document).WithMany(p => p.TaskDocuments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__task_docu__docum__5812160E");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskDocuments)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__task_docu__task___571DF1D5");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
