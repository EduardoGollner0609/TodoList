using Microsoft.Data.SqlClient;
using System.Data;
using TodoList.Database.Exceptions;

namespace TodoList.Database
{
    internal class DatabaseConnection
    {
        private const string connectionString =
        "Server=EDUARDO;Database=todolist;Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            try
            {
                SqlConnection conn = new(connectionString);
                return conn;
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Erro: " + e.Message);
            }
        }

        public static void CloseConnection(SqlConnection conn)
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
            catch (SqlException e)
            {
                throw new DatabaseException("Erro: " + e.Message);
            }
        }
    }
}
