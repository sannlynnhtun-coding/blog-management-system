namespace DotNet8WebApi.PostgreSqlSample.Dtos;

public class AuthorDto
{
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; }
    public List<BlogPostDto> Blogs { get; set; }
}