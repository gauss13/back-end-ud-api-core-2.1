﻿using ApiCoreAngular.CustomExceptionMiddleware;
using Entities.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ApiCoreAngular.Extensions
{
    public static class ExepcionesMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
               {
                   context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                   context.Response.ContentType = "application/json";

                   var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                   if (contextFeature != null)
                   {
                       await context.Response.WriteAsync(new ErrorDetalles()
                       {
                           EstatusCode = context.Response.StatusCode,
                           Mensaje = "Internal Server Error"
                       }.ToString());
                   }
               }
                    );

            });
        }

        public static void ConfigureCustomException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
