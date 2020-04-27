using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication3.Services;

namespace WebApplication3
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//AddSingleton, AddTransient, AddScoped;
			services.AddScoped<IStudentsDal, SqlServerDbDal>();
			services.AddTransient<IDbService, MockDbService>();
			services.AddTransient<IStudentDbServeice, SqlServerStudentDbService>();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStudentDbServeice service)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseWhen(context => context.Request.Path.ToString().Contains("secret"), app =>
			{

				app.Use(async (context, next) =>
				{
					if (!context.Request.Headers.ContainsKey("IndexNumber"))
					{
						context.Response.StatusCode = StatusCodes.Status401Unauthorized;
						await context.Response.WriteAsync("Musisz podac numer indexu");
						return;
					}
					string index = context.Request.Headers["IndexNumber"].ToString();
					//.... exists in db
					var stud = service.GetStudent(index);
					if (stud == null)
					{
						
						context.Response.StatusCode = StatusCodes.Status404NotFound;
						await context.Response.WriteAsync("Student not found");
						return;
						
					}

					await next();
				});
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
