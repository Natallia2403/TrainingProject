using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BookingSite.Data;
using BookingSite.Data.Models;
using BookingSite.Domain.Logic;
using BookingSite.Domain.Logic.Interfaces;
using BookingSite.Domain.Logic.Managers;
using System.Reflection;
using Microsoft.AspNetCore.Http;

namespace BookingSite.Web
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
            services.AddDomainServices();
            
            services.AddOpenApiDocument();
            services.AddControllers();
            //services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddAutoMapper(Assembly.Load("BookingSite.Domain"));

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BookingSiteConnectionString"))
                                                       .EnableSensitiveDataLogging());
            services.AddMvc();

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddScoped<IHotelManager, HotelManager>();
            services.AddScoped<ICountryManager, CountryManager>();
            services.AddScoped<IRoomManager, RoomManager>();
            services.AddScoped<IBookingManager, BookingManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //env.EnvironmentName = "Production";
            if (env.IsDevelopment())
            {
                app.UseOpenApi().UseSwaggerUi3();
            }

            // ��������� ������ HTTP
            var errorActionPath = $"/Error/HandleError";
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorActionPath);
            }

            app.UseStatusCodePagesWithReExecute(errorActionPath);

            app.UseStaticFiles();   // ��������� ��������� ����������� ������
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();    // ����������� ��������������
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}