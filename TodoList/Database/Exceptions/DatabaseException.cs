using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Database.Exceptions
{
    internal class DatabaseException : ApplicationException
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }
    }
}
