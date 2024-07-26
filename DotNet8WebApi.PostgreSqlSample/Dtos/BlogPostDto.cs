namespace DotNet8WebApi.PostgreSqlSample.Dtos;

public class BlogPostDto
{
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public DateTime PublishedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public bool IsPublished { get; set; }
    public string Tags { get; set; }
    public BlogPostAuthorDto Author { get; set; }
    public List<CommentDto> Comments { get; set; }
}

public class BlogPostAuthorDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
}