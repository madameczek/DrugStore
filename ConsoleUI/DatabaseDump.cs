﻿using System;
using System.Collections.Generic;
using System.Text;
using ActiveRecord.DataModels;
using System.IO;
using System.Globalization;

namespace ConsoleUI
{
    class DatabaseDump
    {
        internal void MedicinesDump()
        {
            List<Medicine> medicines;
            try
            {
                medicines = Medicine.GetMedicines();
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
                    $"values ('{item.Name}', '{(item.Address ?? "NULL")}', '{(item.City ?? "NULL")}', '{item.Country ?? "NULL"}');{Environment.NewLine}";
                File.AppendAllText("Manufacturers.txt", line);
            }
        }

        internal void OrdersDump()
        {
            CultureInfo culture = new CultureInfo("EN-us");
            List<Order> orders;
            try
            {
                orders = Order.GetOrders(false);
                File.Delete("Orders.txt");
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            foreach (var item in orders)
            {
                string line = $"insert into Orders (CreatedOn) values ('{item.CreatedOn.ToString(culture)}');{Environment.NewLine}";
                File.AppendAllText("Orders.txt", line);
            }
        }

        internal void OrderItemsDump()
        {
            CultureInfo culture = new CultureInfo("EN-us");
            List<OrderItem> orderItems;
            try
            {
                orderItems = OrderItem.GetOrderItems();
                File.Delete("OrderItems.txt");
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            foreach (var item in orderItems)
            {
                string line = "insert into OrderItems (OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) "+
                    $"values ({item.OrderId}, {item.MedicineId}, {(item.PrescriptionId == null ? "NULL": item.PrescriptionId.ToString())}, "+
                    $"{(item.Quantity == null ? "NULL" : item.Quantity.ToString())}, {(item.DeliveredOn == null ? "NULL" : ((DateTimeOffset)item.DeliveredOn).ToString(culture))});{Environment.NewLine}";
                File.AppendAllText("OrderItems.txt", line);
            }
        }
    }
}
