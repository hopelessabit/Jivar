using System;
using System.Collections.Generic;

namespace Jivar.BO.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
    public string? Name { get; set; }

    public string? Phone { get; set; }

    public string? Username { get; set; }

    public DateTime? Birthday { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreateTime { get; set; }

    public string? Role { get; set; }

    public virtual ICollection<AccountToken> AccountTokens { get; set; } = new List<AccountToken>();

    public virtual ICollection<ProjectRole> ProjectRoles { get; set; } = new List<ProjectRole>();
}
