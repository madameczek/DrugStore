using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class PrescriptionMenu : MenuBuilder
    {
        protected override string MenuTitle => "MENU: RECEPTY";

        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.ListPrescriptions, "Lista recept"),
            new MenuItem(ConsoleKey.D2, Command.PrintPrescription, "Pokaż receptę"),
            new MenuItem(ConsoleKey.D3, Command.UpdatePresctiption, "Aktualizuj dane recepty"),
            new MenuItem(ConsoleKey.D4, Command.AddPrescription, "Dodaj receptę"),
            new MenuItem(ConsoleKey.D5, Command.DeletePrescription, "Usuń receptę"),
            new MenuItem(ConsoleKey.D6, Command.OrderMenu, "Menu zamówień"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Poprzednie menu")
        };

        public override int ParseKey(ConsoleKeyInfo key)
        {
            return base.ParseKey(key) + 30;
        }
    }
}
