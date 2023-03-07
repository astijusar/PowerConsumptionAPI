using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PowerConsumptionAPI.Models;

namespace PowerConsumptionAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var _connectionString = configuration.GetConnectionString("sqlConnection");

            services.AddDbContext<RepositoryContext>(
                options => options
                    .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
            );
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequiredLength = 8;
                o.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<RepositoryContext>()
                .AddDefaultTokenProviders();
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader());
            });
        }

        public static void ConfigureIIS(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => 
            { 
            
            });
        }
    }
}
