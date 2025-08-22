using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using System.Reflection;

namespace SimpleProject.DependencyInjections
{
    public static class GeneralRegisterationDependencyInjection
    {
        public static IServiceCollection AddGeneralDependencyInjection(this IServiceCollection services,IConfiguration configuration)
        {

            //coonect to the database
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("dbcontext")));
            services.AddControllersWithViews();


            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IOTimeout = TimeSpan.FromMinutes(5);
                options.IdleTimeout = TimeSpan.FromMinutes(5);
                options.Cookie.Path = "/";
                options.Cookie.Name = "_SimpleProject";
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true; // Make the session cookie essential
            });
            return services;
        }
    }
}
