using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers;

[Route("api/shelves")]
[ApiController]
public class ShelfController : ControllerBase
{
    private readonly ShelfRepository shelfRepository = new ShelfRepository();

    [HttpGet]
    public ActionResult<List<Shelf>> GetAll()
    {
        List<Shelf> shelves = shelfRepository.GetAll();
        return Ok(shelves);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Shelf> GetById(int id)
    {
        Shelf? Shelf = shelfRepository.GetById(id);
        if (Shelf == null)
        {
            return NotFound();
        }
        return Ok(Shelf);

    }

    [HttpPost]
    public ActionResult<Shelf> Create([FromBody] Shelf newShelf)
    {
        // Validacija podataka
        if (newShelf.Name == null || newShelf.Name.Trim() == "")
        {
            return BadRequest();
        }
        Shelf savedShelf = shelfRepository.Save(newShelf);
        return Ok(savedShelf);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Shelf> Update(int id, [FromBody] Shelf newShelf)
    {
        // Validacija podataka
        if (newShelf.Name == null || newShelf.Name.Trim() == "")
        {
            return BadRequest();
        }

        Shelf? updatedShelf = shelfRepository.Update(id, newShelf);
        if (updatedShelf == null)
        {
            return NotFound();
        }

        return Ok(updatedShelf);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        bool sucess = shelfRepository.Delete(id);
        if (sucess)
        {
            return NoContent();
        }

        return NotFound();
    }
}

