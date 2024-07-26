using System;
using System.Collections.Generic;

namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class Category
{
    public int Categoryid { get; set; }

    public string Categoryname { get; set; } = null!;

    public virtual ICollection<Blogpost> Blogposts { get; set; } = new List<Blogpost>();
}
