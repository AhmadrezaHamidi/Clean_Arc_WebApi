using System;
using AhFramWork.Domain.AggregatesModel.CategoryAggregate;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using AhFramWork.Domain.AggregatesModel.ProductAggregate;
using AhFramWork.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace AhFramWork.Infrastructures.Context
{
    public class CatalogContext : DbContext, IUnitOfWork
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Product> Products { get; set; }

        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogContext).Assembly);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}

