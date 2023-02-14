using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AhFramWork.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<TestEntity> TestEntity { get; set; }
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<TestEntity>().HasData(
                new TestEntity() { Id = Guid.NewGuid(), Name = "Pen", Value = 10, Quantity = 100 },
                new TestEntity() { Id = Guid.NewGuid(), Name = "Paper A4", Value = 1, Quantity = 500 },
                new TestEntity() { Id = Guid.NewGuid(), Name = "Book", Value = 40, Quantity = 400 });

            base.OnModelCreating(modelBuilder);
        }
    }
}
