using System;
using System.Collections.Generic;
using System.Text;
using ActiveRecord.DataModels;
using System.IO;

namespace ConsoleUI
{
    class DatabaseDump
    {
        public Action<string> printInserts;
        internal void MedicinesDump()
        {
            List<Medicine> medicines;
            try
            {
                medicines = Medicine.GetMedicines(0);
                File.Delete("Medicines.txt");
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            foreach (var item in medicines)
            {
                string line = "insert into Medicines (Name, ManufacturerId, Price, StockQty, IsPrescription) " +
                    $"values ('{item.Name}', {item.ManufacturerId}, {(item.Price == null ? "NULL" : item.Price.ToString().Replace(',','.'))}, "+
                    $"{(item.StockQty == null ? "NULL" : item.StockQty.ToString())}, {(item.IsPrescription == null ? "NULL" : (item.IsPrescription == true ? "1":"0"))});{Environment.NewLine}";
                File.AppendAllText("Medicines.txt", line);
            }
        }

        internal void ManufacturersDump()
        {
            List<Manufacturer> manufacturers;
            try
            {
                manufacturers = Manufacturer.GetManufacturers();
                File.Delete("Manufacturers.txt");
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            foreach (var item in manufacturers)
            {
                string line = "insert into Manufacturers (Name, Address, City, Country) " +
                    $"values ('{item.Name}', '{(item.Address ?? "NULL")}', '{(item.City ?? "NULL")}', '{(item.Country ?? "NULL")}');{Environment.NewLine}";
                File.AppendAllText("Manufacturers.txt", line);
            }
        }
    }
}
