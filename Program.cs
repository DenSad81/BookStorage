using System;
using System.Collections.Generic;
using System.Linq;

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

        administrator.Work(storage);
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
    public void Work(Storage storage)
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
                    storage.AddBook(storage.GetCreatedBook());
                    break;

                case CommandDeleteBook:
                    storage.RemoveBook(storage.GetSearchinBookById());
                    break;

                case CommandSearchBook:
                    storage.ShowSerchingBooks(storage.GetSearchingBooks());
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
}

class Storage
{
    private List<Book> _books;

    public Storage()
    {
        _books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        _books.Add(book);
    }

    public Book GetCreatedBook()
    {
        string name = Utils.ReadString("Input name: ");
        string author = Utils.ReadString("Input author: ");
        int year = Utils.ReadInt("Input year: ");
        int quantytiPage = Utils.ReadInt("Input pages: ");

        Book book = new Book(name, author, year, quantytiPage);
        return book;
    }

    public void RemoveBook(Book book)
    {
        _books.Remove(book);
    }

    public void ShowSerchingBooks(List<Book> serchingBooks)
    {
        if (serchingBooks.Count() == 0)
            return;

        Console.WriteLine("ID  Name Author Year Pages");

        foreach (var book in serchingBooks)
            book.ShowData();
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("ID  Name Author Year Pages");

        foreach (var book in _books)
            book.ShowData();
    }

    public Book GetSearchinBookById()
    {
        int id = Utils.ReadInt("Input ID: ");

        foreach (var book in _books)
        {
            if (book.IdActual == id)
                return book;
        }

        Console.WriteLine("Book not find");
        return null;
    }

    public List<Book> GetSearchingBooks()
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
                return GetSearchingBooksByName();

            case CommandAuthor:
                return GetSearchingBooksByAuthor();

            case CommandYear:
                return GetSearchingBooksByYear();

            default:
                Console.WriteLine("Incorrect input");
                return null;
        }
    }

    private List<Book> GetSearchingBooksByName()
    {
        string key = Utils.ReadString("Input what you search by name: ");
        List<Book> findedBooks = new List<Book>();

        foreach (var book in _books)
        {
            if (book.Name.ToLower() == key.ToLower())
                findedBooks.Add(book);
        }

        if (findedBooks.Count == 0)
            Console.WriteLine("Book not find");

        return findedBooks;
    }

    private List<Book> GetSearchingBooksByAuthor()
    {
        string key = Utils.ReadString("Input what you search by autor: ");
        List<Book> findedBooks = new List<Book>();

        foreach (var book in _books)
        {
            if (book.Author.ToLower() == key.ToLower())
                findedBooks.Add(book);
        }

        if (findedBooks.Count == 0)
            Console.WriteLine("Book not find");

        return findedBooks;
    }

    private List<Book> GetSearchingBooksByYear()
    {
        string key = Utils.ReadString("Input what you search by year: ");
        List<Book> findedBooks = new List<Book>();

        foreach (var book in _books)
        {
            if ((Convert.ToString(book.Year)).ToLower() == key.ToLower())
                findedBooks.Add(book);
        }

        if (findedBooks.Count == 0)
            Console.WriteLine("Book not find");

        return findedBooks;
    }
}

class Book
{
    private static int _id = 0;
    private int _quantytiPage;

    public Book(string name, string author, int year, int quantytiPage)
    {
        IdActual = _id++;
        Name = name;
        Author = author;
        Year = year;
        _quantytiPage = quantytiPage;
    }

    public int IdActual { get; private set; }
    public string Name { get; private set; }
    public string Author { get; private set; }
    public int Year { get; private set; }

    public void ShowData()
    {
        Console.WriteLine($"{IdActual}   {Name}  {Author}    {Year}  {_quantytiPage}");
    }
}