using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    internal static partial class ConsoleUI
    {
        private static int orderId = 0;

        /// <summary>
        /// Methods used by Order menu
        /// </summary>
        private static void PrintOrders(bool isOpen = false)
        {
            List<Order> orders;
            try
            {
                orders = Order.GetOrders(isOpen);
            }
            catch (Exception) { throw; }

            if(isOpen)
            {
                Console.WriteLine("Zamówienia posiadające nie zrealizowane pozycje");
            }
            else
            {
                Console.WriteLine("Wszystkie zamówienia");
            }
            orders.ForEach(o => ConsoleUI.WriteLine(o.ToString(),ConsoleUI.Colors.colorTitleBar));
        }

        private static bool DeliverOrder(int orderId)
        {
            try
            {
                List<OrderItem> orderItems = OrderItem.GetOrderItems(orderId);
                List<Medicine> medicines = Medicine.GetMedicines();
                foreach (var item in orderItems)
                {
                    Medicine medicine = medicines.Where(m => m.Id == item.MedicineId && m.StockQty >= item.Quantity && item.DeliveredOn == null).FirstOrDefault();
                    if (medicine != null)
                    {
                        medicine.StockQty -= item.Quantity;
                        item.DeliveredOn = DateTimeOffset.Now;
                        medicine.Save();
                        item.Save();
                    }
                }
            }
            catch (Exception) { throw; }
            return true;
        }
    }
}
