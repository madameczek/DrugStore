using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ConsoleUI
{
    /// <summary>
    /// Common static methods for basic console I/O
    /// </summary>
    internal partial class ConsoleUI
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

        // When using Console.TreatControlCAsInput = true;
        // standard Console.ReadLine() do not work properly due to MS bug.
        // Therefore custom Readline() should be developed.
        // Move to feature branch. It needs some work...
        public static string ReadLine()
        {
            ConsoleKeyInfo cki;
            StringBuilder sb = new StringBuilder();
            do
            {
                cki = Console.ReadKey();
                if (cki.Key == ConsoleKey.Backspace)
                {
                    sb.Length -= 1;
                    Console.CursorLeft -= 1;
                }
                if (cki.Key == ConsoleKey.Escape)
                {
                    return null;
                }
                sb.Append(cki.KeyChar);
            } while (cki.Key != ConsoleKey.Enter);
            return sb.ToString();
        }
    }
}
