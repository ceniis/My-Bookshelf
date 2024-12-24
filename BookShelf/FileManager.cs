using System.Text.Json;
using System.Text.Json.Serialization;

namespace BookShelf
{
    /// <summary>
    /// Class for import/export files
    /// </summary>
    public static class FileManager
    {
        private static readonly string basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "BookShelf"); // adds a file on desktop
        private static readonly string booksFile = Path.Combine(basePath, "books.json");
        private static readonly string collectionsFile = Path.Combine(basePath, "collections.json");

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        /// <summary>
        /// Save books to the file
        /// </summary>
        /// <param name="books">Books to save</param>
        public static void SaveBooks(IEnumerable<Book> books)
        {
            EnsureDirectoryExists();
            var json = JsonSerializer.Serialize(books, options);
            File.WriteAllText(booksFile, json);
        }

        /// <summary>
        /// Load books from the file
        /// </summary>
        /// <returns>List with books or creates new one</returns>
        public static List<Book> LoadBooks()
        {
            if (File.Exists(booksFile))
            {
                var json = File.ReadAllText(booksFile);
                return JsonSerializer.Deserialize<List<Book>>(json, options) ?? new List<Book>();
            }
            return new List<Book>();
        }

        /// <summary>
        /// Save collection to the file with collections 
        /// </summary>
        /// <param name="collections">Collections to save</param>
        public static void SaveCollections(IEnumerable<Collection> collections)
        {
            EnsureDirectoryExists();
            File.WriteAllText(collectionsFile, JsonSerializer.Serialize(collections));
        }

        /// <summary>
        /// Load collections from the file or create new one
        /// </summary>
        /// <returns></returns>
        public static List<Collection> LoadCollections()
        {
            if (File.Exists(collectionsFile))
            {
                return JsonSerializer.Deserialize<List<Collection>>(File.ReadAllText(collectionsFile));
            }
            return new List<Collection>();
        }

        /// <summary>
        /// Check if the directory exist or create new one
        /// </summary>
        private static void EnsureDirectoryExists()
        {
            if (!Directory.Exists(basePath))
            {
                Directory.CreateDirectory(basePath);
            }
        }

        /// <summary>
        /// Imports books from the file
        /// </summary>
        /// <param name="filePath">Path to the file with the information</param>
        /// <returns>True if successful</returns>
        public static bool ImportBooks(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var importedBooks = JsonSerializer.Deserialize<List<Book>>(json);
                    if (importedBooks != null)
                    {
                        var currentBooks = LoadBooks();
                        var maxId = currentBooks.Count > 0 ? currentBooks.Max(b => b.Id) : 0;

                        foreach (var book in importedBooks)
                        {
                            book.Id = ++maxId; // Creates unique ID for each book
                            currentBooks.Add(book);
                        }

                        SaveBooks(currentBooks);

                        // Update the list of books in the manager
                        BookManager.ReloadBooks(currentBooks);

                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing books: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Import book's collections
        /// </summary>
        /// <param name="filePath">Path to the file with the information</param>
        /// <returns>True if successful</returns>
        public static bool ImportCollections(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var importedCollections = JsonSerializer.Deserialize<List<Collection>>(json);
                    if (importedCollections != null)
                    {
                        var currentCollections = LoadCollections();

                        foreach (var collection in importedCollections)
                        {
                            if (!currentCollections.Any(c => c.Name.Equals(collection.Name, StringComparison.OrdinalIgnoreCase)))
                            {
                                currentCollections.Add(collection);
                            }
                        }

                        SaveCollections(currentCollections);

                        // Update the list of books in the manager
                        CollectionManager.ReloadCollections(currentCollections);

                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error importing collections: {ex.Message}");
                return false;
            }
        }

    }
}
