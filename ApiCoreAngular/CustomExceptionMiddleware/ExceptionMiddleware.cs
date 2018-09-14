using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiCoreAngular.CustomExceptionMiddleware
{
   

    public  class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;
        //private readonly ILoggerManager _logger;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                // logger
                await HandlerExceptionAsync(httpContext, ex);
                
            }

        }

        private static Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(new ErrorDetalles() {
                EstatusCode = context.Response.StatusCode,
                Mensaje = "Internal Server Error from the custom middleware"
        
            }.ToString());

        }

      
    }
}
