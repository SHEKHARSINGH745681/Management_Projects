using Book_Management.Data;
using Book_Management.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly ApplicationDbContext DbContext;

    public BookController(ApplicationDbContext dbContext)
    {
        DbContext = dbContext;
    }

    // GET
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
    {
        var books = await DbContext.Books.ToListAsync();

        if (books == null || books.Count == 0)
        {
            return NotFound(new { Message = "No books found." });
        }

        var response = new
        {
            Message = "Here are all the books.",
            Books = books
        };

        return Ok(response);
    }

    // POST
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new { Message = "Invalid data." });
        }

        DbContext.Books.Add(book);
        await DbContext.SaveChangesAsync();

        return Ok(book);
    }

    // Other actions (PUT, DELETE) are similarly secured
}
