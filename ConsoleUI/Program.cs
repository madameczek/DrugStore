using System;
using ActiveRecord;
using System.Threading;
using System.Data.SqlClient;

namespace ConsoleUI
{
    class Program
    {
        
        static void Main()
        {

            if (!CheckDb()) { return; }
            Command command;
            do 
            {
                MenuBuilder mainMenu = new MainMenu();
                mainMenu.PrintMenu();
                command = mainMenu.GetCommand(); 
            } while (command != Command.exit);

            



            //Console.ReadKey();
        }

        static bool CheckDb()
        {
            Console.Write("Łączę z bazą danych... ");
            try
            {
                if (!ActiveRecord.ActiveRecord.DatabaseExists())
                {
                    ConsoleUI.WriteLine($"\nNie można znaleźć bazy danych. Sprawdź, czy baza danych jest założona zgodnie z instrukcjami w pliku readme.md", ConsoleUI.Colors.colorError);
                    Thread.Sleep(2000);
                    return false;
                }
                else
                {
                    ConsoleUI.Write("OK", ConsoleUI.Colors.colorSuccesss);
                    Thread.Sleep(800);
                    Console.CursorLeft = 0;
                    Console.Write(new string(' ', 27));
                    Console.CursorLeft = 0;
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
