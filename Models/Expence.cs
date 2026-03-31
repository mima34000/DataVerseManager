namespace DataVerseManager.Models
{
    public class Expense
    {
        // unique id for each expense
        public int Id { get; set; }

        // name of the expense, e.g. "Groceries"
        public string Name { get; set; } = "";

        // how much it cost
        public decimal Amount { get; set; }

        // category like "Food", "Transport" etc.
        public string Category { get; set; } = "";

        // date when the expense happened
        public DateTime Date { get; set; }
    }
}