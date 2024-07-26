namespace DotNet8WebApi.PostgreSqlSample.Dtos;

public class BlogPostDto
{
    public int PostID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public int AuthorID { get; set; }
    public int CategoryID { get; set; }
    public DateTime PublishedDate { get; set; }
    public DateTime LastUpdatedDate { get; set; }
    public bool IsPublished { get; set; }
    public string Tags { get; set; }
}