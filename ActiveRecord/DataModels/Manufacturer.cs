using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ActiveRecord.DataModels
{
    public sealed class Manufacturer : ActiveRecord
    {
        private string name;
        private string address;
        private string city;
        private string country;
        

        public int Id { get; private set; }
        public string Name { get => name ?? string.Empty; set => name = value; }
        public string Address { get => address ?? string.Empty; set => address = value; }
        public string City { get => city ?? string.Empty; set => city = value; }
        public string Country { get => country ?? string.Empty; set => country = value; }

        public Manufacturer(int id) { Id = id; }
        public Manufacturer() { }

        public override bool Save()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@Name", name ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@Address", address ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@City", city ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            command.Parameters.AddWithValue("@Country", country ?? (object)DBNull.Value).SqlDbType = SqlDbType.NVarChar;
            DbConnect(connection, dbName);

            if (Id == 0)
            {
                command.CommandText = "insert into [Manufacturers](Name, Address, City, Country)" +
                    "values(@Name, @Address, @City, @Country);" +
                    "select scope_identity();";
                Id = Convert.ToInt32(command.ExecuteScalar());
                return Id > 0;
            }
            if (Id > 0)
            {
                command.CommandText = "update [Manufacturers] set Name = @Name, " +
                    "Address = @Address, City = @City, Country = @Country " +
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
            command.CommandText = "select * from [Manufacturers] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();
            
            if (!reader.HasRows) { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
            
            _ = reader.Read();
            ParseReader(reader);
        }

        public static List<Manufacturer> GetManufacturers()
        {
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from [Manufacturers]";
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Lista jest pusta."); }

            while (reader.Read())
            {
                Manufacturer manufacturer = new Manufacturer();
                manufacturer.ParseReader(reader);
                manufacturers.Add(manufacturer);
            }

            return manufacturers;
        }

        private void ParseReader(SqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            if (!(reader["Name"] is DBNull)) { Name = reader["Name"].ToString(); }
            if (!(reader["Address"] is DBNull)) { Address = reader["Address"].ToString(); }
            if (!(reader["City"] is DBNull)) { City = reader["City"].ToString(); }
            if (!(reader["Country"] is DBNull)) { Country = reader["Country"].ToString(); }
        }

        public override bool Remove()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from [Manufacturers] where Id = @Id";
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
                $"{"Adres",padding}: {Address}\n" +
                $"{"Miasto",padding}: {City}\n"+
                $"{"Kraj",padding}: {Country}";
            return output;
        }
    }
}
