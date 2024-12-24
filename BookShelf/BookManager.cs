using BookShelf;

/// <summary>
/// Class to work with books
/// </summary>
public static class BookManager
{
    private static List<Book> books = FileManager.LoadBooks(); // load information from the file

    /// <summary>
    /// Add new book
    /// </summary>
    /// <param name="book">Book to add</param>
    public static void AddBook(Book book)
    {
        book.Id = books.Count > 0 ? books.Max(b => b.Id) + 1 : 1; // create ID
        books.Add(book);
        FileManager.SaveBooks(books); // save
        Console.WriteLine("Book added successfully.");
    }

    /// <summary>
    /// Edit the book if it exists
    /// </summary>
    /// <param name="id">Book's unique ID</param>
    /// <param name="title">Book's name</param>
    /// <param name="author">Book's author</param>
    /// <param name="publisher">Publisher</param>
    /// <param name="year">Year when it has been published</param>
    /// <param name="review">Short description or a review</param>
    public static void EditBook(int id, string title, string author, string publisher, int year, string review)
    {
        var book = GetBookById(id);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }
        book.Title = title;
        book.Author = author;
        book.Publisher = publisher;
        book.Year = year;
        book.Review = review;
        FileManager.SaveBooks(books);
        Console.WriteLine("Book updated successfully.");
    }

    /// <summary>
    /// Search for books 
    /// </summary>
    /// <param name="query">The search string to match against book titles and authors.</param>
    /// <returns>An IEnumerable of books that match the search criteria.</returns>
    public static IEnumerable<Book> SearchBooks(string query)
    {
        return books.Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                                b.Author.Contains(query, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Sort the collection of books 
    /// </summary>
    /// <param name="keySelector">A function that extracts a key from a book for sorting.</param>
    /// <returns>An IEnumerable of books sorted by the specified key.</returns>
    public static IEnumerable<Book> SortBooks(Func<Book, object> keySelector)
    {
        return books.OrderBy(keySelector);
    }

    /// <summary>
    /// Prints books
    /// </summary>
    public static void DisplayBooks()
    {
        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }
        foreach (var book in books)
        {
            Console.WriteLine(book);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id">Book's ID to search for</param>
    /// <returns>All books</returns>
    public static Book GetBookById(int id)
    {
        return books.FirstOrDefault(b => b.Id == id);
    }

    public static IEnumerable<Book> GetAllBooks()
    {
        return books; // return all books
    }

    /// <summary>
    /// Save new books to the list and to the file
    /// </summary>
    /// <param name="newBooks">Books to save</param>
    public static void ReloadBooks(IEnumerable<Book> newBooks)
    {
        books = newBooks.ToList();
        FileManager.SaveBooks(books);
    }
}