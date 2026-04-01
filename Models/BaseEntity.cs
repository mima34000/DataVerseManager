namespace DataVerseManager.Models
{
    public abstract class BaseEntity
    {
        // every item has a unique id
        public int Id { get; set; }

        // every item has a date it was created
        public DateTime Date { get; set; }
    }
}