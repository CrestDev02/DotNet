using Microsoft.EntityFrameworkCore;
using UserManagement.Database.Entity.DataAccess.Context;
using UserManagement.Database.Entity.DataAccess.DTOs;
using UserManagement.Database.Entity.IRepositories;
using UserManagement.Database.Entity.Repositories;
using UserManagement.Web.BusinessLogic.Interfaces;
using UserManagement.Web.BusinessLogic.Service;

namespace UserManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureServices(builder.Services, builder.Configuration);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=User}/{action=Index}");

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            ConfigureDI(services);

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("UmConnString"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null);
                }
                );
            }, ServiceLifetime.Transient);

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(10);
            });
        }

        private static void ConfigureDI(IServiceCollection services)
        {
            #region Services

            services.AddTransient<IUserService, UserService>();

            #endregion

            #region Repositories

            services.AddTransient<IADORepository, ADORepository>();
            services.AddTransient<IExceptionRepository, ExceptionRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            #endregion
        }
    }
}

