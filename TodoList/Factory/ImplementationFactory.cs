using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Database;
using TodoList.Repository;
using TodoList.Repository.Implementation;
using Task = TodoList.Entities.Task;

namespace TodoList.Factory
{
    internal class ImplementationFactory
    {
        public static ICrudRepository<Task> CreateTaskRepository()
        {
            return new TaskRepository(DatabaseConnection.GetConnection());
        }
    }
}
