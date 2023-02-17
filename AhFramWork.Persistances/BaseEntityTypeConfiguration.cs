using System;
using AhFramWork.Domain.Common;
using AhFramWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AhFramWork.Persistances
{

    public abstract class BaseEntityTypeConfiguration<TBase> : IEntityTypeConfiguration<TBase> where TBase : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TBase> entityTypeBuilder)
        {
            entityTypeBuilder.Property<byte[]>("RowVersion").IsRowVersion();
        }
    }
    public class UserEntityTypeConfiguration : BaseEntityTypeConfiguration<TestEntity>
    {

        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.CreatedAt).HasDefaultValueSql("getDate()");
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.LastUpdate);

            builder.ToTable($"tbl{nameof(TestEntity)}");
        }
    }
}

