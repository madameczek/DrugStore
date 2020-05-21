using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace ActiveRecord.DataModels
{
    public sealed class Prescription : ActiveRecord
    {
        private string customerName;
        private string pesel;
        private string prescriptionNumber;

        public int Id { get; set; }
        public string CustomerName { get => customerName ?? string.Empty; set => customerName = value; }
        public string Pesel { get => pesel ?? string.Empty; set => pesel = value; }
        public string PrescriptionNumber { get => prescriptionNumber ?? string.Empty; set => prescriptionNumber = value; }

        public Prescription(int id) { Id = id; }
        public Prescription() { }

        public override bool Save()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@CustomerName", CustomerName ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@CustomerPesel", Pesel).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@PrescriptionNumber", PrescriptionNumber ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            DbConnect(connection, dbName);

            if (Id == 0)
            {
                command.CommandText = "insert into [Prescriptions](CustomerName, CustomerPesel, PrescriptionNumber)" +
                    "values(@CustomerName, @CustomerPesel, @PrescriptionNumber);" +
                    "select scope_identity();";
                Id = Convert.ToInt32(command.ExecuteScalar());
                return Id > 0;
            }
            if (Id > 0)
            {
                command.CommandText = "update [Prescriptions] set CustomerName = @CustomerName, " +
                    "CustomerPesel = @CustomerPesel, PrescriptionNumber = @PrescriptionNumber where Id = @id";
                int result = command.ExecuteNonQuery();
                if (result == 1) { return true; }
                else { throw new DbResultException($"Nie odnaleziono rekordu o Id={Id}."); }
            }
            return false;
        }

        public override void Reload()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from Prescriptions where Id = @Id";
            // dodać listowanie powiązanego zamówienia i dodanych leków na receptę
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
            _ = reader.Read();
            ParseReader(reader);
        }

        public static List<Prescription> GetPrescriptions()
        {
            List<Prescription> prescriptions = new List<Prescription>();
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from [Prescriptions]";
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Lista jest pusta."); }

            while (reader.Read())
            {
                Prescription prescription = new Prescription();
                prescription.ParseReader(reader);
                prescriptions.Add(prescription);
            }

            return prescriptions;
        }

        private void ParseReader(SqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            if (!(reader["CustomerName"] is DBNull)) { CustomerName = reader.GetString("CustomerName"); }
            if (!(reader["CustomerPesel"] is DBNull)) { Pesel = reader.GetString("CustomerPesel"); }
            if (!(reader["PrescriptionNumber"] is DBNull)) { PrescriptionNumber = reader.GetString("PrescriptionNumber"); }
        }

        public override bool Remove()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from [Prescriptions] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            int result = command.ExecuteNonQuery();

            if (result == 1) { return true; }
            else { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
        }

        public override string ToString()
        {
            const int padding = 13;
            string output = $"{"Id",padding}: {Id}\n" +
                $"{"Klient",padding}: {CustomerName}\n" +
                $"{"Pesel",padding}: {Pesel}\n" +
                $"{"Numer recepty",padding}: {PrescriptionNumber}";
            return output;
        }
    }
}
