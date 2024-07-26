using System;
using System.Collections.Generic;

namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class Author
{
    public int Authorid { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Bio { get; set; }

    public virtual ICollection<Blogpost> Blogposts { get; set; } = new List<Blogpost>();
}
