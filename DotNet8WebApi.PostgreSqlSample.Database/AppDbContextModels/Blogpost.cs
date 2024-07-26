namespace DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

public partial class BlogPost
{
    public int PostId { get; set; }

    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public int? AuthorId { get; set; }

    public int? CategoryId { get; set; }

    public DateTime? PublishedDate { get; set; }

    public DateTime? LastUpdatedDate { get; set; }

    public bool? IsPublished { get; set; }

    public string? Tags { get; set; }

    public virtual Author? Author { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
