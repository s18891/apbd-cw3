﻿using System;
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

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            if (httpContext.Request != null)
            {
                string httpMethod = httpContext.Request.Method;     
                string path = httpContext.Request.Path;         //api/students             
                string queryString = httpContext.Request.QueryString.ToString();
                string text = "";

                using (var reader = new StreamReader(httpContext.Request.Body,
                    Encoding.UTF8, true, 1024, true))
                {
                    text = await reader.ReadToEndAsync();
                    httpContext.Request.Body.Position = 0;
                }
                string[] lines = { path, httpMethod, queryString, text };
                System.IO.File.AppendAllLines("requestsLog.txt", lines);
            }
            if (_next != null) await _next(httpContext);
        }
    }
}
