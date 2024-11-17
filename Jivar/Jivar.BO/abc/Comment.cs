﻿using System;
using System.Collections.Generic;

namespace Jivar.BO.abc;

public partial class Comment
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int? TaskId { get; set; }

    public int? CreateBy { get; set; }

    public int? ParentId { get; set; }

    public DateTime? CreateTime { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Comment> InverseParent { get; set; } = new List<Comment>();

    public virtual Comment? Parent { get; set; }

    public virtual Task? Task { get; set; }
}