using System.Text.Json;
using DataVerseManager.Models;

namespace DataVerseManager.Services
{
    public class JsonService
    {
        // path to the file where expenses are saved
        private string _filePath = "expenses.json";

        // saves all expenses to a json file
        public void Save(List<Expense> expenses)
        {
            try
            {
                string json = JsonSerializer.Serialize(expenses);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving data: " + ex.Message);
            }
        }

        // loads expenses from the json file
        public List<Expense> Load()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<Expense>();

                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading data: " + ex.Message);
                return new List<Expense>();
            }
        }
    }
}