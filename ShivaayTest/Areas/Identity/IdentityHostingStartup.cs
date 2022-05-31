using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShivaayTest.Areas.Identity.Data;
using ShivaayTest.Data;

[assembly: HostingStartup(typeof(ShivaayTest.Areas.Identity.IdentityHostingStartup))]
namespace ShivaayTest.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AuthDBContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AuthDBConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    //options.Password.RequiredLength = 8;
                    // options.Password.RequireLowercase = false;
                    //options.Password.RequireUppercase = false;

                })
                    .AddEntityFrameworkStores<AuthDBContext>();
            });
        }
    }
}