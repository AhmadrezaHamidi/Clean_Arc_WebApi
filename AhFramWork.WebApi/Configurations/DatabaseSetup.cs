using System;
using AhFramWork.Application.IServices;
using AhFramWork.Application.Services;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using AhFramWork.Infrastructures.Context;
using AhFramWork.Persistances.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AhFramWork.WebApi.Configurations
{
    public static class DatabaseSetup
    {
        public static void AddDatabaseSetup(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            string connString = configuration.GetConnectionString("CatalogDbConnection");

            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(connString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure();
                });
            });
        }
    }



    public static class MainSetup
    {
        public static void RegisterDI(this IServiceCollection services, IConfiguration configuration)
        {
            DatabaseSetup.AddDatabaseSetup(services, configuration);
            RepositoriesSetup.AddRepositorySetup(services);
            ServicesSetup.AddServicesSetup(services);
        }
    }
    public static class RepositoriesSetup
    {
        public static void AddRepositorySetup(this IServiceCollection services)
        {
            services.AddScoped<IFeatureRepository, FeatureRepositoy>();
        }
    }
    public static class ServicesSetup
    {
        public static void AddServicesSetup(this IServiceCollection services)
        {
            services.AddScoped<IFeatureService, FeatureServie>();
        }
    }
}

