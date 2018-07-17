using AcmeFlightsApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace AcmeFlightsApi
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
            services.AddDbContext<APIContext>(option => option.UseInMemoryDatabase("APIDB"));
            services.AddMvc();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Title = "Flights API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();
            // Add swagger UI
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Flights API V1");
            });
            app.UseMvc();
            SeedData(app);

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("MVC did not find any route.");
            });
        }

        public void SeedData(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<APIContext>();
                // Seed the database.
                context.FlightsItems.Add(new Model.Flights
                {
                    FlightId = 101,
                    AirlineName = "Quantas",
                    TotalSeats = 6,
                    FromLocation = "Brisbane",
                    ToLocation = "Gold Coast",
                    DepartureTime = "8.00.00 AM",
                    ArivalTime = "9.00.00 AM",
                    Duration = "1 Hour"
                });

                context.FlightsScheduleItems.Add(new Model.FlightsSchedule {
                    ScheduleId = 1001,
                    AvailableSeats = 6,
                    DepartureDate = DateTime.Today,
                    Price = "100 $",
                    FlightId = 101
                });
               
                context.SaveChanges();
            }
        }

    }
}
