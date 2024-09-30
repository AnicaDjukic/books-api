using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class BookRepository
    {

        private const string filePath = "Resources/books.csv";

        private const string separator = "|";

        public List<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            foreach (string line in File.ReadLines(filePath))
            {
                string[] csvValues = line.Split(separator);
                int id = int.Parse(csvValues[0]);
                string name = csvValues[1];
                string author = csvValues[2];
                Shelf shelf = null;
                if (csvValues[3] != "")
                {
                    shelf = new Shelf(int.Parse(csvValues[3]), "");
                }
                Book book = new Book(id, name, author, shelf);
                books.Add(book);
            }
            return books;
        }

        public Book? GetById(int id)
        {
            List<Book> books = GetAll();
            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    return book;
                }
            }
            return null;
        }

        public Book Save(Book newBook)
        {
            List<Book> books = GetAll();
            newBook.Id = books.Any() ? books.Max(x => x.Id) + 1 : 1;
            books.Add(newBook);
            SaveAll(books);
            return newBook;
        }

        public Book Update(Book newBook)
        {
            List<Book> books = GetAll();
            foreach (Book book in books)
            {
                if (book.Id == newBook.Id)
                {
                    book.Name = newBook.Name;
                    book.Author = newBook.Author;
                    book.Shelf = newBook.Shelf;
                    // Nakon ažuriranja knjige u books listi sad upiši sve knjige ponovo
                    SaveAll(books);
                    break;
                }
            }
            return newBook;
        }

        public bool Delete(int id)
        {
            List<Book> books = GetAll();
            foreach (Book book in books)
            {
                if (book.Id == id)
                {
                    books.Remove(book);
                    // Nakon uklanjanja knjige iz books liste sad upiši sve knjige ponovo
                    SaveAll(books);
                    return true;
                }
            }

            return false;
        }

        public List<Book> GetByShelf(int shelfid)
        {
            List<Book> books = new List<Book>();
            foreach (Book book in GetAll())
            {
                if (book.Shelf?.Id == shelfid)
                {
                    books.Add(book);
                }
            }
            return books;
        }

        private void SaveAll(List<Book> books)
        {
            StringBuilder output = new StringBuilder();
            foreach (Book b in books)
            {
                string newLine = b.Id.ToString() + separator + b.Name + separator + b.Author + separator + b.Shelf?.Id;
                output.AppendLine(string.Join(separator, newLine));
            }
            File.WriteAllText(filePath, output.ToString());
        } 
    }
}
