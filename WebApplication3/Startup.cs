using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
using WebApplication3.Middleware;
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
			services.AddSingleton<IDbService, MockDbService>();
			services.AddTransient<IStudentDbService, SqlServerStudentDbService>(); services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IStudentDbService service)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseMiddleware<LoggingMiddleware>();

			app.Use(async (context, next) => {
				if (!context.Request.Headers.ContainsKey("Index"))
				{
					context.Response.StatusCode = StatusCodes.Status401Unauthorized;
					await context.Response.WriteAsync("Brak indeksu");
					return;
				}

				string index = context.Request.Headers["Index"].ToString();
				//check in db
				string connString = "Data Source=db-mssql;Initial Catalog=s18891;Integrated Security=True";

				using (var connection = new SqlConnection(connString))
				using (var command = new SqlCommand())
				{
					command.Connection = connection;
					connection.Open();

					//Check if student exists
					command.CommandText = "select * from Student where IndexNumber=" + index;

					var dr = command.ExecuteReader();
					if (!dr.Read())
					{
						await context.Response.WriteAsync("Student o podanym indeksie nie istnieje");
						return;
					}
				}


				await next();
			});

			///
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
