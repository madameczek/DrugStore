using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ActiveRecord.DataModels
{
    public sealed class Medicine : ActiveRecord
    {
        private string name;
        private string manufacturer;

        public int Id { get; set; }
        public string Name { get => name ?? string.Empty; set => name = value; }
        public int? ManufacturerId { get; set; }
        public decimal? Price { get; set; }
        public int? StockQty { get; set; }
        public bool? IsPrescription { get; set; }
        public string Manufacturer { get => manufacturer ?? string.Empty; private set => manufacturer = value; }

        public Medicine(int id) { Id = id; }
        public Medicine() { }

        public override bool Save()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@Name", Name).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@ManufacturerId", ManufacturerId).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@Price", Price ?? (object)DBNull.Value).SqlDbType = SqlDbType.Decimal;
            command.Parameters.AddWithValue("@IsPrescription", IsPrescription ?? (object)DBNull.Value).SqlDbType = SqlDbType.Bit;
            command.Parameters.AddWithValue("@StockQty", StockQty ?? (object)DBNull.Value).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);

            if (Id == 0)
            {
                command.CommandText = "insert into [Medicines](Name, ManufacturerId, Price, StockQty, IsPrescription)" +
                    "values(@Name, @ManufacturerId, @Price, @StockQty, @IsPrescription);" +
                    "select scope_identity();";
                Id = Convert.ToInt32(command.ExecuteScalar());
                return Id > 0;
            }
            if (Id > 0)
            {
                command.CommandText = "update [Medicines] set Name = @Name, " +
                    "ManufacturerId = @ManufacturerId, Price = @Price, StockQty = @StockQty, IsPrescription = @IsPrescription " +
                    "where Id = @id";
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
            command.CommandText = "select m.Id, m.Name, m.ManufacturerId, m.Price, m.StockQty, m.IsPrescription, Manufacturers.Name as Manufacturer from [Medicines] as m " +
                    "join manufacturers on m.ManufacturerId = Manufacturers.id where m.Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
            _ = reader.Read();
            ParseReader(reader);
        }

        public static List<Medicine> GetMedicines(int manufacurerId)
        {
            List<Medicine> medicines = new List<Medicine>();
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select m.Id, m.Name, m.ManufacturerId, m.Price, m.StockQty, m.IsPrescription, Manufacturers.Name as Manufacturer " +
                "from [Medicines] as m join Manufacturers on m.ManufacturerId = Manufacturers.id";
            if (manufacurerId > 0) // Get medicines suppplied by one manufacturer
            {
                command.CommandText += " where m.ManufacturersId = @ManufacturersId;";
                command.Parameters.AddWithValue("@ManufacturersId", manufacurerId).SqlDbType = SqlDbType.Int;
            }
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Lista jest pusta."); }

            while (reader.Read())
            {
                Medicine medicine = new Medicine();
                medicine.ParseReader(reader);
                medicines.Add(medicine);
            }

            return medicines;
        }

        private void ParseReader(SqlDataReader reader)
        { 
            Id = reader.GetInt32("Id");
            if (!(reader["Name"] is DBNull)) { Name = reader.GetString("Name"); }
            if (!(reader["ManufacturerId"] is DBNull)) { ManufacturerId = reader.GetInt32("ManufacturerId"); }
            if (!(reader["Price"] is DBNull)) { Price = reader.GetDecimal("Price"); }
            if (!(reader["StockQty"] is DBNull)) { StockQty = reader.GetInt32("StockQty"); }
            if (!(reader["IsPrescription"] is DBNull)) { IsPrescription = reader.GetBoolean("IsPrescription"); }
            if (!(reader["Manufacturer"] is DBNull)) { Manufacturer = reader.GetString("Manufacturer"); }
        }

        public override bool Remove()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from [Medicines] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            int result = command.ExecuteNonQuery();

            if (result == 1) { return true; }
            else { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
        }

        public override string ToString()
        {
            const int padding = 8;
            string output = $"{"Id",padding}: {Id}\n" +
                $"{"Nazwa",padding}: {Name}\n" +
                $"{"Dostawca",padding}: {Manufacturer}\n" +
                $"{"Cena",padding}: {(Price != null ? ((decimal)Price).ToString("#.00") : "Brak danych")}\n" +
                $"{"Magazyn",padding}: {StockQty}{(StockQty != null ? " szt.": "Brak danych")}\n" +
                $"{"Recepta",padding}: {(IsPrescription != null ? ((bool)IsPrescription ? "TAK":"Nie") : "Brak danych")}";
            return output;
        }
    }
}
