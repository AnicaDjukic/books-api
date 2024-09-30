using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    [Route("/api/shelves/{shelfId:int}/books")]
    [ApiController]
    public class ShelfBooksController : ControllerBase
    {
        private readonly ShelfRepository shelfRepository = new ShelfRepository();
        private readonly BookRepository bookRepository = new BookRepository();

        // za dobavljanje knjiga koje se nalaze na određenoj polici
        [HttpGet]
        public ActionResult<List<Book>> GetShelfBooks(int shelfId)
        {
            Shelf? shelf = shelfRepository.GetById(shelfId);  // ? jer možda polica ne postoji (možemo dobiti null kao povratnu vrednost)
            if (shelf == null)
            {
                // ako polica ne postoji vraćamo statusni kod 404 NotFound
                return NotFound();
            }
            List<Book> shelfBooks = bookRepository.GetByShelf(shelfId);
            // vraćamo pronađene knjige uz statusni kod 200 OK
            return Ok(shelfBooks);
        }
    }

}
