using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class ManufacturerMenu : MenuBuilder
    {
        protected override string MenuTitle => "MENU: DOSTAWCY";

        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.ListManufacturers, "Lista dostawców"),
            new MenuItem(ConsoleKey.D2, Command.PrintManufacturer, "Pokaż dostawcę"),
            new MenuItem(ConsoleKey.D3, Command.UpdateManufacturer, "Aktualizuj dane dostawcy"),
            new MenuItem(ConsoleKey.D4, Command.AddManufacturer, "Dodaj dostawcę"),
            new MenuItem(ConsoleKey.D5, Command.DeleteManufacturer, "Usuń dostawcę"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Poprzednie menu")
        };

        public override int ParseKey(ConsoleKeyInfo cki)
        {
            return base.ParseKey(cki) + 20;
        }
    }
}
