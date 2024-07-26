namespace DotNet8WebApi.PostgreSqlSample.Features;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly AppDbContext _context;

    public CommentsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
    {
        var comments = await _context.Comments.ToListAsync();
        return comments.Select(c => c.ToDto()).ToList();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CommentDto>> GetComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null)
        {
            return NotFound();
        }

        return comment.ToDto();
    }

    [HttpPost]
    public async Task<ActionResult<CommentDto>> PostComment(CommentDto commentDto)
    {
        var comment = new Comment
        {
            PostId = commentDto.PostId,
            CommenterName = commentDto.CommenterName,
            CommenterEmail = commentDto.CommenterEmail,
            CommentText = commentDto.CommentText,
            CommentDate = commentDto.CommentDate
        };

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetComment), new { id = comment.CommentId }, comment.ToDto());
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutComment(int id, CommentDto commentDto)
    {
        if (id != commentDto.CommentId)
        {
            return BadRequest();
        }

        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        comment.PostId = commentDto.PostId;
        comment.CommenterName = commentDto.CommenterName;
        comment.CommenterEmail = commentDto.CommenterEmail;
        comment.CommentText = commentDto.CommentText;
        comment.CommentDate = commentDto.CommentDate;

        _context.Entry(comment).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if (comment == null)
        {
            return NotFound();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}