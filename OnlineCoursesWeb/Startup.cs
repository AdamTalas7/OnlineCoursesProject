using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnlineCoursesWeb.Data;
using OnlineCoursesWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineCoursesWeb
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private async Task CreatingRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            var adminExists = await RoleManager.RoleExistsAsync("Admin");
            if (!adminExists)
            {
                IdentityRole newRole = new IdentityRole("Admin");
                await RoleManager.CreateAsync(newRole);
            }
            var teacherExists = await RoleManager.RoleExistsAsync("Teacher");
            if (!teacherExists)
            {
                IdentityRole newRole = new IdentityRole("Teacher");
                await RoleManager.CreateAsync(newRole);
            }
            var studentExists = await RoleManager.RoleExistsAsync("Student");
            if (!studentExists)
            {
                IdentityRole newRole = new IdentityRole("Student");
                await RoleManager.CreateAsync(newRole);
            }

            UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
            IdentityUser admin = await userManager.FindByNameAsync("admin@gmail.com");
            if (admin == null)
            {
                var user = new IdentityUser { UserName = "admin@gmail.com", Email = "admin@gmail.com", EmailConfirmed = true, LockoutEnabled = false };
                var create = await userManager.CreateAsync(user, "Adminpassword12!");
                if (create.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            
            IdentityUser demoStudent = await userManager.FindByNameAsync("demostudent@gmail.com");
            if (demoStudent == null)
            {
                var user = new IdentityUser { UserName = "demostudent@gmail.com", Email = "demostudent@gmail.com", EmailConfirmed = true, LockoutEnabled = false };
                var create = await userManager.CreateAsync(user, "demoStudent4!");
                if (create.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Student");
                }
            }
            
            IdentityUser demoTeacher = await userManager.FindByNameAsync("demoteacher@gmail.com");
            if (demoTeacher == null)
            {
                var user = new IdentityUser { UserName = "demoteacher@gmail.com", Email = "demoteacher@gmail.com", EmailConfirmed = true, LockoutEnabled = false };
                var create = await userManager.CreateAsync(user, "demoTeacher4!");
                if (create.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Teacher");
                }
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
            .AddRazorRuntimeCompilation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            CreatingRoles(serviceProvider).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
