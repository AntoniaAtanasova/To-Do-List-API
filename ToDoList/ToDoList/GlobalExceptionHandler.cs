using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList.BLL.Exceptions;

namespace ToDoList.Web
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                string result;

                try
                {
                    var ex = (ToDoListException)error;
                    response.StatusCode = ex.StatusCode.Value;
                    response.ContentType = "application/json";

                    result = JsonSerializer.Serialize
                        (new
                        {
                            ErrorMessage = ex.Message,
                            ExceptionType = error.GetType().Name.ToString()
                        });

                    await response.WriteAsync(result);
                }
                catch (InvalidCastException)
                {
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    response.ContentType = "application/json";
                    result = JsonSerializer.Serialize
                    (new
                    {
                        ErrorMessage = "There was an exception while trying to process your requests",
                        ExceptionType = error.GetType().Name.ToString()
                    });

                    await response.WriteAsync(result);
                }
            }
        }
    }
}
