using BooksApi.Models;

namespace BooksApi.Repositories
{
    public class ShelfRepository
    {
        private const string filePath = "data/shelves.csv"; // Putanja je prostija u veb aplikaciji
        public static Dictionary<int, Shelf> Data;

        public ShelfRepository()
        {
            if (Data == null)
            {
                Load();
            }
        }

        private void Load()
        {
            Data = new Dictionary<int, Shelf>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] attributes = line.Split('|');
                int id = int.Parse(attributes[0]);
                string name = attributes[1];
                Shelf shelf = new Shelf(id, name);
                Data[id] = shelf;
            }
        }

        public void Save()
        {
            List<string> lines = new List<string>();
            foreach (Shelf s in Data.Values)
            {
                lines.Add($"{s.Id}|{s.Name}");
            }
            File.WriteAllLines(filePath, lines);
        }
    }
}
