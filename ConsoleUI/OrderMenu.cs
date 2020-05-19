using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class OrderMenu : MenuBuilder
    {
        protected override string MenuTitle => "Zamówienia";

        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D5, Command.AddOrder, "Dodaj zamówienie"),
            new MenuItem(ConsoleKey.D6, Command.AddOrderItem, "Dodaj pozycję zamówienia"),
            new MenuItem(ConsoleKey.D7, Command.DeleteOrderItem, "Usuń pozycję zamówienia"),
            new MenuItem(ConsoleKey.D8, Command.DeleteOrder, "Usuń zamówienie"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Poprzednie menu")
        };

        public override int ParseKey(ConsoleKeyInfo key)
        {
            return base.ParseKey(key) + 40;
        }
    }
}
