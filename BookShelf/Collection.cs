namespace BookShelf
{
    public class Collection
    {
        public int Id { get; set; } // Унікальний ідентифікатор колекції
        public string Name { get; set; } // Назва колекції
        public List<int> BookIds { get; set; } = new List<int>(); // Ідентифікатори книг у колекції

        public override string ToString()
        {
            return $"[{Id}] {Name} ({BookIds.Count} books)";
        }
    }

}