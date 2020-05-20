using ActiveRecord.DataModels;
using System;
using System.Collections.Generic;
using System.Text;

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
                            int id = ConsoleUI.GetId("Podaj nr dostawcy, którego dane będą poprawiane");
                            Manufacturer manufacturer = new Manufacturer(id);
                            try
                            {
                                manufacturer.Reload();
                            }
                            catch (ArgumentException) { throw; }
                            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
                            manufacturer = GetManufacturerDetails(id);
                            AddOrUpdateManufacturer(manufacturer);
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.PrintManufacturer:
                        try
                        {
                            Manufacturer manufacturer = ReloadManufacturer(ConsoleUI.GetId("Podaj Id dostawcy"));
                            Console.WriteLine(manufacturer);
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.AddManufacturer:
                        try
                        {
                            Manufacturer manufacturer = GetManufacturerDetails();
                            AddOrUpdateManufacturer(manufacturer);
                            Console.WriteLine(manufacturer);
                        }
                        catch (Exception) { }
                        break;

                    case Command.DeleteManufacturer:
                        try
                        {
                            DeleteManufacturer(ConsoleUI.GetId("Podaj Id dostawcy, który ma być usunięty"));
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.ListMedicines:
                        try
                        {
                            PrintMedicines();
                        }
                        catch (Exception) { }
                        break;

                    case Command.UpdateMedicine:
                        try
                        {
                            int id = ConsoleUI.GetId("Podaj nr leku, którego dane będą poprawiane");
                            Medicine medicine = new Medicine(id);
                            try
                            {
                                medicine.Reload();
                            }
                            catch (ArgumentException) { throw; }
                            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
                            medicine = GetMedicineDetails(id);
                            AddOrUpdateMedicine(medicine);
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.PrintMedicine:
                        try
                        {
                            Medicine medicine = ReloadMedicine(ConsoleUI.GetId("Podaj Id leku"));
                            Console.WriteLine(medicine);
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.AddMedicine:
                        try
                        {
                            Medicine medicine = GetMedicineDetails();
                            AddOrUpdateMedicine(medicine);
                            medicine.Reload();
                            Console.WriteLine(medicine);
                        }
                        catch (Exception) { }
                        break;

                    case Command.DeleteMedicine:
                        try
                        {
                            DeleteMedicine(ConsoleUI.GetId("Podaj Id leku, który ma być usunięty"));
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.ListPrescriptions:
                        try
                        {
                            PrintPrescriptions();
                        }
                        catch (Exception) { }
                        break;

                    case Command.PrintPrescription:
                        try
                        {
                            Prescription prescription = ReloadPrescription(ConsoleUI.GetId("Podaj Id recepty"));
                            Console.WriteLine(prescription);
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.UpdatePresctiption:
                        try
                        {
                            int id = ConsoleUI.GetId("Podaj nr recepty, która będzie poprawiana");
                            Prescription prescription = new Prescription(id);
                            try
                            {
                                prescription.Reload();
                            }
                            catch (ArgumentException) { throw; }
                            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
                            prescription = GetPrescriptionDetails(id);
                            AddOrUpdatePrescription(prescription);
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;

                    case Command.AddPrescription:
                        try
                        {
                            Prescription prescription = GetPrescriptionDetails();
                            AddOrUpdatePrescription(prescription);
                            prescription.Reload();
                            Console.WriteLine(prescription);
                        }
                        catch (Exception) { }
                        break;

                    case Command.DeletePrescription:
                        try
                        {
                            DeletePrescription(ConsoleUI.GetId("Podaj Id recepty, która ma być usunięta"));
                        }
                        catch (ArgumentException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
                        catch (Exception) { }
                        break;


                    

                    case Command.AddOrder:
                        goto default;
                        break;
                    case Command.DeleteOrder:
                        goto default;
                        break;
                    case Command.AddOrderItem:
                        goto default;
                        break;
                    case Command.DeleteOrderItem:
                        goto default;
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
        /// Returns integer
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static int GetId(string prompt)
        {
            if (int.TryParse(ConsoleUI.GetString(prompt), out int id))
            {
                return id;
            }
            throw new ArgumentException("Nie rozpoznano liczby.");
        }

        /// <summary>
        /// Returns Id of existing Manufacturer or throws ArgumentException
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static int GetExistingId(string prompt)
        {
            int id;
            try
            {
                id = GetId(prompt);
                Manufacturer manufacturer = new Manufacturer(id);
                manufacturer.Reload();
            }
            catch (ArgumentException) { throw; }
            catch (Exception) { throw; }
            return id;
        }

        /// <summary>
        /// Returns decimal in return to string input from console
        /// Throws Exception on parse rerror
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static decimal? GetDecimal(string prompt)
        {
            string stringToParse = ConsoleUI.GetString(prompt);
            if (string.IsNullOrEmpty(stringToParse)) { return null; }
            bool isResult = Decimal.TryParse(stringToParse, out decimal number);
            if (isResult) { return number; }
            throw new Exception("Nie rozpoznano liczby.");
        }

        /// <summary>
        /// Returns bool in return to string input from console
        /// Throws Exception on parse rerror
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        static bool? GetBool(string prompt)
        {
            string stringToParse = ConsoleUI.GetString(prompt);
            string[] yes = { "yes", "y", "tak", "t", " true"};
            string[] no = { "no", "n", "nie", "false", "f" };
            if (string.IsNullOrEmpty(stringToParse)) { return null; }
            foreach (string item in yes)
            {
                if (item == stringToParse.ToLower().Trim()) { return true; }
            }
            foreach (string item in no)
            {
                if (item == stringToParse.ToLower().Trim()) { return false; }
            }
            throw new Exception("Nie rozpoznano odpowiedzi.");
        }

        static void PrintResultOK(bool result)
        {
            if (result) { ConsoleUI.WriteLine("Operacja wykonana poprawnie", ConsoleUI.Colors.colorSuccesss); }
        }
    }
}
