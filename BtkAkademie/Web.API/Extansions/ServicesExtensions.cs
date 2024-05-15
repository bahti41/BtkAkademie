using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
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
            options.UseSqlServer(configuration.GetConnectionString("SqlConncetion")));
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


        public static void ConfigureActionfilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
            services.AddSingleton<LogFilterAttribute>();
            services.AddScoped<ValidateMediaTypeAttribute>();
        }


        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination")
                );
            });
        }


        public static void ConfigureDataShaper(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<BookDTO>, DataShaper<BookDTO>>();
        }


        public static void AddCustomMediaTypes(this IServiceCollection services)
        {
            services.Configure<MvcOptions>(option =>
            {
                var systemTextJsonOutputFormatter = option
                .OutputFormatters
                .OfType<SystemTextJsonOutputFormatter>()?.FirstOrDefault();

                if (systemTextJsonOutputFormatter is not null)
                {
                    systemTextJsonOutputFormatter.SupportedMediaTypes.Add("application/vnd.sonmez.hateoes+json");
                }

                var xmlOutputFormatter = option
                .OutputFormatters
                .OfType<XmlDataContractSerializerOutputFormatter>().FirstOrDefault();

                if(xmlOutputFormatter is not null)
                {
                    xmlOutputFormatter.SupportedMediaTypes.Add("application/vnd.sonmez.hateoes+xml");
                }
            });
        }
    }
}
