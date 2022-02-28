using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Common;

namespace ToDoList.BLL.Exceptions
{
    public class ToDoListException : Exception
    {
        public int? StatusCode { get; set; }

        public ToDoListException(string message, int statusCode = Constants.InternalServerError) : base(message)
        {
            StatusCode = statusCode;
        }

        public ToDoListException(string value, string message, int statusCode) : base(String.Format(message, value))
        {
            StatusCode = statusCode;
        }

        public ToDoListException(int value, string message, int statusCode) : base(String.Format(message, value))
        {
            StatusCode = statusCode;
        }
    }
}
