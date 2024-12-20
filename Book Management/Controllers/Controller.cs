<<<<<<< HEAD
﻿using System;
using Book_Management.Data;
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

            DbContext.Books.Add(book);
            await DbContext.SaveChangesAsync();

            return Ok();
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
    
=======
﻿using Book_Management.Data;
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
>>>>>>> origin/main
