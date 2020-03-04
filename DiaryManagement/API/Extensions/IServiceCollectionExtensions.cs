using API.Filters;
using AutoMapper;
using Core.Data;
using Core.Services;
using Infrastructure.Common;
using Infrastructure.Data;
using Infrastructure.DataServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace API.Extensions
{
    /// <summary>
    /// IServiceCollection Extensions
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Register Services
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register Filters globally
            services.RegisterFilters();

            // Register automapper
            services.RegisterAutomapper();

            // Register Entity Framework
            services.RegisterEntityFramework(configuration);

            // Dependency resolver. Add your dependecies in this method
            services.DependencyResolver();

            // Register Swagger
            services.RegisterSwagger();
        }

        /// <summary>
        /// Extension method to register filters globally
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterFilters(this IServiceCollection services)
        {
            // Register Exception filter
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ExceptionFilter));
            });

            // Register Model validation filter
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(ModelValidationFilter));
            });
        }

        /// <summary>
        /// Extension method to register automapper
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterAutomapper(this IServiceCollection services)
        {
            // Add automapper profile
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperProfiles());
            });

            // Add mapper as service
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        /// <summary>
        /// Extension method to register entity framework
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void RegisterEntityFramework(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString), ServiceLifetime.Singleton);
        }

        /// <summary>
        /// Extension method to resolve dependencies
        /// </summary>
        /// <param name="services"></param>
        public static void DependencyResolver(this IServiceCollection services)
        {
            services.AddTransient<IDiaryService, DiaryService>();
            services.AddTransient(serviceProvider => new Lazy<IDiaryService>(() => serviceProvider.GetRequiredService<IDiaryService>()));
            services.AddTransient<IDiaryDataService, DiaryDataService>();
        }

        /// <summary>
        /// Extension method to register swagger
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Sample APIs",
                    Description = "ASP.NET Core 2.1 Web API",
                    Contact = new OpenApiContact()
                    {
                        Name = "your name",
                        Email = "your email id",
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
        }
    }
}
