using System;
using System.Collections.Generic;
using System.Linq;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    /// <summary>
    /// Methods used by InventoryMenu
    /// </summary>
    internal partial class ConsoleUI
    {
        private Manufacturer GetManufacturerDetails(int id = 0) // 0 to insert new record
        {
            Manufacturer manufacturer = new Manufacturer(id)
            {
                Name = ConsoleUI.GetNotEmptyString("Podaj nazwę dostawcy"),
                Address = ConsoleUI.GetString("Podaj adres"),
                City = ConsoleUI.GetString("Podaj miasto"),
                Country = ConsoleUI.GetString("Podaj kraj")
            };
            return manufacturer;
        }

        private void AddOrUpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                PrintResultOK(manufacturer.Save());
            }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
        }

        private Manufacturer ReloadManufacturer(int id)
        {
            Manufacturer manufacturer = new Manufacturer(id);
            try
            {
                manufacturer.Reload();
            }
            catch (ArgumentException e) { throw; }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception) { ConsoleUI.WriteLine("Nieznany błąd", ConsoleUI.Colors.colorError); throw; }
            return manufacturer;
        }

        private void PrintManufacturers()
        {
            List<Manufacturer> manufacturers;
            try
            {
                manufacturers = Manufacturer.GetManufacturers();
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }

            int paddingName = manufacturers.Max(m => m.Name.Length);
            int paddingAddress = manufacturers.Max(m => m.Address.Length);
            int paddingCity = manufacturers.Max(m => m.City.Length);
            int paddingCountry = manufacturers.Max(m => m.Country.Length);
            paddingName = Math.Max(paddingName, "Nazwa firmy".Length);
            paddingAddress = Math.Max(paddingAddress, "Adres".Length);
            paddingCity = Math.Max(paddingCity, "Miasto".Length);
            paddingCountry = Math.Max(paddingCountry, "Kraj".Length);

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

        private void DeleteManufacturer(int id)
        {
            try
            {
                PrintResultOK(new Manufacturer(id).Remove());
            }
            catch (ArgumentException) { throw; }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
            catch (Exception e)
            {
                if (e.Message.Contains("FK_Medicines_Manufacturers"))
                {
                    ConsoleUI.WriteLine("Nie można usunąć dostawcy, bo istnieją w bazie powiązane produkty", ConsoleUI.Colors.colorError);
                }
                else { ConsoleUI.WriteLine(e.Message + "Wystąpił nieznany błąd", ConsoleUI.Colors.colorError); }
            }
        }
    }
}
