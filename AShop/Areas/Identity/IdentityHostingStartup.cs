using System;
using AShop_Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AShop.Areas.Identity.IdentityHostingStartup))]
namespace AShop.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AshopDB>(options =>
                    options.UseSqlServer(
                        //context.Configuration.GetConnectionString("AShopIdentityDbContextConnection")));
                        context.Configuration.GetConnectionString("DefaultConnection")));


                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                  //  .AddEntityFrameworkStores<AshopDB>();
            });
        }
    }
}