﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ConsoleUI
{
    public enum Command
    {
        InventoryMenu = 1,
        ManufacturerMenu,
        PrescriptionMenu,
        OrderMenu,

        ListMedicines = 11,
        PrintMedicine,
        UpdateMedicine,
        AddMedicine,
        DeleteMedicine,

        ListManufacturers = 21,
        PrintManufacturer,
        UpdateManufacturer,
        AddManufacturer,
        DeleteManufacturer,

        ListPrescriptions = 31,
        PrintPrescription,
        UpdatePresctiption,
        AddPrescription,
        DeletePrescription,

        ListOrdersById = 41,
        ListOrdersByPesel,
        UpdateOrder,
        PrintOrder,
        DeliverOrder,
        AddOrder,
        DeleteOrder,

        ListOrderItems = 51,
        AddOrderItem,
        DeleteOrderItem,

        exit = 0
    }
    abstract class MenuBuilder
    {
        protected abstract string MenuTitle { get; }
        private readonly string selectOptionText = "Wybierz opcję:";
        private const int promptKeyPadding = 3;
        protected struct MenuItem
        {
            internal ConsoleKey CommandKey { get; set; }
            public Command Command { get; private set; }
            internal string PromptKey { get; private set; }
            internal string PromptText { get; private set; }

            public MenuItem(ConsoleKey commandKey, Command command, string promptKey, string promptText)
            {
                CommandKey = commandKey;
                Command = command;
                PromptKey = promptKey;
                PromptText = promptText;
            }

            /// <summary>
            /// Three parameters constructor use only for digit keys 0 - 9
            /// </summary>
            /// <param name="commandKey"></param>
            /// <param name="command"></param>
            /// <param name="promptText"></param>
            public MenuItem(ConsoleKey commandKey, Command command, string promptText)
            {
                CommandKey = commandKey;
                Command = command;
                PromptKey = commandKey.ToString().Substring(1, 1);
                PromptText = promptText;
            }
        }

        protected abstract List<MenuItem> Items { get; }

        public void PrintMenu()
        {
            Console.WriteLine();
            ConsoleUI.WriteLine(MenuTitle, ConsoleUI.Colors.colorTitleBar);
            Console.WriteLine();
            foreach (MenuItem item in Items)
            {
                ConsoleUI.Write(item.PromptKey.PadLeft(promptKeyPadding), ConsoleUI.Colors.colorTitleBar);
                Console.WriteLine($" - {item.PromptText}");
            }
            ConsoleUI.Write(selectOptionText, ConsoleUI.Colors.colorInputPrompt);
        }

        public Command GetCommand()
        {
            bool exitLoop = false;

            Command command = Command.exit;
            do
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                foreach (MenuItem item in Items)
                {
                    if (item.CommandKey == cki.Key)
                    {
                        exitLoop = true;
                        command = item.Command;
                    }
                }
                Console.CursorLeft = selectOptionText.Length;
            } while (!exitLoop);
            Console.CursorLeft = 0;
            Console.WriteLine(new string(' ', selectOptionText.Length + 1));
            Console.CursorTop -= 1;
            return command;
        }

        // To bound Command enum values higher than 9 with digit keys 0-9
        // override in derives class and add a shifting number to base method
        // ie. return base.ParseKey(key) + 10;
        public virtual int ParseKey(ConsoleKeyInfo cki)
        {
            return int.Parse(cki.Key.ToString().Substring(1, 1));
        }
    }
}
