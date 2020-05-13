using System;
using System.Data.SqlClient;
using System.IO;

namespace ActiveRecord
{
    
    public abstract class ActiveRecord
    {
        private const string connectionString = "Integrated Security = SSPI; Data Source=.\\SQLEXPRESS;";
        private const string dbName = "DrugStore";
        public int ID { get; private set; }
        public abstract void Save();
        public abstract void Reload();
        public abstract void Remove();

        public static bool DatabaseExists(string dbName = dbName)
        {
            using SqlConnection connection = new SqlConnection();
            using SqlCommand command = new SqlCommand();
            connection.ConnectionString = connectionString;
            command.CommandText = "SELECT db_id(@DatabaseName)";
            command.Connection = connection;
            command.Parameters.AddWithValue("@DatabaseName", dbName);
            connection.Open();
            return command.ExecuteScalar() != DBNull.Value;
        }

        internal protected void DbConnect(string dbName = dbName)
        {
            SqlConnection connection = new SqlConnection
            {
                ConnectionString = string.Concat(connectionString, "Initial Catalog=", dbName, ";")
            };
            connection.Open();
        }

 
        //+conn open/close
    }
}
