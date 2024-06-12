using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.Data;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _context.Books.ToList();
            return Ok(books);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddBook([FromBody] Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAllBooks), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateBook(int id, [FromBody] Book book)
        {
            var existingBook = _context.Books.Find(id);
            if (existingBook == null)
                return NotFound();

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.ISBN = book.ISBN;

            _context.SaveChanges();
            return Ok(existingBook);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook(int id)
        {
            var book = _context.Books.Find(id);
            if (book == null)
                return NotFound();

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
