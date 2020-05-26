using ActiveRecord.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleUI
{
    /// <summary>
    /// Main program loop. This loop catches all commands.
    /// This part of the class contains also common methods used by menus, which are application specific.
    /// 
    /// The reason for use partial class is that manipulation of menu items isn't very complicated.
    /// Creating submenus, adding and moving command is easy. To do so:
    /// 1. Change/add menu classes derived from MenuBuilder (NewMenu : Menu Builder)
    /// 2. Make corresponding changes in enum Command. Int returned by overrided NewMenu.ParseKey() has to match enum Command.
    /// </summary>
    internal partial class ConsoleUI
    {
        public static void Run(MenuBuilder menu)
        {
            Command command;
            do
            {
                menu.PrintMenu();
                command = menu.GetCommand();

                switch (command)
                {
                    
                    case Command.InventoryMenu:
                        MenuBuilder medicineMenu = new MedicineMenu();
                        Run(medicineMenu);
                        break;

                    case Command.ManufacturerMenu:
                        MenuBuilder manufacturerMenu = new ManufacturerMenu();
                        Run(manufacturerMenu);
                        break;

                    case Command.OrderMenu:
                        MenuBuilder orderMenu = new OrderMenu();
                        Run(orderMenu);
                        break;

                    case Command.PrescriptionMenu:
                        MenuBuilder prescriptionMenu = new PrescriptionMenu();
                        Run(prescriptionMenu);
                        break;

                    case Command.ListManufacturers:
                        try
                        {
                            PrintManufacturers();
                        }
                        catch (Exception) { }
                        break;

                    case Command.UpdateManufacturer:
                        try
                        {
                            int id = ConsoleUI.GetInt("Podaj nr dostawcy, którego dane będą poprawiane");
                            Manufacturer manufacturer = new Manufacturer(id);
                            try
                            {
                                manufacturer.Reload();
                            }
                            catch (Exception) { throw; }
                            ConsoleUI.WriteLine("Dane dostawcy. Jeśli pole ma pozostać nie zmienione, wciśnij [Enter].", ConsoleUI.Colors.colorHelp);
                            ConsoleUI.WriteLine(manufacturer.ToString(), ConsoleUI.Colors.colorTitleBar); ;
                            Manufacturer updatedManufacturer = GetManufacturerDetails(id);
                            AddOrUpdateManufacturer(manufacturer, updatedManufacturer);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.PrintManufacturer:
                        try
                        {
                            Manufacturer manufacturer = ReloadManufacturer(ConsoleUI.GetInt("Podaj Id dostawcy"));
                            ConsoleUI.WriteLine(manufacturer.ToString(), ConsoleUI.Colors.colorOutput);
                            if ((bool)GetBool("Czy pokazać leki tego dostawcy? ([Enter] = tak)", true))
                            {
                                PrintMedicines(manufacturer.Id);
                            }
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.AddManufacturer:
                        try
                        {
                            Manufacturer manufacturer = GetManufacturerDetails();
                            if(string.IsNullOrEmpty(manufacturer.Name)) { throw new Exception("Nazwa dostawcy nie może być pusta"); }
                            AddOrUpdateManufacturer(manufacturer);
                            ConsoleUI.WriteLine("Nowy dostawca:", ConsoleUI.Colors.colorTitleBar);
                            ConsoleUI.WriteLine(manufacturer.ToString(), ConsoleUI.Colors.colorTitleBar);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.DeleteManufacturer:
                        try
                        {
                            DeleteManufacturer(ConsoleUI.GetInt("Podaj Id dostawcy, który ma być usunięty"));
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.ListMedicines:
                        try
                        {
                            PrintMedicines();
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.UpdateMedicine:
                        try
                        {
                            int id = ConsoleUI.GetInt("Podaj nr leku, którego dane będą poprawiane");
                            Medicine medicine = new Medicine(id);
                            try
                            {
                                medicine.Reload();
                            }
                            catch (Exception) { throw; }
                            medicine = GetMedicineDetails(id);
                            AddOrUpdateMedicine(medicine);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.PrintMedicine:
                        try
                        {
                            Medicine medicine = ReloadMedicine(ConsoleUI.GetInt("Podaj Id leku"));
                            ConsoleUI.WriteLine(medicine.ToString(), ConsoleUI.Colors.colorOutput);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.AddMedicine:
                        try
                        {
                            Medicine medicine = GetMedicineDetails();
                            AddOrUpdateMedicine(medicine);
                            medicine.Reload();
                            Console.WriteLine(medicine);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.DeleteMedicine:
                        try
                        {
                            DeleteMedicine(ConsoleUI.GetInt("Podaj Id leku, który ma być usunięty"));
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.ListPrescriptions:
                        try
                        {
                            PrintPrescriptions();
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.PrintPrescription:
                        try
                        {
                            Prescription prescription = ReloadPrescription(ConsoleUI.GetInt("Podaj Id recepty"));
                            ConsoleUI.WriteLine(prescription.ToString(), ConsoleUI.Colors.colorOutput);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.UpdatePresctiption:
                        try
                        {
                            int id = ConsoleUI.GetInt("Podaj nr recepty, która będzie poprawiana");
                            Prescription prescription = new Prescription(id);
                            try
                            {
                                prescription.Reload();
                            }
                            catch (Exception) { throw; }
                            prescription = GetPrescriptionDetails(id);
                            AddOrUpdatePrescription(prescription);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.AddPrescription:
                        try
                        {
                            Prescription prescription = GetPrescriptionDetails();
                            AddOrUpdatePrescription(prescription);
                            prescription.Reload();
                            Console.WriteLine(prescription);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.DeletePrescription:
                        try
                        {
                            DeletePrescription(ConsoleUI.GetInt("Podaj Id recepty, która ma być usunięta"));
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.ListOrdersById:
                        try
                        {
                            bool isOpen = (bool)GetBool("Czy pokazać tylko nie zrealizowane zamówienia? ([t|n], [Enter] = tak)", true);
                            PrintOrders(isOpen);
                        }
                        catch (Exception) { }
                        break;

                    case Command.PrintOrder:
                        try
                        {
                            orderId = GetInt("Podaj numer zamówienia");
                            PrintOrderItems(orderId);
                            MenuBuilder orderItemMenu = new OrderItemMenu();
                            Run(orderItemMenu);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.UpdateOrder:
                        try
                        {
                            orderId = GetInt("Podaj numer zamówienia");
                            PrintOrderItems(orderId);
                            MenuBuilder orderItemMenu = new OrderItemMenu();
                            Run(orderItemMenu);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.AddOrder:
                        try
                        {
                            Order order = new Order();
                            PrintResultOK(order.Save());
                            orderId = order.Id;
                            MenuBuilder orderItemMenu = new OrderItemMenu();
                            Run(orderItemMenu);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.DeleteOrder:
                        goto default;

                    case Command.DeliverOrder:
                        try
                        {
                            orderId = GetInt("Podaj numer zamówienia");
                            PrintOrderItems(orderId);
                            PrintResultOK(DeliverOrder(orderId, out decimal orderValue));
                            //PrintOrderItems(orderId);
                            Console.WriteLine($"Wartość zrealizowanych pozycji: {orderValue.ToString("#.00#")}");
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.ListOrderItems:
                        try
                        {
                            PrintOrderItems(orderId);
                        }
                        catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        break;

                    case Command.AddOrderItem:
                        try
                        {
                            OrderItem orderItem;
                            orderItem = GetOrderItemDetails(orderId);
                            PrintResultOK(orderItem.Save());
                            orderItem.Reload();
                        }
                        catch (Exception e)
                        { 
                            if (e.Message.Contains("FK_OrderItems_Orders"))
                            {
                                ConsoleUI.WriteLine("Zamówienie zawiera już ten lek", ConsoleUI.Colors.colorError);
                            }
                            else
                            {
                                ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError);
                            }
                        }
                        break;

                    case Command.DeleteOrderItem:
                        try
                        {
                            int orderItemId = GetInt("Podaj Id pozycji zamówienia");
                            OrderItem orderItem = new OrderItem(orderItemId);
                            orderItem.Reload();
                            if(orderItem.DeliveredOn == null)
                            {
                                orderItem.Remove();
                            }
                            else
                            {
                                ConsoleUI.WriteLine("Nie można usunąć z zamówienia leku, który został wydany", ConsoleUI.Colors.colorError);
                            }
                        }
                        catch (Exception e)
                        {
                            ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError);
                        }
                        break;

                    case Command.exit:
                        /*DatabaseDump dd = new DatabaseDump();
                        dd.MedicinesDump();
                        dd.ManufacturersDump();*/
                        break;
                    default:
                        ConsoleUI.WriteLine("Komenda nie obsługiwana", ConsoleUI.Colors.colorWarning);
                        break;
                }

            } while (command != Command.exit);
        }

        /// <summary>
        /// Returns Id of existing Manufacturer
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>Returns Id of existing Manufacturer</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="DbResultException"></exception>
        static int GetExistingManufacturerId(string prompt)
        {
            int id;
            try
            {
                id = GetInt(prompt);
                Manufacturer manufacturer = new Manufacturer(id);
                manufacturer.Reload();
            }
            catch (FormatException) { throw; }
            catch (DbResultException) { throw; }
            return id;
        }

        /// <summary>
        /// Returns Id of existing Medicine
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>Returns Id of existing Medicine</returns>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="DbResultException"></exception>
        static int GetExistingMedicineId(string prompt)
        {
            int id;
            try
            {
                id = GetInt(prompt);
                Medicine medicine = new Medicine(id);
                medicine.Reload();
            }
            catch (FormatException) { throw; }
            catch (DbResultException) { throw; }
            return id;
        }

        /// <summary>
        /// Prints message on console
        /// </summary>
        /// <param name="result"></param>
        static void PrintResultOK(bool result)
        {
            if (result) { ConsoleUI.WriteLine("Operacja wykonana poprawnie", ConsoleUI.Colors.colorSuccesss); }
        }
    }
}
