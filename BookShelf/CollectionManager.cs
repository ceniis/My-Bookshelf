using BookShelf;

/// <summary>
/// Class to work with book collection
/// </summary>
public static class CollectionManager
{
    private static List<Collection> collections = FileManager.LoadCollections(); // load information from file

    /// <summary>
    /// Add new book collection
    /// </summary>
    /// <param name="name">Name of the collection</param>
    public static void AddCollection(string name)
    {
        // check if the name is unique
        if (collections.Any(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Collection with this name already exists."); 
            return;
        }
        collections.Add(new Collection { Name = name }); // add
        FileManager.SaveCollections(collections); // save
        Console.WriteLine("Collection added successfully.");
    }

    /// <summary>
    /// Add book to the collection if it exists
    /// </summary>
    /// <param name="collectionName">Collection where book will be added</param>
    /// <param name="bookId">Book's ID to add</param>
    public static void AddBookToCollection(string collectionName, int bookId)
    {
        var collection = collections.FirstOrDefault(c => c.Name.Equals(collectionName, StringComparison.OrdinalIgnoreCase));
        if (collection == null)
        {
            Console.WriteLine("Collection not found.");
            return;
        }
        if (!collection.BookIds.Contains(bookId))
        {
            collection.BookIds.Add(bookId);
            FileManager.SaveCollections(collections);
            Console.WriteLine("Book added to collection.");
        }
        else
        {
            Console.WriteLine("Book already in collection.");
        }
    }

    /// <summary>
    /// View collection list if they exist
    /// </summary>
    public static void DisplayCollections()
    {
        if (collections.Count == 0)
        {
            Console.WriteLine("No collections available.");
            return;
        }
        foreach (var collection in collections)
        {
            Console.WriteLine(collection);
        }
    }

    /// <summary>
    /// Search collection by name
    /// </summary>
    /// <param name="name">Param to search by</param>
    /// <returns>The collection or null</returns>
    public static Collection GetCollectionByName(string name)
    {
        return collections.FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public static Collection GetCollectionById(int id)
    {
        if (id <= 0 || id > collections.Count)
        {
            return null;
        }
        return collections[id - 1];
    }

    public static IEnumerable<Collection> GetAllCollections()
    {
        return collections; // returns all collections
    }

    /// <summary>
    /// Save new collection to the list and the file
    /// </summary>
    /// <param name="newCollections">Collection to save</param>
    public static void ReloadCollections(IEnumerable<Collection> newCollections)
    {
        collections = newCollections.ToList();
        FileManager.SaveCollections(collections);
    }

    /// <summary>
    /// Show books in the collection
    /// </summary>
    /// <param name="collectionId">Collection to work with</param>
    public static void DisplayBooksInCollection(int collectionId)
    {
        var collection = GetCollectionById(collectionId);
        if (collection == null)
        {
            Console.WriteLine("Collection not found.");
            return;
        }

        Console.WriteLine($"Books in collection '{collection.Name}':");
        if (collection.BookIds.Count == 0)
        {
            Console.WriteLine("No books in this collection.");
            return;
        }

        foreach (var bookId in collection.BookIds)
        {
            var book = BookManager.GetBookById(bookId);
            if (book != null)
            {
                Console.WriteLine(book); // prints if collection exist and if there're some books
            }
        }
    }
}
