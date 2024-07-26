using System;
using System.Collections.Generic;

namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class Blogpost
{
    public int Postid { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? Authorid { get; set; }

    public int? Categoryid { get; set; }

    public DateTime? Publisheddate { get; set; }

    public DateTime? Lastupdateddate { get; set; }

    public bool? Ispublished { get; set; }

    public string? Tags { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
