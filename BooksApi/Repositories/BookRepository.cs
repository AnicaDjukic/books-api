using BooksApi.Models;

namespace BooksApi.Repositories
{
    public class BookRepository
    {
        private const string filePath = "data/books.csv"; // Putanja je prostija u veb aplikaciji
        public static Dictionary<int, Book> Data;

        public BookRepository()
        {
            if (Data == null)
            {
                Load();
            }
        }

        private void Load()
        {
            Data = new Dictionary<int, Book>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] attributes = line.Split('|');
                int id = int.Parse(attributes[0]);
                string name = attributes[1];
                string author = attributes[2];
                Book book = new Book(id, name, author);
                Data[id] = book;

                if (attributes[3] == "")
                {
                    continue;
                }
                // Uveži knjige i police
                int shelfId = int.Parse(attributes[3]);
                book.Shelf = ShelfRepository.Data[shelfId];
            }
        }

        public void Save()
        {
            List<string> lines = new List<string>();
            foreach (Book r in Data.Values)
            {
                string shelfId = r.Shelf == null ? "" : r.Shelf.Id.ToString();
                lines.Add($"{r.Id}|{r.Name}|{r.Author}|{shelfId}");
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
