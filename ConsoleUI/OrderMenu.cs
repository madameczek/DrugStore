using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class OrderMenu : MenuBuilder
    {
        protected override string MenuTitle => "MENU: ZAMÓWIENIA";

        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.ListOrdersById, "Lista zamówień"),
            new MenuItem(ConsoleKey.D2, Command.PrintOrder, "Pokaż szczegóły zamówienia"),
            new MenuItem(ConsoleKey.D3, Command.UpdateOrder, "Edytuj zamówienie"),
            new MenuItem(ConsoleKey.D4, Command.AddOrder, "Dodaj zamówienie"),
            //new MenuItem(ConsoleKey.D5, Command.DeleteOrder, "Usuń zamówienie"),
            new MenuItem(ConsoleKey.D6, Command.DeliverOrder, "Realizuj zamówienie"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Poprzednie menu")
        };

        public override int ParseKey(ConsoleKeyInfo key)
        {
            return base.ParseKey(key) + 40;
        }
    }
}
