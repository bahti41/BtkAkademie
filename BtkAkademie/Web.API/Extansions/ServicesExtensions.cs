using Microsoft.EntityFrameworkCore;
using Repositories.Contracts;
using Repositories.EfCore;
using Services;
using Services.Contracts;

namespace Web.API.Extansions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection")));

        }

        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static void ConfigurServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }


        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerService, LoggerManager>();
        }
    }
}
