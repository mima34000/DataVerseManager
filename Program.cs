using DataVerseManager.Models;
using DataVerseManager.Services;
using DataVerseManager.UI;

// load existing expenses from json file on startup
var jsonService = new JsonService();
var store = new DataStore<Expense>();
store.SetAll(jsonService.Load());

// start the main menu
var menu = new MenuDisplay(store, jsonService);
menu.ShowMainMenu();