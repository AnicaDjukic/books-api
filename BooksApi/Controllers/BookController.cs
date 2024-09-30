using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly BookRepository bookRepository = new BookRepository();

        [HttpGet("/api/books")]
        public ActionResult<List<Book>> GetAll()
        {
            List<Book> books = bookRepository.GetAll();
            return Ok(books);
        }

        [HttpGet("/api/books/{id:int}")]
        public ActionResult<Book> GetById(int id)
        {
            Book? book = bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);

        }

        [HttpPost("/api/books")]
        public ActionResult<Book> Create([FromBody] Book newBook)
        {
            // Validacija podataka
            if (newBook.Name == null || newBook.Name.Trim() == "" || newBook.Author == null || newBook.Author.Trim() == "")
            {
                return BadRequest();
            }
            Book savedBook = bookRepository.Save(newBook);
            return Ok(savedBook);
        }

        [HttpPut("/api/books/{id:int}")]
        public ActionResult<Book> Update(int id, [FromBody] Book newBook)
        {
            // Validacija podataka
            if (newBook.Name == null || newBook.Name.Trim() == "" || newBook.Author == null || newBook.Author.Trim() == "")
            {
                return BadRequest();
            }

            Book? book = bookRepository.GetById(id);
            if (book == null)
            {
                return NotFound();
            }

            // ažuriramo samo naziv i autora, za vezu sa policom će biti zadužen drugi kontroler
            book.Name = newBook.Name;
            book.Author = newBook.Author;

            Book updatedBook = bookRepository.Update(book);

            return Ok(updatedBook);
        }

        [HttpDelete("/api/books/{id:int}")]
        public ActionResult Delete(int id)
        {
            bool sucess = bookRepository.Delete(id);
            if (sucess)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
