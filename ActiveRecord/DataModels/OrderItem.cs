using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ActiveRecord.DataModels
{
    public sealed class OrderItem : ActiveRecord
    {
        public int Id { get; private set; }
        public int OrderId { get; set; }
        public int MedicineId { get; set; }
        public int? PrescriptionId { get; set; }
        public int? Quantity { get; set; }
        public DateTimeOffset? DeliveredOn { get; set; }

        public OrderItem(int id) { Id = id; }
        public OrderItem() { }

        public override bool Save()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@OrderId", OrderId).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@MedicineId", MedicineId).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@PrescriptionId", PrescriptionId ?? (object)DBNull.Value).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@Quantity", Quantity ?? (object)DBNull.Value).SqlDbType = SqlDbType.Int;
            command.Parameters.AddWithValue("@DeliveredOn", DeliveredOn ?? (object)DBNull.Value).SqlDbType = SqlDbType.DateTimeOffset;
            DbConnect(connection, dbName);

            if (Id == 0)
            {
                command.CommandText = "insert into [OrderItems](OrderId, MedicineId, PrescriptionId, Quantity, DeliveredOn) " +
                    "values(@OrderId, @MedicineId, @PrescriptionId, @Quantity, @DeliveredOn); select scope_identity();";
                Id = Convert.ToInt32(command.ExecuteScalar());
                return Id > 0;
            }
            if (Id > 0)
            {
                command.CommandText = "update [OrderItems] set OrderId = @OrderId, MedicineId = @MedicineId, PrescriptionId = @PrescriptionId, " +
                    "Quantity = @Quantity, DeliveredOn = @DeliveredOn where Id = @id";
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
            command.CommandText = "select * from [OrderItems] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }

            _ = reader.Read();
            ParseReader(reader);
        }

        /// <summary>
        /// Returns list of OrderItem objects. If parameter is given (>0) then lists items associated with orderId
        /// </summary>
        /// <param name="manufacurerId"></param>
        /// <returns></returns>
        /// <exception cref="DbResultException"></exception>
        public static List<OrderItem> GetOrderItems(int orderId = 0)
        {
            List<OrderItem> orderItems = new List<OrderItem>();
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from [OrderItems]";
            if (orderId > 0) // Get items of one order
            {
                command.CommandText += " where OrderId = @OrderId;";
                command.Parameters.AddWithValue("@OrderId", orderId).SqlDbType = SqlDbType.Int;
            }
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Lista jest pusta."); }

            while (reader.Read())
            {
                OrderItem orderItem = new OrderItem();
                orderItem.ParseReader(reader);
                orderItems.Add(orderItem);
            }

            return orderItems;
        }

        private void ParseReader(SqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            OrderId = reader.GetInt32("OrderId");
            MedicineId = reader.GetInt32("MedicineId");
            if (!(reader["PrescriptionId"] is DBNull)) { PrescriptionId = reader.GetInt32("PrescriptionId"); }
            if (!(reader["Quantity"] is DBNull)) { Quantity = reader.GetInt32("Quantity"); }
            if (!(reader["DeliveredOn"] is DBNull)) { DeliveredOn = reader.GetDateTimeOffset(reader.GetOrdinal("DeliveredOn")); }
        }

        public override bool Remove()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from [OrderItems] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            int result = command.ExecuteNonQuery();

            if (result == 1) { return true; }
            else { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
        }
    }
}
