using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    public class ConsoleUI
    {
        public struct Colors
        {
            public const ConsoleColor colorInputPrompt = ConsoleColor.DarkCyan;
            public const ConsoleColor colorSuccesss = ConsoleColor.Green;
            public const ConsoleColor colorError = ConsoleColor.Red;
            public const ConsoleColor colorWarning = ConsoleColor.Yellow;
            public const ConsoleColor colorHelp = ConsoleColor.DarkGray;
            public const ConsoleColor colorTitleBar = ConsoleColor.DarkCyan;
        }

        public static string GetString(string prompt)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine().Trim();
            return input;
        }

        public static string GetNotEmptyString(string prompt)
        {
            string output;
            do
            {
                output = GetString(prompt);
            } while (output == string.Empty);
            return output;
        }

        public static void Write(string text, ConsoleColor color)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = tempColor;
        }

        public static void Write(string text)
        {
            Console.Write(text);
        }

        public static void WriteLine(string text, ConsoleColor color)
        {
            ConsoleColor tempColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = tempColor;
        }

        public static void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}
