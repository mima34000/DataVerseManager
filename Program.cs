using DataVerseManager.Models;
using DataVerseManager.Services;
using DataVerseManager.UI;
using Spectre.Console;

// show a spinner while loading data
var jsonService = new JsonService();
var store = new DataStore<Expense>();

AnsiConsole.Status()
    .Start("Loading data...", ctx =>
    {
        ctx.Spinner(Spinner.Known.Dots);
        ctx.SpinnerStyle(Style.Parse("green"));
        store.SetAll(jsonService.Load());
    });

// start the main menu
var menu = new MenuDisplay(store, jsonService);
menu.ShowMainMenu();