
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AhFramWork.Persistances
{
    public class AppDbContextDesignTimeFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        const string SqliteDbConnection = "Data Source=EFCorePractice.db;Cache=Shared";
        const string SqlServerDbConnection = "data source =.; Initial Catalog = AhmadMessages; user id = sa; password = Aa123456;";

        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            //optionsBuilder.UseSqlite(args.Length > 0 ? args[0] : SqliteDbConnection, a => a.MigrationsAssembly(GetType().Assembly.FullName));
            optionsBuilder.UseSqlServer(args.Length > 0 ? args[0] : SqlServerDbConnection, a => a.MigrationsAssembly(GetType().Assembly.FullName));
            return new AppDbContext(optionsBuilder.Options);
        }
    }
}

