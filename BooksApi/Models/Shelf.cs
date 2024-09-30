using System.Text.Json.Serialization;

namespace WebApplication1.Models;

public class Shelf
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Shelf(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
