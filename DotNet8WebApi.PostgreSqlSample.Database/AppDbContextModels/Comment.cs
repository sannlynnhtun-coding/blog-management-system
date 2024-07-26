using System;
using System.Collections.Generic;

namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class Comment
{
    public int CommentId { get; set; }

    public int? PostId { get; set; }

    public string? CommenterName { get; set; }

    public string? CommenterEmail { get; set; }

    public string? CommentText { get; set; }

    public DateTime? CommentDate { get; set; }

    public virtual Blogpost? Post { get; set; }
}
