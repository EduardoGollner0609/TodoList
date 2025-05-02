using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Repository
{
    internal interface ICrudRepository <T>
    {
        // Create
        void Insert(T entity);

        // Read
        T FindById(int id);
        List<T> FindAll();

        // Update
        void Update(T entity, int id);

        //Delete
        void DleeteById(int id);
    }
}
