namespace BookShelf
{
    /// <summary>
    /// Class for user's interface 
    /// </summary>
    public static class Menu
    {
        /// <summary>
        /// Main menu run
        /// </summary>
        public static void Run()
        {
            string choice;
            do
            {
                // main user's menu
                Console.Clear();
                Console.WriteLine("=== BookShelf Menu ===");
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Edit Book");
                Console.WriteLine("3. View Books");
                Console.WriteLine("4. Add Collection");
                Console.WriteLine("5. Add Book to Collection");
                Console.WriteLine("6. View Collections");
                Console.WriteLine("7. View Books in Collection");
                Console.WriteLine("8. Search Books");
                Console.WriteLine("9. Export Data");
                Console.WriteLine("10. Import Data");
                Console.WriteLine("11. Exit");
                Console.Write("Enter choice: ");
                choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBookMenu();
                        break;
                    case "2":
                        EditBookMenu();
                        break;
                    case "3":
                        BookManager.DisplayBooks();
                        Console.ReadLine();
                        break;
                    case "4":
                        AddCollectionMenu();
                        break;
                    case "5":
                        AddBookToCollectionMenu();
                        break;
                    case "6":
                        CollectionManager.DisplayCollections();
                        Console.ReadLine();
                        break;
                    case "7":
                        ViewBooksInCollectionMenu();
                        break;
                    case "8":
                        SearchBooksMenu();
                        break;
                    case "9":
                        ExportDataMenu();
                        break;
                    case "10":
                        ImportDataMenu();
                        break;
                }
            } while (choice != "11");
        }

        /// <summary>
        /// Menu for adding new book
        /// </summary>
        private static void AddBookMenu()
        {
            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            Console.Write("Enter author: ");
            string author = Console.ReadLine();
            Console.Write("Enter publisher: ");
            string publisher = Console.ReadLine();
            Console.Write("Enter year: ");
            int year = int.Parse(Console.ReadLine());
            Console.Write("Enter review: ");
            string review = Console.ReadLine();

            // add
            BookManager.AddBook(new Book
            {
                Title = title,
                Author = author,
                Publisher = publisher,
                Year = year,
                Review = review
            });
        }

        /// <summary>
        /// Menu for editing a book
        /// </summary>
        private static void EditBookMenu()
        {
            Console.Write("Enter book ID to edit: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid ID. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            var book = BookManager.GetBookById(bookId); // search the book
            if (book == null)
            {
                Console.WriteLine("Book not found. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            // edit
            Console.WriteLine("Editing Book:");
            Console.WriteLine(book);

            Console.Write("Enter new title (or leave blank to keep current): ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title)) title = book.Title;

            Console.Write("Enter new author (or leave blank to keep current): ");
            string author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author)) author = book.Author;

            Console.Write("Enter new publisher (or leave blank to keep current): ");
            string publisher = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(publisher)) publisher = book.Publisher;

            Console.Write("Enter new year (or leave blank to keep current): ");
            string yearInput = Console.ReadLine();
            int year = string.IsNullOrWhiteSpace(yearInput) ? book.Year : int.Parse(yearInput);

            Console.Write("Enter new review (or leave blank to keep current): ");
            string review = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(review)) review = book.Review;

            BookManager.EditBook(bookId, title, author, publisher, year, review);
        }

        /// <summary>
        /// Menu for adding new collection
        /// </summary>
        private static void AddCollectionMenu()
        {
            Console.Write("Enter collection name: ");
            string name = Console.ReadLine();
            CollectionManager.AddCollection(name);
        }

        /// <summary>
        /// Menu for adding book to collection
        /// </summary>
        private static void AddBookToCollectionMenu()
        {
            Console.Write("Enter collection ID: ");
            if (!int.TryParse(Console.ReadLine(), out int collectionId)) 
            {
                Console.WriteLine("Invalid ID. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            Console.Write("Enter book ID to add: ");
            if (!int.TryParse(Console.ReadLine(), out int bookId))
            {
                Console.WriteLine("Invalid ID. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            var collection = CollectionManager.GetCollectionById(collectionId);
            if (collection == null)
            {
                Console.WriteLine("Collection not found.");
                Console.ReadKey();
                return;
            }

            // add if both id are correct and the collection exists
            CollectionManager.AddBookToCollection(collection.Name, bookId);
        }

        /// <summary>
        /// Menu to view existing book collections
        /// </summary>
        private static void ViewBooksInCollectionMenu()
        {
            Console.Write("Enter collection ID to view: ");
            if (!int.TryParse(Console.ReadLine(), out int collectionId))
            {
                Console.WriteLine("Invalid ID. Press any key to return to the menu.");
                Console.ReadKey();
                return;
            }

            CollectionManager.DisplayBooksInCollection(collectionId);
            Console.ReadLine();
        }

        /// <summary>
        /// Prints search menu and results
        /// </summary>
        private static void SearchBooksMenu()
        {
            Console.Write("Enter search query: ");
            string query = Console.ReadLine();
            var results = BookManager.SearchBooks(query);

            Console.WriteLine("Search Results:");
            foreach (var book in results)
            {
                Console.WriteLine(book);
            }
            Console.ReadLine();
        }

        /// <summary>
        /// Exports data from jsom
        /// </summary>
        private static void ExportDataMenu()
        {
            FileManager.SaveBooks(BookManager.GetAllBooks());
            FileManager.SaveCollections(CollectionManager.GetAllCollections());
            Console.WriteLine("Data exported successfully. Press any key to return to the menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// Imports data to json
        /// </summary>
        private static void ImportDataMenu()
        {
            var books = FileManager.LoadBooks();
            var collections = FileManager.LoadCollections();

            BookManager.ReloadBooks(books);
            CollectionManager.ReloadCollections(collections);

            Console.WriteLine("Data imported successfully. Press any key to return to the menu.");
            Console.ReadKey();
        }
    }

}
