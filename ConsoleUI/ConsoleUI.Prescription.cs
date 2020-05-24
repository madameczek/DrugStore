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
                pesel = GetNotEmptyString("Podaj PESEL (może być 11 zer)");
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
            catch (Exception) { throw; }
        }

        private static Prescription ReloadPrescription(int id)
        {
            Prescription prescription = new Prescription(id);
            try
            {
                prescription.Reload();
            }
            catch (DbResultException) { throw; }
            catch (Exception) { throw new Exception("Nieznany błąd"); }
            return prescription;
        }

        private static void PrintPrescriptions()
        {
            List<Prescription> prescriptions;
            try
            {
                prescriptions = Prescription.GetPrescriptions();
            }
            catch (Exception) { throw; }

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
            catch (DbResultException) { throw; }
            catch (Exception e)
            {
                if (e.Message.Contains("FK_OrderDetails_Prescriptions"))
                {
                    throw new Exception("Nie można usunąć recepty, bo istnieją w bazie zamówienia na leki z tej recepty. Najpierw usuń leki z zamówienia");
                }
                else
                {
                    throw new Exception("Wystąpił błąd: " + e.Message);
                }
            }
        }
    }
}
