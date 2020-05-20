using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    /// <summary>
    /// Common static methods for basic console I/O
    /// </summary>
    internal static partial class ConsoleUI
    {
        public readonly struct Colors
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
            string output = Console.ReadLine().Trim();
            return string.IsNullOrEmpty(output) ? null : output;
        }

        public static string GetNotEmptyString(string prompt)
        {
            string output;
            do
            {
                output = GetString(prompt);
            } while (string.IsNullOrEmpty(output));
            return output;
        }

        public static void Write(string text, ConsoleColor color)
        {
            ConsoleColor saveConsoleColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = saveConsoleColor;
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
