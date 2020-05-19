using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    /// <summary>
    /// Methods used by InventoryMenu
    /// </summary>
    internal partial class ConsoleUI
    {
        private Medicine GetMedicineDetails(int id = 0) // 0 to insert new record
        {
            Medicine medicine = new Medicine(id);
            try
            {
                medicine.Name = GetNotEmptyString("Podaj nazwę leku");
                medicine.ManufacturerId = GetExistingId("Podaj Id dostawcy (musi być w bazie)");
                medicine.Price = GetDecimal("Podaj cenę ([Enter] by nie wprowadzać)");
                medicine.IsPrescription = GetBool("Czy lek jest na receptę? [Enter] by nie wprowadzać");
                medicine.StockQty = (int?)GetDecimal("Podaj stan magazynowy [Enter] by nie wprowadzać");
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            return medicine;
        }

        private void AddOrUpdateMedicine(Medicine medicine)
        {
            try
            {
                PrintResultOK(medicine.Save());
            }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
        }

        private Medicine ReloadMedicine(int id)
        {
            Medicine medicine = new Medicine(id);
            try
            {
                medicine.Reload();
            }
            catch (ArgumentException e) { throw; }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception) { ConsoleUI.WriteLine("Nieznany błąd", ConsoleUI.Colors.colorError); throw; }
            return medicine;
        }

        private void PrintMedicines(int manufacurerId = 0)
        {
            List<Medicine> medicines;
            try
            {
                medicines = Medicine.GetMedicines(manufacurerId);
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }

            int paddingName = medicines.Max(m => m.Name.Length);
            int paddingManufacturer = medicines.Max(m => m.Manufacturer.ToString().Length);
            int paddingPrice = medicines.Max(m => m.Price.ToString().Length);
            int paddingStockQty = medicines.Max(m => m.StockQty.ToString().Length);
            paddingName = Math.Max(paddingName, "Nazwa leku".Length);
            paddingManufacturer = Math.Max(paddingManufacturer, "Dostawca".Length);
            paddingPrice = Math.Max(paddingPrice, "Cena".Length);
            paddingStockQty = Math.Max(paddingStockQty, "Magazyn".Length);
            int paddingIsPrescription = Math.Max(3, "Recepta".Length);

            ConsoleUI.WriteLine(new string('-', paddingName + paddingManufacturer + paddingPrice + paddingStockQty + paddingIsPrescription + 9), ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.Write($"{"Id".PadLeft(4)}|{"Nazwa leku".PadRight(paddingName)}|{"Dostawca".PadRight(paddingManufacturer)}|" +
                $"{"Cena".PadLeft(paddingPrice)}|{"Magazyn".PadLeft(paddingStockQty)}|" +
                $"{"Recepta".PadRight(paddingIsPrescription)}\n", ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.WriteLine(new string('-', paddingName + paddingManufacturer + paddingPrice + paddingStockQty + paddingIsPrescription + 9), ConsoleUI.Colors.colorTitleBar);

            foreach (Medicine medicine in medicines)
            {
                Console.Write(medicine.Id.ToString().PadLeft(4));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(medicine.Name.ToString().PadRight(paddingName));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(medicine.Manufacturer.ToString().PadRight(paddingManufacturer));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(medicine.Price.ToString().PadLeft(paddingPrice));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(medicine.StockQty.ToString().PadLeft(paddingStockQty));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write($"{(medicine.IsPrescription == true ? "TAK" : medicine.IsPrescription == false ? "NIE" : string.Empty).PadRight(paddingIsPrescription)}");
                Console.WriteLine();
            }
            ConsoleUI.WriteLine(new string('-', paddingName + paddingManufacturer + paddingPrice + paddingStockQty + paddingIsPrescription + 9), ConsoleUI.Colors.colorTitleBar);
        }
    }
}
