namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class Category
{
    public int CategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public virtual ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
}
