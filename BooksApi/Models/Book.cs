namespace WebApplication1.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public Shelf? Shelf { get; set; }

        public Book(int id, string name, string author, Shelf shelf)
        {
            Id = id;
            Name = name;
            Author = author;
            Shelf = shelf;
        }
    }
}
