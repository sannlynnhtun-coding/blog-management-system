using DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

namespace DotNet8WebApi.PostgreSqlSample.Dtos
{
    public class AuthorDto
    {
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
    }

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

    public class CategoryDto
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    }

    public class CommentDto
    {
        public int CommentID { get; set; }
        public int PostID { get; set; }
        public string CommenterName { get; set; }
        public string CommenterEmail { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }

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
}
