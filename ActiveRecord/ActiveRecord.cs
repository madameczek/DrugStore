using System;
using System.Data.SqlClient;

namespace ActiveRecord
{
    public abstract class ActiveRecord
    {
        public static string dbName = "DrugStore";
        private const string connectionString = "Integrated Security = SSPI; Data Source=.\\SQLEXPRESS;";
        public abstract bool Save();
        public abstract void Reload();
        public abstract void ParseReader(SqlDataReader reader);
        public abstract bool Remove();

        public static bool DatabaseExists(string dbName)
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

        internal static void DbConnect(SqlConnection connection, string dbName)
        {
            connection.ConnectionString = string.Concat(connectionString, "Initial Catalog=", dbName, ";");
            connection.Open();
        }
    }
}
