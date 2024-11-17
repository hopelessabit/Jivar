using Jivar.BO.Enumarate;

namespace Jivar.BO.Models;

public partial class ProjectRole
{
    public int ProjectId { get; set; }

    public int AccountId { get; set; }

    public ProjectRoleType Role { get; set; }

    public virtual Project? Project { get; set; }

    public virtual Account? Account { get; set; }
}
