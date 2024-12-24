namespace BookShelf
{
    public static class Utils
    {
        public static int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result))
                    return result;
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }
    }
}