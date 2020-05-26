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
        private static Medicine GetMedicineDetails(int id = 0) // 0 to insert new record
        {
            Medicine medicine = new Medicine(id);
            try
            {
                medicine.Name = GetNotEmptyString("Podaj nazwę leku");
                medicine.ManufacturerId = GetExistingManufacturerId("Podaj Id dostawcy (musi być w bazie)");
                medicine.Price = GetDecimal("Podaj cenę ([Enter] by nie wprowadzać)");
                medicine.IsPrescription = GetBool("Czy lek jest na receptę? [Enter] by nie wprowadzać");
                medicine.StockQty = (int?)GetDecimal("Podaj stan magazynowy [Enter] by nie wprowadzać");
            }
            catch (Exception) { throw; }
            return medicine;
        }

        private static void AddOrUpdateMedicine(Medicine medicine)
        {
            try
            {
                PrintResultOK(medicine.Save());
            }
            catch (Exception) { throw; }
        }

        private static Medicine ReloadMedicine(int id)
        {
            Medicine medicine = new Medicine(id);
            try
            {
                medicine.Reload();
            }
            catch (DbResultException) { throw; }
            catch (Exception) { throw new Exception("Nieznany błąd"); }
            return medicine;
        }

        private static void PrintMedicines(int manufacurerId = 0)
        {
            List<Medicine> medicines;
            try
            {
                medicines = Medicine.GetMedicines(manufacurerId);
            }
            catch (Exception) { throw; }

            int paddingName = medicines.Max(m => m.Name.Length);
            int paddingManufacturer = medicines.Max(m => m.Manufacturer.ToString().Length);
            int paddingPrice = medicines.Max(m => m.Price.ToString().Length);
            int paddingStockQty = medicines.Max(m => m.StockQty.ToString().Length);
            paddingName = Math.Max(paddingName, "Nazwa leku".Length);
            paddingManufacturer = Math.Max(paddingManufacturer, "Dostawca".Length);
            paddingPrice = Math.Max(paddingPrice, "Cena".Length);
            paddingStockQty = Math.Max(paddingStockQty, "Magazyn".Length);
            int paddingIsPrescription = Math.Max(3, "Recepta".Length);

            Console.WriteLine();
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
                Console.Write($"{(medicine.Price == null ? string.Empty : ((decimal)medicine.Price).ToString("#.00")).PadLeft(paddingPrice)}");
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(medicine.StockQty.ToString().PadLeft(paddingStockQty));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write($"{(medicine.IsPrescription == true ? "TAK" : medicine.IsPrescription == false ? "NIE" : string.Empty).PadRight(paddingIsPrescription)}");
                Console.WriteLine();
            }
            ConsoleUI.WriteLine(new string('-', paddingName + paddingManufacturer + paddingPrice + paddingStockQty + paddingIsPrescription + 9), ConsoleUI.Colors.colorTitleBar);
        }

        private static void DeleteMedicine(int id)
        {
            try
            {
                PrintResultOK(new Medicine(id).Remove());
            }
            catch (DbResultException) { throw; }
            catch (Exception e)
            {
                if (e.Message.Contains("FK_OrderDetails_Medicines"))
                { 
                    throw new Exception("Nie można usunąć leku, bo istnieją w bazie powiązane recepty"); 
                }
                else 
                { 
                    throw new Exception("Wystąpił błąd: " + e.Message);
                }
                throw;
            }
        }
    }
}
