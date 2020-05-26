using System;
using System.Collections.Generic;
using System.Linq;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    /// <summary>
    /// Methods used by InventoryMenu
    /// </summary>
    internal static partial class ConsoleUI
    {
        private static Manufacturer GetManufacturerDetails(int id = 0) // 0 to insert new record
        {
            Manufacturer manufacturer = new Manufacturer(id)
            {
                Name = ConsoleUI.GetString("Podaj nazwę dostawcy"),
                Address = ConsoleUI.GetString("Podaj adres"),
                City = ConsoleUI.GetString("Podaj miasto"),
                Country = ConsoleUI.GetString("Podaj kraj")
            };
            return manufacturer;
        }

        private static void AddOrUpdateManufacturer(Manufacturer manufacturer, Manufacturer updatedManufacturer = null)
        {
            if(updatedManufacturer != null)
            {
                if (!string.IsNullOrEmpty(updatedManufacturer.Name)) { manufacturer.Name = updatedManufacturer.Name; }
                if (!string.IsNullOrEmpty(updatedManufacturer.Address)) { manufacturer.Address = updatedManufacturer.Address; }
                if (!string.IsNullOrEmpty(updatedManufacturer.City)) { manufacturer.City = updatedManufacturer.City; }
                if (!string.IsNullOrEmpty(updatedManufacturer.Country)) { manufacturer.Country = updatedManufacturer.Country; }
            }
            try
            {
                PrintResultOK(manufacturer.Save());
            }
            catch (Exception) { throw; }
        }

        private static Manufacturer ReloadManufacturer(int id)
        {
            Manufacturer manufacturer = new Manufacturer(id);
            try
            {
                manufacturer.Reload();
            }
            catch (DbResultException) { throw; }
            catch (Exception) { throw new Exception("Nieznany błąd"); }
            return manufacturer;
        }

        private static void PrintManufacturers()
        {
            List<Manufacturer> manufacturers;
            try
            {
                manufacturers = Manufacturer.GetManufacturers();
            }
            catch (Exception) { throw; }

            int paddingName = manufacturers.Max(m => m.Name.Length);
            int paddingAddress = manufacturers.Max(m => m.Address.Length);
            int paddingCity = manufacturers.Max(m => m.City.Length);
            int paddingCountry = manufacturers.Max(m => m.Country.Length);
            paddingName = Math.Max(paddingName, "Nazwa firmy".Length);
            paddingAddress = Math.Max(paddingAddress, "Adres".Length);
            paddingCity = Math.Max(paddingCity, "Miasto".Length);
            paddingCountry = Math.Max(paddingCountry, "Kraj".Length);

            Console.WriteLine();
            ConsoleUI.WriteLine(new string('-', paddingName + paddingAddress + paddingCity + paddingCountry + 8), ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.Write($"{"Id".PadLeft(4)}|{"Nazwa firmy".PadRight(paddingName)}|{"Adres".PadRight(paddingAddress)}|" +
                $"{"Miasto".PadRight(paddingCity)}|{"Kraj".PadRight(paddingCountry)}\n",ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.WriteLine(new string('-', paddingName + paddingAddress + paddingCity + paddingCountry + 8), ConsoleUI.Colors.colorTitleBar);

            foreach (Manufacturer manufacturer in manufacturers)
            {
                Console.Write(manufacturer.Id.ToString().PadLeft(4));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(manufacturer.Name.ToString().PadRight(paddingName));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(manufacturer.Address.ToString().PadRight(paddingAddress));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(manufacturer.City.ToString().PadRight(paddingCity));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(manufacturer.Country.ToString().PadRight(paddingCountry));
                Console.WriteLine();
            }
            ConsoleUI.WriteLine(new string('-', paddingName + paddingAddress + paddingCity + paddingCountry + 8), ConsoleUI.Colors.colorTitleBar);
        }

        private static void DeleteManufacturer(int id)
        {
            try
            {
                PrintResultOK(new Manufacturer(id).Remove());
            }
            catch (DbResultException) { throw; }
            catch (Exception e)
            {
                if (e.Message.Contains("FK_Medicines_Manufacturers"))
                {
                    throw new Exception("Nie można usunąć dostawcy, bo istnieją w bazie powiązane produkty");
                }
                else 
                { 
                    throw new Exception("Wystąpił błąd: "+ e.Message);
                }
            }
        }
    }
}
