using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    internal static partial class ConsoleUI
    {
        /// <summary>
        /// Methods used by OrderItem menu
        /// </summary>
        private static OrderItem GetOrderItemDetails(int orderId, int id = 0) // 0 to insert new order item
        {
            OrderItem orderItem = new OrderItem(id)
            {
                OrderId = orderId,
                MedicineId = GetExistingMedicineId("Podaj Id leku"),
                Quantity = (int)GetDecimal("Podaj ilość opakowań"),
            };
            return orderItem;
        }

        private static void AddOrUpdateOrderItem(OrderItem orderItem)
        {
            try
            {
                PrintResultOK(orderItem.Save());
            }
            catch (Exception) { throw; }
        }


        private static OrderItem ReloadOrderItem(int id)
        {
            OrderItem orderItem = new OrderItem(id);
            try
            {
                orderItem.Reload();
            }
            catch (DbResultException) { throw; }
            catch (Exception) { throw new Exception("Nieznany błąd"); }
            return orderItem;
        }




        private static void PrintOrderItems(int orderId)
        {
            List<OrderItem> orderItems;
            Dictionary<OrderItem, Medicine> orderDetails = new Dictionary<OrderItem, Medicine>();
            try
            {
                orderItems = OrderItem.GetOrderItems(orderId);
                foreach (var item in orderItems)
                {
                    Medicine medicine = new Medicine(item.MedicineId);
                    medicine.Reload();
                    orderDetails.Add(item, medicine);
                }
            }
            catch (Exception) { throw; }

            int paddingMedicineName = orderDetails.Max(m => m.Value.Name.Length);
            int paddingMedicinePrice = orderDetails.Max(m => m.Value.Price.ToString().Length);
            int paddingOrderQuantity = "Ilość".Length;
            int paddingItemDeliveredOn = 10;
            paddingMedicineName = Math.Max(paddingMedicineName, "Nazwa leku".Length);

            Console.WriteLine();
            ConsoleUI.WriteLine($"Pozycje zamówienia {orderId}. Kolumna 'Ilość' pokazuje ilość do realizacji.");
            ConsoleUI.WriteLine(new string('-', paddingMedicineName + paddingMedicinePrice + paddingOrderQuantity + paddingItemDeliveredOn + 6), ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.Write($"{"Id".PadLeft(2)}|{"Nazwa leku".PadRight(paddingMedicineName)}|{"Ilość".PadLeft(paddingOrderQuantity)}|" +
                $"{"Cena".PadLeft(paddingMedicinePrice)}|{"Wydano".PadRight(paddingItemDeliveredOn)}\n", ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.WriteLine(new string('-', paddingMedicineName + paddingMedicinePrice + paddingOrderQuantity + paddingItemDeliveredOn + 6), ConsoleUI.Colors.colorTitleBar);

            foreach (var orderItem in orderDetails)
            {
                Console.Write(orderItem.Key.Id.ToString().PadLeft(2));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(orderItem.Value.Name.PadRight(paddingMedicineName));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write($"{(orderItem.Key.Quantity == null ? string.Empty : orderItem.Key.Quantity.ToString().PadLeft(paddingOrderQuantity))}");
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar); 
                Console.Write($"{(orderItem.Value.Price == null ? string.Empty : ((decimal)orderItem.Value.Price).ToString("#.00")).PadLeft(paddingMedicinePrice)}");
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write($"{(orderItem.Key.DeliveredOn == null ? string.Empty : ((DateTimeOffset)orderItem.Key.DeliveredOn).ToString("yyyy-MM-dd").PadLeft(paddingItemDeliveredOn))}");
                Console.WriteLine();
            }
            ConsoleUI.WriteLine(new string('-', paddingMedicineName + paddingMedicinePrice + paddingOrderQuantity + paddingItemDeliveredOn + 6), ConsoleUI.Colors.colorTitleBar);
        }
    }
}
