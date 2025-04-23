using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.Repositories;

namespace BooksApi.Controllers
{
    [Route("api/shelves/{shelfId}/books")]
    [ApiController]
    public class ShelfBooksController : ControllerBase
    {
        private ShelfRepository shelfRepository = new ShelfRepository();
        private BookRepository bookRepository = new BookRepository();
        
        [HttpGet]
        public ActionResult<List<Book>> Get(int shelfId)
        {
            if (!ShelfRepository.Data.ContainsKey(shelfId))
            {
                return NotFound();
            }

            List<Book> allBooks = BookRepository.Data.Values.ToList();
            List<Book> shelfBooks = new List<Book>();
            foreach (Book book in allBooks)
            {
                if (book.Shelf != null && book.Shelf.Id == shelfId)
                {
                    shelfBooks.Add(book);
                }
            }
            
            return Ok(shelfBooks);
        }

        [HttpPut("{bookId}")]
        public ActionResult<Book> Add(int shelfId, int bookId)
        {
            if (!ShelfRepository.Data.ContainsKey(shelfId))
            {
                return NotFound("Shelf not found");
            }

            if (!BookRepository.Data.ContainsKey(bookId))
            {
                return NotFound("Book not found");
            }

            Book book = BookRepository.Data[bookId];
            book.Shelf = ShelfRepository.Data[shelfId];
            bookRepository.Save();
            
            return Ok(book);
        }

        [HttpDelete("{bookId}")]
        public ActionResult Remove(int bookId)
        {
            if (!BookRepository.Data.ContainsKey(bookId))
            {
                return NotFound("Book not found");
            }
            BookRepository.Data[bookId].Shelf = null;
            bookRepository.Save();

            return NoContent();
        }
    }
}
