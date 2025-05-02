using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
