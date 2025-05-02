using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Database;
using TodoList.Database.Exceptions;
using TodoList.Entities;
using TodoList.Entities.Enums;
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
            Task task = null;

            string query = "SELECT * FROM tb_tasks WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        task = InstantiateTask(reader);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Erro ao buscar tarefas");
                }
                finally
                {
                    DatabaseConnection.CloseConnection(_conn);
                }
                return task;
            }
        }

        public List<Task> FindAll()
        {
            List<Task> tasks = new List<Task>();

            string query = "SELECT * FROM tb_tasks";

            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                try
                {
                    _conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        tasks.Add(InstantiateTask(reader));
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Erro ao buscar tarefas");
                }
                finally
                {
                    DatabaseConnection.CloseConnection(_conn);
                }
                return tasks;
            }
        }


        public bool ExistsById(int id)
        {
            string query = "SELECT COUNT(*) FROM tb_tasks WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(query, _conn))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _conn.Open();

                    int count = (int) cmd.ExecuteScalar();

                    return count > 0;
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Erro ao buscar tarefas");
                }
                finally
                {
                    DatabaseConnection.CloseConnection(_conn);
                }
            }
        }

        public void Update(Task entity, int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task InstantiateTask(SqlDataReader reader)
        {
            return new(
                reader.GetInt16(0),
                reader.GetString(1),
                reader.GetString(2),
                (Importance)reader.GetInt16(3),
                reader.GetDateTime(4), (Status)
                reader.GetInt16(5));
        }
    }
}
