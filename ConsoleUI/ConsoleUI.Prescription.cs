using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    /// <summary>
    /// Methods used by PrescriptionMenu
    /// </summary>
    internal static partial class ConsoleUI
    {
        private static Prescription GetPrescriptionDetails(int id = 0) // 0 to insert new record
        {
            Prescription prescription = new Prescription(id);
            prescription.CustomerName = GetNotEmptyString("Imię i nazwisko klienta");
            string pesel;
            do
            {
                pesel = GetNotEmptyString("Podaj PESEL");
            } while (!PeselChecker.Verify(pesel));
            prescription.Pesel = pesel;
            prescription.PrescriptionNumber = GetNotEmptyString("Podaj numer recepty");
            return prescription;
        }

        private static void AddOrUpdatePrescription(Prescription prescription)
        {
            try
            {
                PrintResultOK(prescription.Save());
            }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
        }

        private static Prescription ReloadPrescription(int id)
        {
            Prescription prescription = new Prescription(id);
            try
            {
                prescription.Reload();
            }
            catch (ArgumentException) { throw; }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception) { ConsoleUI.WriteLine("Nieznany błąd", ConsoleUI.Colors.colorError); throw; }
            return prescription;
        }

        private static void PrintPrescriptions()
        {
            List<Prescription> prescriptions;
            try
            {
                prescriptions = Prescription.GetPrescriptions();
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }

            int paddingName = prescriptions.Max(m => m.CustomerName.Length);
            int paddingPrescriptionNumber = prescriptions.Max(m => m.PrescriptionNumber.Length);
            paddingName = Math.Max(paddingName, "Klient".Length);
            paddingPrescriptionNumber = Math.Max(paddingPrescriptionNumber, "Numer recepty".Length);

            ConsoleUI.WriteLine(new string('-', paddingName + paddingPrescriptionNumber + 18), ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.Write($"{"Id".PadLeft(4)}|{"Klient".PadRight(paddingName)}|{"PESEL".PadRight(11)}|" +
                $"{"Numer recepty".PadRight(paddingPrescriptionNumber)}\n", ConsoleUI.Colors.colorTitleBar);
            ConsoleUI.WriteLine(new string('-', paddingName + paddingPrescriptionNumber + 18), ConsoleUI.Colors.colorTitleBar);

            foreach (Prescription prescription in prescriptions)
            {
                Console.Write(prescription.Id.ToString().PadLeft(4));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(prescription.CustomerName.ToString().PadRight(paddingName));
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(prescription.Pesel);
                ConsoleUI.Write("|", ConsoleUI.Colors.colorTitleBar);
                Console.Write(prescription.PrescriptionNumber.ToString().PadRight(paddingPrescriptionNumber));
                Console.WriteLine();
            }
            ConsoleUI.WriteLine(new string('-', paddingName + paddingPrescriptionNumber + 18), ConsoleUI.Colors.colorTitleBar);
        }

        private static void DeletePrescription(int id)
        {
            try
            {
                PrintResultOK(new Prescription(id).Remove());
            }
            catch (ArgumentException) { throw; }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); }
            catch (Exception e)
            {
                if (e.Message.Contains("FK_OrderDetails_Prescriptions"))
                {
                    ConsoleUI.WriteLine("Nie można usunąć recepty, bo istnieją w bazie powiązane zamówienia z lekami z tej recepty", ConsoleUI.Colors.colorError);
                }
                else { ConsoleUI.WriteLine(e.Message + "Wystąpił nieznany błąd", ConsoleUI.Colors.colorError); }
            }
        }
    }
}
