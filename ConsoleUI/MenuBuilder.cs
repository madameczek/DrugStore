using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public enum Command
    {
        AddCustomer = 1,
        AddPrescription = 2,
        AddOrder = 3,
        DeleteOrder = 4,
        AddOrderItem = 5,
        DeleteOrderItem = 6,
        AddManufacturer = 7,
        AddMedicine = 8,
        exit = 0
    }
    abstract class MenuBuilder
    {
        private readonly string selectOptionText = "Wybierz opcję:";
        public struct MenuItem
        {
            internal ConsoleKey ItemKey { get; set; }
            public Command Command { get; private set; }
            internal string PromptKey { get; private set; }
            internal string PromptText { get; private set; }

            public MenuItem(ConsoleKey itemKey, Command command, string promptKey, string promptText)
            {
                ItemKey = itemKey;
                Command = command;
                PromptKey = promptKey;
                PromptText = promptText;
            }

            /// <summary>
            /// Three parameters constructor use only for digit keys 0 - 9
            /// </summary>
            /// <param name="itemKey"></param>
            /// <param name="command"></param>
            /// <param name="promptText"></param>
            public MenuItem(ConsoleKey itemKey, Command command, string promptText)
            {
                ItemKey = itemKey;
                Command = command;
                PromptKey = itemKey.ToString().Substring(1, 1);
                PromptText = promptText;
            }
        }

        public abstract List<MenuItem> Items { get; }

        public void PrintMenu()
        {
            foreach (MenuItem item in Items)
            {
                ConsoleUI.Write(item.PromptKey, ConsoleUI.Colors.colorInputPrompt);
                Console.WriteLine($" - {item.PromptText}");
            }
            Console.Write(selectOptionText);
        }

        public Command GetCommand()
        {
            bool exitLoop = false;
            Command command = Command.exit;
            do
            {
                Console.TreatControlCAsInput = true;
                ConsoleColor tmpColor = Console.ForegroundColor;
                Console.ForegroundColor = Console.BackgroundColor;
                ConsoleKeyInfo consoleKey = Console.ReadKey();
                foreach (MenuItem item in Items)
                {
                    if (item.ItemKey == consoleKey.Key)
                    {
                        exitLoop = true;
                        command = item.Command;
                    }
                }
                Console.CursorLeft = selectOptionText.Length;
                Console.ForegroundColor = tmpColor;
            } while (!exitLoop);
            Console.WriteLine();
            return command;
        }

        public virtual int ParseKey(ConsoleKeyInfo key)
        {
            return int.Parse(key.Key.ToString().Substring(1, 1));
        }
    }
}
