using System;
using System.Collections.Generic;

namespace Jivar.BO.abc;

public partial class AccountToken
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string? AccessToken { get; set; }

    public string? RefreshToken { get; set; }

    public string? TokenType { get; set; }

    public bool? Expired { get; set; }

    public bool? Revoked { get; set; }

    public virtual Account? Account { get; set; }
}
