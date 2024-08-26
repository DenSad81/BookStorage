using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Storage storage = new Storage();
        Administrator administrator = new Administrator();

        storage.AddBook(new Book("AAA", "aaa", 111, 11));
        storage.AddBook(new Book("BBB", "bbb", 222, 22));
        storage.AddBook(new Book("CCC", "ccc", 333, 33));
        storage.AddBook(new Book("DDD", "ddd", 444, 44));
        storage.AddBook(new Book("BBB", "bbb", 222, 22));
        storage.AddBook(new Book("EEE", "eee", 555, 55));

        administrator.Procces(storage);
    }
}

static class Utils
{
    public static string ReadString(string text = "")
    {
        Console.Write(text + " ");
        string stringFromConsole = Console.ReadLine();
        return stringFromConsole;
    }

    public static int ReadInt(string text = "")
    {
        int digitToOut;
        Console.Write(text + " ");

        while (int.TryParse(Console.ReadLine(), out digitToOut) == false)
            Console.Write(text + " ");

        return digitToOut;
    }
}

class Administrator
{
    public void Procces(Storage storage)
    {
        const string CommandAddBook = "1";
        const string CommandDeleteBook = "2";
        const string CommandSearchBook = "3";
        const string CommandShowAllBooks = "4";
        const string CommandExit = "9";

        bool isRun = true;

        Console.WriteLine($"Menu: {CommandAddBook}-Add book;");
        Console.WriteLine($"      {CommandDeleteBook}-Delete book;");
        Console.WriteLine($"      {CommandSearchBook}-Search book;");
        Console.WriteLine($"      {CommandShowAllBooks}-Show all books;");
        Console.WriteLine($"      {CommandExit}-Exit;");

        while (isRun)
        {
            switch (Utils.ReadString("Your shois: "))
            {
                case CommandAddBook:
                    storage.AddBook(GetCreatedBook());
                    break;

                case CommandDeleteBook:
                    storage.RemoveBook(GetSerchingBook(storage));
                    break;

                case CommandSearchBook:
                    storage.ShowSerchingBook(GetSerchingBook(storage));
                    break;

                case CommandShowAllBooks:
                    storage.ShowAllBooks();
                    break;

                case CommandExit:
                    isRun = false;
                    break;
            }
        }
    }

    private Book GetCreatedBook()
    {
        string name = Utils.ReadString("Input name: ");
        string author = Utils.ReadString("Input author: ");
        int year = Utils.ReadInt("Input year: ");
        int quantytiPage = Utils.ReadInt("Input pages: ");

        Book newBook = new Book(name, author, year, quantytiPage);
        return newBook;
    }   

    private List<Book> GetSerchingBook(Storage storage)
    {
        const string CommandName = "1";
        const string CommandAuthor = "2";
        const string CommandYear = "3";

        Console.WriteLine($"Menu: {CommandName}-Name;");
        Console.WriteLine($"      {CommandAuthor}-Autor;");
        Console.WriteLine($"      {CommandYear}-Year;");

        switch (Utils.ReadString("Your shois: "))
        {
            case CommandName:
                return GetSerchingBookByName(storage);

            case CommandAuthor:
                return GetSerchingBookByAuthor(storage);

            case CommandYear:
                return GetSerchingBookByYear(storage);

            default:
                Console.WriteLine("Incorrect input");
                return null;
        }
    }

    private List<Book> GetSerchingBookByName(Storage storage)
    {
        string key = Utils.ReadString("Input what you search by name: ");
        List<Book> findedBooks = new List<Book>();

        foreach (var book in storage.Books)
        {
            if (book.CheckPresenceByName(key))
                findedBooks.Add(book);
        }

        if (findedBooks.Count == 0)
            Console.WriteLine("Book not find");

        return findedBooks;
    }

    private List<Book> GetSerchingBookByAuthor(Storage storage)
    {
        string key = Utils.ReadString("Input what you search by autor: ");
        List<Book> findedBooks = new List<Book>();

        foreach (var book in storage.Books)
        {
            if (book.CheckPresenceByAuthor(key))
                findedBooks.Add(book);
        }

        if (findedBooks.Count == 0)
            Console.WriteLine("Book not find");

        return findedBooks;
    }

    private List<Book> GetSerchingBookByYear(Storage storage)
    {
        string key = Utils.ReadString("Input what you search by year: ");
        List<Book> findedBooks = new List<Book>();

        foreach (var book in storage.Books)
        {
            if (book.CheckPresenceByYear(key))
                findedBooks.Add(book);
        }

        if (findedBooks.Count == 0)
            Console.WriteLine("Book not find");

        return findedBooks;
    }
}

class Storage
{
    public List<Book> Books { get; private set; }

    public Storage()
    {
        Books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        Books.Add(book);
    }

    public void RemoveBook(List<Book> books)
    {
        foreach (var book in books)
            Books.Remove(book);
    }

    public void ShowSerchingBook(List<Book> serchingBooks)
    {
        if (serchingBooks.Count() == 0)
            return;

        Console.WriteLine("Name Author Year Pages");

        foreach (var book in serchingBooks)
            book.ShowData();
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("Name Author Year Pages");

        foreach (var book in Books)
            book.ShowData();
    }
}

class Book
{
    private string _name;
    private string _author;
    private int _year;
    private int _quantytiPage;

    public Book(string name, string author, int year, int quantytiPage)
    {
        _name = name;
        _author = author;
        _year = year;
        _quantytiPage = quantytiPage;
    }

    public void ShowData()
    {
        Console.WriteLine($"{_name}  {_author}    {_year}  {_quantytiPage}");
    }

    public bool CheckPresenceByName(string key)
    {
        return (_name.ToLower() == key.ToLower());
    }

    public bool CheckPresenceByAuthor(string key)
    {
        return (_author.ToLower() == key.ToLower());
    }

    public bool CheckPresenceByYear(string key)
    {
        return ((Convert.ToString(_year)).ToLower() == key.ToLower());
    }
}