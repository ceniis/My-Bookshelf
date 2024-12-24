namespace BookShelf
{
    /// <summary>
    /// Class to represent the book
    /// </summary>
    public class Book
    {
        public int Id { get; set; } // unique book's ID
        public string Title { get; set; } // Book's name
        public string Author { get; set; } // author of the book
        public string Publisher { get; set; } // publisher
        public int Year { get; set; } // year of publishing
        public string Review { get; set; } // short review or description

        public override string ToString() // return the string with information about the book
        {
            return $"[{Id}] {Title} by {Author}, {Year} ({Publisher})\nReview: {Review}";
        }
    }
}