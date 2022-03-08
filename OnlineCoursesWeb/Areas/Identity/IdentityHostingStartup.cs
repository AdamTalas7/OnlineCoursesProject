using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineCoursesWeb.Data;

[assembly: HostingStartup(typeof(OnlineCoursesWeb.Areas.Identity.IdentityHostingStartup))]
namespace OnlineCoursesWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OnlineCoursesWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OnlineCoursesWebContextConnection")));

                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<OnlineCoursesWebContext>();
            });
        }
    }
}