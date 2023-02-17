using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using AhFramWork.Persistances.UnitOfWork;
using System.Reflection.Metadata;

namespace AhFramWork.Persistances
{
	public static class RegisterationServices
	{
        public static void AddPersistance(this IServiceCollection services,
            IConfiguration Configuration
            )
        {

            ///IUnitOfWork<AppDbContext> unitOfWork
            var connectionstring = Configuration.GetConnectionString("defaultConnection");

            //services
            //  .AddDbContext<BloggingContext>(opt => opt.UseMySql("Server=localhost;database=uow;uid=root;pwd=root1234;"))
            //  //.AddDbContext<BloggingContext>(opt => opt.UseInMemoryDatabase("UnitOfWork"))
            //  .AddUnitOfWork<BloggingContext>()
            //  .AddCustomRepository<Blog, CustomBlogRepository>();



           var rrr =   services.AddDbContext<AppDbContext>(opetions =>
                opetions.UseSqlServer(connectionstring))
                .AddUnitOfWork<AppDbContext>();



            services.AddUnitOfWork<AppDbContext>();




          ///  IUnitOfWork<AppDbContext> unitOfWork =
                ///TODO new UnitOfWork<AppDbContext>();


            //.DbContext.Database.GetAppliedMigrations();
            //unitOfWork.DbContext.Database.Migrate();

        }
    }
}

