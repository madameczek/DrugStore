using System;
using System.Collections.Generic;
using System.Text;
using ActiveRecord.DataModels;

namespace ConsoleUI
{
    /// <summary>
    /// Methods used by PrescriptionMenu
    /// </summary>
    internal partial class ConsoleUI
    {
        private Prescription GetPrescriptionDetails(int id = 0) // 0 to insert new record
        {
            Prescription prescription = new Prescription(id);
            try
            {
                prescription.CustomerName = GetNotEmptyString("Imię i nazwisko klienta");
                prescription.Pesel = GetNotEmptyString("Podaj PESEL");  // dodać kontrolę pesel
                prescription.PrescriptionNumber = GetNotEmptyString("Podaj numer recepty");
            }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            return prescription;
        }

        private void AddOrUpdatePrescription(Prescription prescription)
        {
            try
            {
                PrintResultOK(prescription.Save());
            }
            catch (DbResultErrorException e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
            catch (Exception e) { ConsoleUI.WriteLine(e.Message, ConsoleUI.Colors.colorError); throw; }
        }

        private Prescription ReloadPrescription(int id)
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

        private void PrintPrescriptions(int manufacurerId = 0) { }

        private void DeletePrescription(int id)
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
