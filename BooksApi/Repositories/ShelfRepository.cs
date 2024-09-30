using System.Text;
using WebApplication1.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication1.Repositories
{
    public class ShelfRepository
    {

        private const string filePath = "Resources/shelves.csv";

        private const string separator = "|";

        public List<Shelf> GetAll()
        {
            List<Shelf> shelves = new List<Shelf>();
            foreach (string line in File.ReadLines(filePath))
            {
                string[] csvValues = line.Split(separator);
                int id = int.Parse(csvValues[0]);
                string name = csvValues[1];
                Shelf shelf = new(id, name);
                shelves.Add(shelf);
            }
            return shelves;
        }

        public Shelf? GetById(int id)
        {
            List<Shelf> shelves = GetAll();
            foreach (Shelf shelf in shelves)
            {
                if (shelf.Id == id)
                {
                    return shelf;
                }
            }
            return null;
        }

        public Shelf Save(Shelf newShelf)
        {
            List<Shelf> shelves = GetAll();
            newShelf.Id = shelves.Any() ? shelves.Max(x => x.Id) + 1 : 1;
            shelves.Add(newShelf);
            SaveAll(shelves);
            return newShelf;
        }

        public Shelf? Update(int id, Shelf newShelf)
        {
            List<Shelf> shelves = GetAll();
            foreach (Shelf Shelf in shelves)
            {
                if (Shelf.Id == id)
                {
                    Shelf.Name = newShelf.Name;
                    // Nakon ažuriranja police u shelves listi sad upiši sve police ponovo
                    SaveAll(shelves);
                    return Shelf;
                }
            }
            return null;
        }

        public bool Delete(int id)
        {
            List<Shelf> shelves = GetAll();
            foreach (Shelf Shelf in shelves)
            {
                if (Shelf.Id == id)
                {
                    shelves.Remove(Shelf);
                    // Nakon uklanjanja police iz shelves liste sad upiši sve police ponovo
                    SaveAll(shelves);
                    return true;
                }
            }

            return false;
        }

        private void SaveAll(List<Shelf> shelves)
        {
            StringBuilder output = new StringBuilder();
            foreach (Shelf b in shelves)
            {
                string newLine = b.Id.ToString() + separator + b.Name;
                output.AppendLine(string.Join(separator, newLine));
            }
            File.WriteAllText(filePath, output.ToString());
        }
    }
}
