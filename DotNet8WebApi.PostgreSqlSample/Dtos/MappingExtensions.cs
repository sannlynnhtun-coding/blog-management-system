namespace DotNet8WebApi.PostgreSqlSample.Dtos;

public static class MappingExtensions
{
    public static AuthorDto ToDto(this Author author)
    {
        return new AuthorDto
        {
            AuthorId = author.AuthorId,
            Name = author.Name,
            Email = author.Email,
            Bio = author.Bio,
            Blogs = author.BlogPosts.Select(x=> x.ToDto()).ToList()
        };
    }

    public static CategoryDto ToDto(this Category category)
    {
        return new CategoryDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName
        };
    }

    public static BlogPostDto ToDto(this BlogPost blogPost)
    {
        return new BlogPostDto
        {
            PostId = blogPost.PostId,
            Title = blogPost.Title,
            Content = blogPost.Content,
            AuthorId = Convert.ToInt32(blogPost.AuthorId),
            CategoryId = Convert.ToInt32(blogPost.CategoryId),
            PublishedDate = Convert.ToDateTime(blogPost.PublishedDate),
            LastUpdatedDate = Convert.ToDateTime(blogPost.LastUpdatedDate),
            IsPublished = Convert.ToBoolean(blogPost.IsPublished),
            Tags = blogPost.Tags,
            Author = blogPost.Author.ToAuthorDto(),
            Comments = blogPost.Comments.Select(x=> x.ToDto()).ToList()
        };
    }

    public static BlogPostAuthorDto ToAuthorDto(this Author author)
    {
        return new BlogPostAuthorDto
        {
            AuthorId = author.AuthorId,
            Name = author.Name,
            Email = author.Email,
            Bio = author.Bio,
        };
    }

    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
        {
            CommentId = comment.CommentId,
            PostId = Convert.ToInt32(comment.PostId),
            CommenterName = comment.CommenterName,
            CommenterEmail = comment.CommenterEmail,
            CommentText = comment.CommentText,
            CommentDate = Convert.ToDateTime(comment.CommentDate)
        };
    }
}