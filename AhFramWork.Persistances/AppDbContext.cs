using AhFramWork.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AhFramWork.Persistances;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    //public DbSet<ThreadEntity> Threads{ get; set; }
    //public DbSet<ParticipantEntity> Participant { get; set; }
    //public DbSet<MessageEntity> Message { get; set; }


    public DbSet<TestEntity> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }



}
