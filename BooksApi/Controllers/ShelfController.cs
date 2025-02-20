using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;
using BooksApi.Repositories;

namespace BooksApi.Controllers
{

    [Route("api/shelves")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private ShelfRepository shelfRepository = new ShelfRepository();

        [HttpGet]
        public ActionResult<List<Shelf>> GetAll()
        {
            List<Shelf> shelves = ShelfRepository.Data.Values.ToList();
            return Ok(shelves);
        }

        [HttpGet("{id}")]
        public ActionResult<Shelf> GetById(int id)
        {
            if (!ShelfRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            return Ok(ShelfRepository.Data[id]);
        }


        [HttpPost]
        public ActionResult<Shelf> Create([FromBody] Shelf newShelf)
        {
            if (string.IsNullOrWhiteSpace(newShelf.Name))
            {
                return BadRequest();
            }

            newShelf.Id = SracunajNoviId(ShelfRepository.Data.Keys.ToList());
            ShelfRepository.Data[newShelf.Id] = newShelf;
            shelfRepository.Save();

            return Ok(newShelf);
        }


        [HttpPut("{id}")]
        public ActionResult<Shelf> Update(int id, [FromBody] Shelf uShelf)
        {
            if (string.IsNullOrWhiteSpace(uShelf.Name))
            {
                return BadRequest();
            }

            if (!ShelfRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            Shelf shelf = ShelfRepository.Data[id];
            shelf.Name = uShelf.Name;
            shelfRepository.Save();

            return Ok(shelf);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!ShelfRepository.Data.ContainsKey(id))
            {
                return NotFound();
            }

            ShelfRepository.Data.Remove(id);
            shelfRepository.Save();

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