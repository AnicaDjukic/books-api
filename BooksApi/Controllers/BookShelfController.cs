using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("/api/books/{bookId:int}/shelf")]
    [ApiController]
    public class BookShelfController : ControllerBase
    {
        private readonly ShelfRepository shelfRepository = new ShelfRepository();
        private readonly BookRepository bookRepository = new BookRepository();

        // za dodavanje knjige na policu
        [HttpPut("{shelfId:int}")]
        public ActionResult<List<Book>> AddBookToShelf(int bookId, int shelfId)
        {
            Book? book = bookRepository.GetById(bookId);  // ? jer možda knjiga ne postoji (možemo dobiti null kao povratnu vrednost)
            if (book == null)
            {
                // ako knjiga ne postoji vraćamo statusni kod 404 NotFound
                return NotFound();
            }
            Shelf? shelf = shelfRepository.GetById(shelfId);  // ? jer možda polica ne postoji (možemo dobiti null kao povratnu vrednost)
            if (shelf == null)
            {
                // ako polica ne postoji vraćamo statusni kod 404 NotFound
                return NotFound();
            }
            book.Shelf = shelf;   // nađenoj knjizi za policu podešavamo traženu policu
            Book updatedBook = bookRepository.Update(book);
            // vraćamo ažuriranu knjigu uz statusni kod 200 OK
            return Ok(updatedBook);
        }

        // za uklanjanje knjige sa police
        [HttpDelete]
        public ActionResult<List<Book>> RemoveBookFromShelf(int bookId)
        {
            Book? book = bookRepository.GetById(bookId);  // ? jer možda knjiga ne postoji (možemo dobiti null kao povratnu vrednost)
            if (book == null)
            {
                // ako knjiga ne postoji vraćamo statusni kod 404 NotFound
                return NotFound();
            }
            book.Shelf = null;   // stavljamo null za vrednost police na kojoj se knjiga nalazi
            Book updatedBook = bookRepository.Update(book);
            // vraćamo ažuriranu knjigu uz statusni kod 200 OK
            return Ok(updatedBook);
        }

    }

}
