using Spectre.Console;
using DataVerseManager.Models;
using DataVerseManager.Services;

namespace DataVerseManager.UI
{
    public class MenuDisplay
    {
        private DataStore<Expense> _store;
        private JsonService _jsonService;

        public MenuDisplay(DataStore<Expense> store, JsonService jsonService)
        {
            _store = store;
            _jsonService = jsonService;
        }

        // shows the main menu and handles user choices
        public void ShowMainMenu()
        {
            while (true)
            {
                AnsiConsole.Clear();

                AnsiConsole.Write(new FigletText("Budget Manager")
                    .Color(Color.Green));

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[yellow]What do you want to do?[/]")
                        .AddChoices(new[]
                        {
                            "Add Expense",
                            "View All Expenses",
                            "Update Expense",
                            "Delete Expense",
                            "Search Expenses",
                            "Save and Exit"
                        }));

                switch (choice)
                {
                    case "Add Expense":
                        AddExpense();
                        break;
                    case "View All Expenses":
                        ViewExpenses();
                        break;
                    case "Update Expense":
                        UpdateExpense();
                        break;
                    case "Delete Expense":
                        DeleteExpense();
                        break;
                    case "Search Expenses":
                        SearchExpenses();
                        break;
                    case "Save and Exit":
                        _jsonService.Save(_store.GetAll());
                        AnsiConsole.MarkupLine("[green]Data saved. Goodbye![/]");
                        return;
                }
            }
        }

        // adds a new expense
        private void AddExpense()
        {
            var name = AnsiConsole.Ask<string>("Expense name:");
            var amount = AnsiConsole.Ask<decimal>("Amount:");
            var category = AnsiConsole.Ask<string>("Category:");

            var expense = new Expense
            {
                Id = _store.GetAll().Count + 1,
                Name = name,
                Amount = amount,
                Category = category,
                Date = DateTime.Now
            };

            _store.Add(expense);
            AnsiConsole.MarkupLine("[green]Expense added![/]");
            Console.ReadKey();
        }

        // shows all expenses in a table
        private void ViewExpenses()
        {
            var expenses = _store.GetAll();

            if (expenses.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No expenses found.[/]");
                Console.ReadKey();
                return;
            }

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Amount");
            table.AddColumn("Category");
            table.AddColumn("Date");

            foreach (var e in expenses)
            {
                table.AddRow(
                    e.Id.ToString(),
                    e.Name,
                    e.Amount.ToString("C"),
                    e.Category,
                    e.Date.ToShortDateString()
                );
            }

            AnsiConsole.Write(table);
            Console.ReadKey();
        }

        // updates an existing expense by id
        private void UpdateExpense()
        {
            var expenses = _store.GetAll();

            if (expenses.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No expenses to update.[/]");
                Console.ReadKey();
                return;
            }

            var id = AnsiConsole.Ask<int>("Enter expense ID to update:");
            var expense = expenses.FirstOrDefault(e => e.Id == id);

            if (expense == null)
            {
                AnsiConsole.MarkupLine("[red]Expense not found.[/]");
                Console.ReadKey();
                return;
            }

            expense.Name = AnsiConsole.Ask<string>("New name:", expense.Name);
            expense.Amount = AnsiConsole.Ask<decimal>("New amount:", expense.Amount);
            expense.Category = AnsiConsole.Ask<string>("New category:", expense.Category);

            AnsiConsole.MarkupLine("[green]Expense updated![/]");
            Console.ReadKey();
        }

        // deletes an expense by id
        private void DeleteExpense()
        {
            var expenses = _store.GetAll();

            if (expenses.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No expenses to delete.[/]");
                Console.ReadKey();
                return;
            }

            var id = AnsiConsole.Ask<int>("Enter expense ID to delete:");
            var expense = expenses.FirstOrDefault(e => e.Id == id);

            if (expense == null)
            {
                AnsiConsole.MarkupLine("[red]Expense not found.[/]");
            }
            else
            {
                _store.Remove(expense);
                AnsiConsole.MarkupLine("[green]Expense deleted![/]");
            }

            Console.ReadKey();
        }

        // searches expenses by name or category
        private void SearchExpenses()
        {
            var keyword = AnsiConsole.Ask<string>("Search by name or category:");

            var results = _store.GetAll()
                .Where(e => e.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                            e.Category.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (results.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]No results found.[/]");
                Console.ReadKey();
                return;
            }

            var table = new Table();
            table.AddColumn("ID");
            table.AddColumn("Name");
            table.AddColumn("Amount");
            table.AddColumn("Category");
            table.AddColumn("Date");

            foreach (var e in results)
            {
                table.AddRow(
                    e.Id.ToString(),
                    e.Name,
                    e.Amount.ToString("C"),
                    e.Category,
                    e.Date.ToShortDateString()
                );
            }

            AnsiConsole.Write(table);
            Console.ReadKey();
        }
    }
}