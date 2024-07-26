namespace BlogAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
    {
        var authors = await _context.Authors
            .Include(x=> x.BlogPosts)
            .ToListAsync();
        return authors.Select(a => a.ToDto()).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
    {
        var author = await _context.Authors
            .Include(x=> x.BlogPosts)
            .FirstOrDefaultAsync(x=> x.AuthorId == id);

        if (author == null)
        {
            return NotFound();
        }

        return author.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<AuthorDto>> PostAuthor(AuthorDto authorDto)
    {
        var author = new Author
        {
            Name = authorDto.Name,
            Email = authorDto.Email,
            Bio = authorDto.Bio
        };

        _context.Authors.Add(author);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAuthor), new { id = author.AuthorId }, author.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthor(int id, AuthorDto authorDto)
    {
        if (id != authorDto.AuthorId)
        {
            return BadRequest();
        }

        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        author.Name = authorDto.Name;
        author.Email = authorDto.Email;
        author.Bio = authorDto.Bio;

        _context.Entry(author).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var author = await _context.Authors.FindAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(author);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}