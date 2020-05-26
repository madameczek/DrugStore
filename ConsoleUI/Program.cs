using System;
using System.Threading;
using System.Data.SqlClient;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Drug Store 0.9 beta. SQL database based drug store helper.";
            // Check if executable's arguments can be an existing database name or take default name.
            if (args.Length > 1)
            {
                ConsoleUI.WriteLine("Można podać tylko jeden argument jako nazwę bazy danych.", ConsoleUI.Colors.colorError);
                return;
            }
            if (args.Length ==1) { ActiveRecord.ActiveRecord.dbName = args[0]; }
            if (!CheckDb(ActiveRecord.ActiveRecord.dbName)) { return; }

            MenuBuilder mainMenu = new MainMenu();
            ConsoleUI.Run(mainMenu);
        }
        /// <summary>
        /// Check if database exists. This does not check correct db schema.
        /// </summary>
        /// <param name="dbName"></param>
        /// <returns></returns>
        static bool CheckDb(string dbName)
        {

            string connecting = $"Łączę z bazą danych '{dbName}'... ";
            Console.Write(connecting);
            try
            {
                if (!ActiveRecord.ActiveRecord.DatabaseExists(dbName))
                {
                    ConsoleUI.WriteLine($"\nNie można znaleźć bazy danych. Sprawdź, czy baza danych jest utworzona zgodnie z instrukcjami w pliku readme.md", ConsoleUI.Colors.colorError);
                    Thread.Sleep(2000);
                    return false;
                }
                else
                {
                    ConsoleUI.Write("OK", ConsoleUI.Colors.colorSuccesss);
                    Thread.Sleep(800);
                    Console.CursorLeft = 0;
                    ConsoleUI.WriteLine("Drug Store 0.9 beta. Welcome".PadRight(connecting.Length + 2), ConsoleUI.Colors.colorTitleBar);
                }
            }
            catch (SqlException)
            {
                ConsoleUI.WriteLine("\nBłąd połączenia z serwerem SQLExpress", ConsoleUI.Colors.colorError);
                Thread.Sleep(1500);
                return false;
            }
            return true;
        }
    }
}
