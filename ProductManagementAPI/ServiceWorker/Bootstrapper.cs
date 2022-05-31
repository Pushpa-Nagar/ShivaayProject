using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductManagementAPI.ServiceWorker
{
    public class Bootstrapper
    {
        static Bootstrapper()
        {
        }
        public Bootstrapper(IServiceCollection services, string defaultConnection)
        {
            ProductManagementAPI.Infrastructure.Repository.Bootstrapper.ConfigureServices(services, defaultConnection);
        }
    }
}
