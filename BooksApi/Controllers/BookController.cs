using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.Repositories;

namespace BooksApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private ShelfRepository shelfRepository = new ShelfRepository();
        private BookRepository bookRepository = new BookRepository();

        [HttpGet]
        public ActionResult<List<Book>> GetAll()
        {
            List<Book> books = BookRepository.Data.Values.ToList();
            return Ok(books);
        }
        
        [HttpGet("{id}")]
        public ActionResult<Book> GetById(int id)
        {
            if (!BookRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }
            return Ok(BookRepository.Data[id]);
        }


        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book newBook)
        {
            if (string.IsNullOrWhiteSpace(newBook.Name) || string.IsNullOrWhiteSpace(newBook.Author))
            {
                return BadRequest();
            }

            newBook.Id = SracunajNoviId(BookRepository.Data.Keys.ToList());
            BookRepository.Data[newBook.Id] = newBook;
            bookRepository.Save();

            return Ok(newBook);
        }

        
        [HttpPut("{id}")]
        public ActionResult<Book> Update(int id, [FromBody] Book uBook)
        {
            if (string.IsNullOrWhiteSpace(uBook.Name) || string.IsNullOrWhiteSpace(uBook.Author))
            {
                return BadRequest();
            }
            if (!BookRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            Book book = BookRepository.Data[id];
            book.Name = uBook.Name;
            book.Author = uBook.Author;
            bookRepository.Save();

            return Ok(book);
        }
        
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!BookRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            BookRepository.Data.Remove(id);
            bookRepository.Save();

            return NoContent();
        }

        private int SracunajNoviId(List<int> identifikatori)
        {
            int maxId = 0;
            foreach (int id in identifikatori)
            {
                if (id > maxId)
                {
                    maxId = id;
                }
            }

            return maxId + 1;
        }
    }
}
