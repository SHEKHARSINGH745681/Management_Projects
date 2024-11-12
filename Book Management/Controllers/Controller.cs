using System;
using Book_Management.Data;
using Book_Management.Exceptions;
using Book_Management.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Book_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Controller : ControllerBase
    {
        private readonly ApplicationDbContext DbContext;

        public Controller(ApplicationDbContext dbContext)
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
            // Validate the incoming model
            if (!ModelState.IsValid)
            {
                var errorResponse = new ErrorResponse
                {
                    StatusCode = 400,
                    Title = "Bad Request",
                    ExceptionMessage = "Please fill out the data correctly."
                };

                return BadRequest(errorResponse);
            }
            else
            // Add the book to the DbContext
            DbContext.Books.Add(book);
            await DbContext.SaveChangesAsync();

            return Ok(book);
        }

        //UPDATE
        [HttpPut("/update")]
        public async Task<ActionResult<Book>> UpadteBook(Book book)
        {
            DbContext.Books.Update(book);
            await DbContext.SaveChangesAsync();

            return Ok(new { message = "Updated Sucessfully" });
        }

        //DELETE
        [HttpDelete("/delete{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await DbContext.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound(new { message = "Book not found" });
            }

            DbContext.Books.Remove(book);
            await DbContext.SaveChangesAsync();

            return Ok(new { message = "Deleted Successfully" });
        }

     }
}
    
