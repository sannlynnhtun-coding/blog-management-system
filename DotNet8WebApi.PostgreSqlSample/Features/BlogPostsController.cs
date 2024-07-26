using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;
using DotNet8WebApi.PostgreSqlSample.Dtos;

namespace DotNet8WebApi.PostgreSqlSample.Features;

[Route("api/[controller]")]
[ApiController]
public class BlogPostsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BlogPostsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BlogPostDto>>> GetBlogPosts()
    {
        var blogPosts = await _context.Blogposts.ToListAsync();
        return blogPosts.Select(bp => bp.ToDto()).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostDto>> GetBlogPost(int id)
    {
        var blogPost = await _context.Blogposts.FindAsync(id);

        if (blogPost == null)
        {
            return NotFound();
        }

        return blogPost.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<BlogPostDto>> PostBlogPost(BlogPostDto blogPostDto)
    {
        var blogPost = new Blogpost
        {
            Title = blogPostDto.Title,
            Content = blogPostDto.Content,
            AuthorId = blogPostDto.AuthorID,
            CategoryId = blogPostDto.CategoryID,
            PublishedDate = blogPostDto.PublishedDate,
            LastUpdatedDate = blogPostDto.LastUpdatedDate,
            IsPublished = blogPostDto.IsPublished,
            Tags = blogPostDto.Tags
        };

        _context.Blogposts.Add(blogPost);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.PostId }, blogPost.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlogPost(int id, BlogPostDto blogPostDto)
    {
        if (id != blogPostDto.PostID)
        {
            return BadRequest();
        }

        var blogPost = await _context.Blogposts.FindAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        blogPost.Title = blogPostDto.Title;
        blogPost.Content = blogPostDto.Content;
        blogPost.AuthorId = blogPostDto.AuthorID;
        blogPost.CategoryId = blogPostDto.CategoryID;
        blogPost.PublishedDate = blogPostDto.PublishedDate;
        blogPost.LastUpdatedDate = blogPostDto.LastUpdatedDate;
        blogPost.IsPublished = blogPostDto.IsPublished;
        blogPost.Tags = blogPostDto.Tags;

        _context.Entry(blogPost).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBlogPost(int id)
    {
        var blogPost = await _context.Blogposts.FindAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        _context.Blogposts.Remove(blogPost);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}