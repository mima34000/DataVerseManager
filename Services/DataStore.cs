namespace DataVerseManager.Services
{
    public class DataStore<T>
    {
        // list that holds all items
        private List<T> _items = new List<T>();

        // adds a new item to the list
        public void Add(T item)
        {
            _items.Add(item);
        }

        // returns all items
        public List<T> GetAll()
        {
            return _items;
        }

        // removes an item from the list
        public void Remove(T item)
        {
            _items.Remove(item);
        }

        // replaces the whole list with a new one
        public void SetAll(List<T> items)
        {
            _items = items;
        }
    }
}