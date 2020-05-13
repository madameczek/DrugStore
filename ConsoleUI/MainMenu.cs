using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class MainMenu : MenuBuilder
    {
        public List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.AddCustomer, "Dodaj klienta"),
            new MenuItem(ConsoleKey.D2, Command.AddPrescription, "Dodaj receptę"),
            new MenuItem(ConsoleKey.D3, Command.AddOrder, "Dodaj zamówienie"),
            new MenuItem(ConsoleKey.D4, Command.DeleteOrder, "Usuń zamówienie"),
            new MenuItem(ConsoleKey.D5, Command.AddOrderItem, "Dodaj pozycję zamówienia"),
            new MenuItem(ConsoleKey.D6, Command.DeleteOrderItem, "Usuń pozycję zamówienia"),
            new MenuItem(ConsoleKey.D7, Command.AddManufacturer, "Dodaj dostawcę"),
            new MenuItem(ConsoleKey.D8, Command.AddMedicine, "Dodaj lek"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Wyjście")
        };

        public override List<MenuItem> Items { get => items; }
    }
}
