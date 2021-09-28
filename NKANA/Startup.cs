using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NKANA.Data;
using NKANA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NKANA
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.EnableEndpointRouting = false;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<NkanaUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddControllersWithViews();

            //CreateUsersAndRoles(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private static void CreateUsersAndRoles(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<NkanaUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole{ Name = "Admin" },
                new IdentityRole{ Name = "Artist" },
                new IdentityRole{ Name = "SuperAdmin" }
            };

            foreach (var role in roles)
            {
                roleManager.CreateAsync(role);
            }

            var users = new List<NkanaUser>
            {
                new NkanaUser{ UserName = "Admin", NormalizedUserName = "ADMIN", NormalizedEmail = "ADMIN@ACDTE.COM", Email = "admin@acdte.com", EmailConfirmed = true },
                new NkanaUser{ UserName = "SuperAdmin", NormalizedUserName = "SUPERADMIN", NormalizedEmail = "SUPERADMIN@ACDTE.COM", Email = "superadmin@acdte.com", EmailConfirmed = true },
                new NkanaUser{ UserName = "System", NormalizedUserName = "SYSTEM", NormalizedEmail = "SYSTEM@ACDTE.COM", Email = "system@acdte.com", EmailConfirmed = true },
                new NkanaUser{ UserName = "Africhina", NormalizedUserName = "AFRICHINA", NormalizedEmail = "AFRICHINA@ACDTE.COM", Email = "africhina@acdte.com", EmailConfirmed = true },
            };

            var passwords = new string[] { "greenadmin@Fdt122", "FindingSpaces&Trees", "Stranded@001", "level12#Goose" };
            for (int i = 0; i < users.Count; i++)
            {
                var result = userManager.CreateAsync(users[i], passwords[i]).GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    userManager.AddToRolesAsync(users[i], new string[] { "Admin", "SuperAdmin" });
                }
            }
        }
    }
}
