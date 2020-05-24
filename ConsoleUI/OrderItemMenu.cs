using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    class OrderItemMenu: MenuBuilder
    {
        protected override string MenuTitle => "Pozycje zamówienia";

        protected override List<MenuItem> Items => items;

        private readonly List<MenuItem> items = new List<MenuItem>
        {
            new MenuItem(ConsoleKey.D1, Command.ListOrderItems, "Pokaż pozycje zamówienia"),
            new MenuItem(ConsoleKey.D4, Command.AddOrderItem, "Dodaj pozycję zamówienia"),
            new MenuItem(ConsoleKey.D5, Command.DeleteOrderItem, "Usuń pozycję zamówienia"),
            new MenuItem(ConsoleKey.Escape, Command.exit, "Esc", "Poprzednie menu")
        };

        public override int ParseKey(ConsoleKeyInfo key)
        {
            return base.ParseKey(key) + 50;
        }
    }
}
