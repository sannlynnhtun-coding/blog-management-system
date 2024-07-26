using System;
using System.Collections.Generic;

namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class Comment
{
    public int Commentid { get; set; }

    public int? Postid { get; set; }

    public string? Commentername { get; set; }

    public string? Commenteremail { get; set; }

    public string? Commenttext { get; set; }

    public DateTime? Commentdate { get; set; }

    public virtual Blogpost? Post { get; set; }
}
