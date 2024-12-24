namespace BookShelf
{
    class Program
    {
        static void Main(string[] args)
        {
            //title
            Console.Title = "My bookshelf";
            //text and background colors
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;

            // start program
            Menu.Run(); 
        }
    }
}