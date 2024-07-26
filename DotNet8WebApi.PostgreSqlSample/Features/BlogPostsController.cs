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
        var blogPosts = await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Comments)
            .ToListAsync();
        return blogPosts.Select(bp => bp.ToDto()).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BlogPostDto>> GetBlogPost(int id)
    {
        var blogPost = await _context.BlogPosts
            .Include(x => x.Author)
            .Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.PostId == id);

        if (blogPost == null)
        {
            return NotFound();
        }

        return blogPost.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<BlogPostDto>> PostBlogPost(BlogPostDto blogPostDto)
    {
        var blogPost = new BlogPost
        {
            Title = blogPostDto.Title,
            Content = blogPostDto.Content,
            AuthorId = blogPostDto.AuthorId,
            CategoryId = blogPostDto.CategoryId,
            PublishedDate = blogPostDto.PublishedDate,
            LastUpdatedDate = blogPostDto.LastUpdatedDate,
            IsPublished = blogPostDto.IsPublished,
            Tags = blogPostDto.Tags
        };

        _context.BlogPosts.Add(blogPost);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.PostId }, blogPost.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutBlogPost(int id, BlogPostDto blogPostDto)
    {
        if (id != blogPostDto.PostId)
        {
            return BadRequest();
        }

        var blogPost = await _context.BlogPosts.FindAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        blogPost.Title = blogPostDto.Title;
        blogPost.Content = blogPostDto.Content;
        blogPost.AuthorId = blogPostDto.AuthorId;
        blogPost.CategoryId = blogPostDto.CategoryId;
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
        var blogPost = await _context.BlogPosts.FindAsync(id);
        if (blogPost == null)
        {
            return NotFound();
        }

        _context.BlogPosts.Remove(blogPost);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}