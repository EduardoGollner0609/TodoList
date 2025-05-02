using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Database;
using TodoList.Database.Exceptions;
using Task = TodoList.Entities.Task;

namespace TodoList.Repository.Implementation
{
    internal class TaskRepository : ICrudRepository<Task>
    {
        private SqlConnection _conn;

        public TaskRepository(SqlConnection conn)
        {
            _conn = conn;
        }

        public void Insert(Task entity)
        {
            string query = "INSERT INTO tb_tasks(title, description, importance, date, status) " +
                "VALUES (@title, @description, @importance, @date, @status)";

            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@title", entity.Title);
                cmd.Parameters.AddWithValue("@description", entity.Description);
                cmd.Parameters.AddWithValue("@importance", (int)entity.Importance);
                cmd.Parameters.AddWithValue("@date", entity.Date);
                cmd.Parameters.AddWithValue("@status", (int)entity.Status);

                try
                {
                    _conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected == 0)
                    {
                        throw new DatabaseException("Erro ao inserir tarefa! Nenhuma linha foi afetada!");
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Erro ao inserir tarefa: " + e.Message);
                }
                finally
                {
                    DatabaseConnection.CloseConnection(_conn);
                }
            }
        }

        public Task FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Task> FindAll()
        {
            throw new NotImplementedException();
        }


        public bool ExistsById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Task entity, int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
