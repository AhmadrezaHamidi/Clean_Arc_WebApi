using AhFramWork.Persistance.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AhFramWork.Persistance
{
    public static class ServiceRegistration
    {

        public static void AddPersistance(this IServiceCollection services,string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                var connection = connectionString is null ? "DefultConnection" : connectionString;
                opt. 
            })            

        }
    }
}
