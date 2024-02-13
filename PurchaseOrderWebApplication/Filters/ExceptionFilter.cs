using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PurchaseOrderWebApplication.Models;
namespace PurchaseOrderWebApplication.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorViewModel
            (
                404,
                context.Exception.Message,
                context.Exception.StackTrace?.ToString()
            );

            context.Result = new JsonResult(error);
            Console.WriteLine(error.ToString());
        }
    }
    }
