using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class MedicineMenu : MenuBuilder
    {
        protected override string MenuTitle => "MENU: LEKI";

        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.ListMedicines, "Wyświetl leki"),
            new MenuItem(ConsoleKey.D2, Command.PrintMedicine, "Wyświetl dane leku"),
            new MenuItem(ConsoleKey.D3, Command.UpdateMedicine, "Aktualizuj dane leku"),
            new MenuItem(ConsoleKey.D4, Command.AddMedicine, "Dodaj lek"),
            new MenuItem(ConsoleKey.D5, Command.DeleteMedicine, "Usuń lek"),
            new MenuItem(ConsoleKey.D7, Command.ManufacturerMenu, "Zarządzaj dostawcami"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Poprzednie menu")
        };

        public override int ParseKey(ConsoleKeyInfo cki)
        {
            return base.ParseKey(cki) + 10;
        }
    }
}
