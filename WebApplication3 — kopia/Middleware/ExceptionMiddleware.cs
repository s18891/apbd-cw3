using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exc)
            {
                await HandleExceptionAsync(context, exc);

            }


        }

        private Task HandleExceptionAsync(HttpContext context, Exception exc)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if(exc is StudentCannotDefendException)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                return context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = exc.Message


                }.ToString());

            }

            return context.Response.WriteAsync(new ErrorDetails
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Wystąpił jakis błąd..."


            }.ToString());
        }
    }
}
