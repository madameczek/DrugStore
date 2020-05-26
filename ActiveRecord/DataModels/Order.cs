using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace ActiveRecord.DataModels
{
    public sealed class Order : ActiveRecord
    {
        public int Id { get; private set; }
        public DateTimeOffset CreatedOn { get; private set; }
        public bool IsOpen { get; private set; }


        public Order(int id) { Id = id; }
        public Order() { }

        public override bool Save()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.Parameters.AddWithValue("@CreatedOn", DateTimeOffset.Now).SqlDbType = SqlDbType.DateTimeOffset;
            DbConnect(connection, dbName);
            command.CommandText = "insert into Orders(CreatedOn) values(@CreatedOn); select scope_identity();";
            Id = Convert.ToInt32(command.ExecuteScalar());
            return Id > 0;
        }

        public override void Reload()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select * from [Orders] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();
            if (!reader.HasRows) { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
            _ = reader.Read();
            ParseReader(reader);
        }

        /// <summary>
        /// Returns list of orders with at least one item.
        /// If isOpen is set true, returns orders with at least one not delivered medicine
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        /// <exception cref="DbResultException"></exception>
        public static List<Order> GetOrders(bool isOpen)
        {
            List<Order> orders = new List<Order>();
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "select Orders.Id, Orders.CreatedOn from Orders " +
                "join OrderItems on OrderItems.OrderId = Orders.Id";
            if (isOpen) // Get open orders
            {
                command.CommandText += " where OrderItems.Quantity >0";
            }
            command.CommandText += " group by Orders.Id, Orders.CreatedOn";
            DbConnect(connection, dbName);
            SqlDataReader reader = command.ExecuteReader();

            if (!reader.HasRows) { throw new DbResultException($"Lista jest pusta."); }

            while (reader.Read())
            {
                Order order = new Order();
                order.ParseReader(reader);
                orders.Add(order);
            }

            return orders;
        }

        private void ParseReader(SqlDataReader reader)
        {
            Id = reader.GetInt32("Id");
            CreatedOn = reader.GetDateTimeOffset(reader.GetOrdinal("CreatedOn"));
        }

        public override bool Remove()
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "delete from [Orders] where Id = @Id";
            command.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.Int;
            DbConnect(connection, dbName);
            int result = command.ExecuteNonQuery();

            if (result == 1) { return true; }
            else { throw new DbResultException($"Nie odnaleziono rekordu o id={Id}."); }
        }

        public override string ToString()
        {
            return $"Id: {Id,4}, utworzone: {CreatedOn:yyyy-MM-dd}";
        }
    }
}
