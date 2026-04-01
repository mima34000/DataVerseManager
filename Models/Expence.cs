namespace DataVerseManager.Models
{
    // Expense inherits Id and Date from BaseEntity
    public class Expense : BaseEntity
    {
        // name of the expense, e.g. "Groceries"
        public string Name { get; set; } = "";

        // how much it cost
        public decimal Amount { get; set; }

        // category like "Food", "Transport" etc.
        public string Category { get; set; } = "";
    }
}