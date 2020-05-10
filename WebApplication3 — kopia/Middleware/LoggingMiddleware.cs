using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using WebApplication3.Services;

namespace WebApplication3.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task InvokeAsync(HttpContext context, IStudentDbServeice serveice)
        {
            context.Request.EnableBuffering();
            if(context.Request != null)
            {
                string path = context.Request.Path; //api students
                string method = context.Request.Method;//GET POST
                string queryString = context.Request.QueryString.ToString();
                string bodyStr = "";
                using(var reader=new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    bodyStr = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;
                }
                string[] lines = { path, method, queryString, bodyStr };
                File.AppendAllLines("requestsLog.txt", lines);


            }



            if (_next!=null) await _next(context);
        }
    }
}
