namespace DotNet8WebApi.PostgreSqlSample.Dtos;

public class CommentDto
{
    public int CommentId { get; set; }
    public int PostId { get; set; }
    public string CommenterName { get; set; }
    public string CommenterEmail { get; set; }
    public string CommentText { get; set; }
    public DateTime CommentDate { get; set; }
}