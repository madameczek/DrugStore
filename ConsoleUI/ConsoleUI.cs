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
            public const ConsoleColor colorInputPrompt = ConsoleColor.DarkGray;
            public const ConsoleColor colorOutput = ConsoleColor.DarkCyan;
            public const ConsoleColor colorSuccesss = ConsoleColor.Green;
            public const ConsoleColor colorError = ConsoleColor.Red;
            public const ConsoleColor colorWarning = ConsoleColor.Yellow;
            public const ConsoleColor colorHelp = ConsoleColor.DarkGray;
            public const ConsoleColor colorTitleBar = ConsoleColor.DarkCyan;
        }

        public static string GetString(string prompt)
        {
            ConsoleUI.Write($"{prompt}: ", ConsoleUI.Colors.colorInputPrompt);
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

        /// <summary>
        /// Returns int from console input
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>Returns int</returns>
        /// <exception cref="FormatException"></exception>
        private static int GetInt(string prompt)
        {
            if (int.TryParse(ConsoleUI.GetString(prompt), out int id))
            {
                return id;
            }
            throw new FormatException("Nie rozpoznano liczby.");
        }

        /// <summary>
        /// Returns decimal from console input
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>Returns decimal</returns>
        /// <exception cref="FormatException"></exception>
        static decimal? GetDecimal(string prompt)
        {
            string stringToParse = ConsoleUI.GetString(prompt);
            if (string.IsNullOrEmpty(stringToParse)) { return null; }
            bool isResult = Decimal.TryParse(stringToParse, out decimal number);
            if (isResult) { return number; }
            throw new FormatException("Nie rozpoznano liczby.");
        }

        /// <summary>
        /// Returns bool? from console input.
        /// If parameter trueOnEmpty = true and ipmut string is empty, then returns true (shortcut for true).
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="trueOnEmpty"></param>
        /// <returns></returns>
        /// <exception cref="FormatException"></exception>
        static bool? GetBool(string prompt, bool trueOnEmpty = false)
        {
            string stringToParse = ConsoleUI.GetString(prompt);
            string[] yes = { "yes", "y", "tak", "t", " true", "1" };
            string[] no = { "no", "n", "nie", "false", "f", "0" };
            if (string.IsNullOrEmpty(stringToParse))
            {
                if (trueOnEmpty) { return true; }
                else { return null; }
            }
            foreach (string item in yes)
            {
                if (item == stringToParse.ToLower().Trim()) { return true; }
            }
            foreach (string item in no)
            {
                if (item == stringToParse.ToLower().Trim()) { return false; }
            }
            throw new FormatException("Nie rozpoznano odpowiedzi.");
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
