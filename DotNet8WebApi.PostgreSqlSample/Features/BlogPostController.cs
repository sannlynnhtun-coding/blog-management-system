using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DotNet8WebApi.PostgreSqlSample.Database.AppDbContextModels;

namespace DotNet8WebApi.PostgreSqlSample.Features
{
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
        public async Task<ActionResult<IEnumerable<Blogpost>>> GetBlogPosts()
        {
            return await _context.Blogposts
                //.Include(bp => bp.Author)
                //.Include(bp => bp.Category)
            .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blogpost>> GetBlogPost(int id)
        {
            var blogPost = await _context.Blogposts
                .Include(bp => bp.Author)
                .Include(bp => bp.Category)
                .FirstOrDefaultAsync(bp => bp.Postid == id);

            if (blogPost == null)
            {
                return NotFound();
            }
            return blogPost;
        }

        [HttpPost]
        public async Task<ActionResult<Blogpost>> PostBlogPost(Blogpost blogPost)
        {
            _context.Blogposts.Add(blogPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBlogPost", new { id = blogPost.Postid }, blogPost);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogPost(int id, Blogpost blogPost)
        {
            if (id != blogPost.Postid)
            {
                return BadRequest();
            }

            _context.Entry(blogPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BlogPostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

        private bool BlogPostExists(int id)
        {
            return _context.Blogposts.Any(e => e.Postid == id);
        }
    }
}
