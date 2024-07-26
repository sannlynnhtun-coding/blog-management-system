using DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

namespace DotNet8WebApi.PostgreSqlSample.Dtos;

public static class MappingExtensions
{
    public static AuthorDto ToDto(this Author author)
    {
        return new AuthorDto
        {
            AuthorID = author.AuthorId,
            Name = author.Name,
            Email = author.Email,
            Bio = author.Bio
        };
    }

    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryID = category.CategoryId,
            CategoryName = category.CategoryName
        };
    }

    public static BlogPostDto ToDto(this Blogpost blogPost)
    {
        return new BlogPostDto
        {
            PostID = blogPost.PostId,
            Title = blogPost.Title,
            Content = blogPost.Content,
            AuthorID = Convert.ToInt32(blogPost.AuthorId),
            CategoryID = Convert.ToInt32(blogPost.CategoryId),
            PublishedDate = Convert.ToDateTime(blogPost.PublishedDate),
            LastUpdatedDate = Convert.ToDateTime(blogPost.LastUpdatedDate),
            IsPublished = Convert.ToBoolean(blogPost.IsPublished),
            Tags = blogPost.Tags
        };
    }

    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
        {
            CommentID = comment.CommentId,
            PostID = Convert.ToInt32(comment.PostId),
            CommenterName = comment.CommenterName,
            CommenterEmail = comment.CommenterEmail,
            CommentText = comment.CommentText,
            CommentDate = Convert.ToDateTime(comment.CommentDate)
        };
    }
}