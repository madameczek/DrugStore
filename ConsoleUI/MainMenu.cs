using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class MainMenu : MenuBuilder
    {
        protected override string MenuTitle => "Menu główne";
        
        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.InventoryMenu, "Zarządzaj lekami"),
            new MenuItem(ConsoleKey.D2, Command.ManufacturerMenu, "Zarządzaj dostawcami"),
            //new MenuItem(ConsoleKey.D3, Command.PrescriptionMenu, "Recepty"),
            new MenuItem(ConsoleKey.D4, Command.OrderMenu, "Zamówienia"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Wyjście z programu")
        };

        public override int ParseKey(ConsoleKeyInfo key)
        {
            return base.ParseKey(key);
        }
    }
}
