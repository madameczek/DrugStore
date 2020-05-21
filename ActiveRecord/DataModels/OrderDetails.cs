using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ActiveRecord.DataModels
{
    public sealed class OrderDetails : ActiveRecord
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public int? Quantity { get; set; }
        public DateTimeOffset? DeliveredOn { get; set; }

        public override bool Save()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@OrderId", OrderId).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@MedicineId", MedicineId).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@Quantity", Quantity ?? (object)DBNull.Value).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@DeliveredOn", DeliveredOn ?? (object)DBNull.Value).SqlDbType = SqlDbType.DateTimeOffset;
            DbConnect(connection, dbName);

            if (Id == 0)
            {
                command.CommandText = "insert into [OrderDetails](OrderId, MedicineId, Quantity, DeliveredOn) " +
                    "values(@OrderId, @MedicineId, @Quantity, @DeliveredOn); select scope_identity();";
                Id = Convert.ToInt32(command.ExecuteScalar());
                return Id > 0;
            }
            if (Id > 0)
            {
                command.CommandText = "update [OrderDetails](OrderId, MedicineId, Quantity, DeliveredOn) " +
                    "values(@OrderId, @MedicineId, @Quantity, @DeliveredOn) where Id = @id";
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
            command.CommandText = "select * from [OrderDetails] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }

            _ = reader.Read();
            ParseReader(reader);
        }

        private void ParseReader(SqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            OrderId = reader.GetInt32("OrderId");
            MedicineId = reader.GetInt32("MedicineId");
            if (!(reader["Quantity"] is DBNull)) { Quantity = reader.GetInt32("Quantity"); }
            if (!(reader["DeliveredOn"] is DBNull)) { DeliveredOn = reader.GetDateTimeOffset(reader.GetOrdinal("DeliveredOn")); }
        }

        public override bool Remove()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from [OrderDetails] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            int result = command.ExecuteNonQuery();

            if (result == 1) { return true; }
            else { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
        }
    }
}
