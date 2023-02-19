using System;
using AhFramWork.Domain.AggregatesModel.CategoryAggregate;
using AhFramWork.Domain.AggregatesModel.FeatureAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhFramWork.Infrastructures.Configurations
{
    internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories", "Catalog");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
            .HasConversion(
                v => v.Value,
                v => new CategoryId(v));

            builder.Property(e => e.CategoryName)
            .HasMaxLength(128).IsRequired();

            builder.Property(e => e.Description).HasMaxLength(1024);

            builder.OwnsOne(s => s.Thumbnail, y =>
            {
                //write Value Object Configuraion
            });

            builder.OwnsMany(s => s.CategoryFeatures, t =>
            {
                t.WithOwner().HasForeignKey("CategoryId");

                t.ToTable("CategoryFeatures", "Catalog");

                t.Property(x => x.CategoryId)
                .HasConversion(
                    v => v.Value,
                    v => new CategoryId(v));

                t.Property(x => x.FeatureId)
                .HasConversion(
                    v => v.Value,
                    v => new FeatureId(v));

                t.HasKey("CategoryId", "FeatureId");

            });


        }
    }
}

