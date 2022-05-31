using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductManagementAPI.Infrastructure.DataModels;
using ProductManagementAPI.Infrastructure.Repository.IProductManagementRepository;
using ProductManagementAPI.Infrastructure.Repository.ProductManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.Infrastructure.Repository
{
    public class Bootstrapper
    {
        public Bootstrapper()
        {
        }

        public static void ConfigureServices(IServiceCollection services, string defaultConnectionString)
        {
            services.AddEntityFrameworkSqlServer();
            services.AddDbContextPool<ShivaayProductDBContext>(options =>
            options.UseSqlServer(defaultConnectionString));

            services.AddScoped<IProductGroupRepository, ProductGroupRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IAgreementRepository, AgreementRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
