﻿using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repositories.Contrats;
using Repositories.EFCore;
using Services;
using Services.Contrats;

namespace WebApi.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,
            IConfiguration configuration) => services.AddDbContext<RepositoryContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("sqlConnection")));



        public static void ConfigureRepositoryManeger(this IServiceCollection services) =>
            services.AddScoped<IRepositoryManager, RepositoryManager>();


        public static void ConfigureServiceManager(this IServiceCollection services) =>
            services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureLoggerService(this IServiceCollection services)=>
            services.AddSingleton<ILogerService,LoggerManager>();

        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidatitonFilterAttribute>();//IoC
            services.AddSingleton<LogFilterAttribute>();
        }

    }
}
