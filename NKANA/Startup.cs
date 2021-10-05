using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity; using NKANA.Models;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NKANA.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net;

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

            services.ConfigureApplicationCookie(x =>
            {
                x.LoginPath = new PathString("/Identity/Account/login");
                x.AccessDeniedPath = new PathString("/Identity/Account/AccessDenied");
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddIdentity<NkanaUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })
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

        private async Task<bool> CreateUsersAndRoles(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var userManager = serviceProvider.GetRequiredService<UserManager<NkanaUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var roles = new List<IdentityRole>
            {
                new IdentityRole{ Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole{ Name = "Artist", NormalizedName = "ARTIST" },
                new IdentityRole{ Name = "SuperAdmin", NormalizedName = "SUPERADMIN" }
            };
            
            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            var users = new List<NkanaUser>
            {
                new NkanaUser{ Id = "4f974cf1-bc02-4aa7-bede-69a91d0e771d", UserName = "Admin", NormalizedUserName = "ADMIN", NormalizedEmail = "ADMIN@ACDTE.COM", Email = "admin@acdte.com", EmailConfirmed = true },
                new NkanaUser{ Id = "0e20a2de-342e-4b9d-a153-1c180e7f6435", UserName = "SuperAdmin", NormalizedUserName = "SUPERADMIN", NormalizedEmail = "SUPERADMIN@ACDTE.COM", Email = "superadmin@acdte.com", EmailConfirmed = true },
                new NkanaUser{ Id = "1135f23e-2eaf-44ef-830d-fcafce7b94ae", UserName = "System", NormalizedUserName = "SYSTEM", NormalizedEmail = "SYSTEM@ACDTE.COM", Email = "system@acdte.com", EmailConfirmed = true },
                new NkanaUser{ Id = "1135f23e-44ef-2eaf-830d-7b94aefcafce", UserName = "Africhina", NormalizedUserName = "AFRICHINA", NormalizedEmail = "AFRICHINA@ACDTE.COM", Email = "africhina@acdte.com", EmailConfirmed = true },
            };

            var passwords = new string[] { "greenadmin@Fdt122", "FindingSpaces&Trees", "Stranded@001", "level12#Goose" };
            for (int i = 0; i < users.Count; i++)
            {
                if (await userManager.FindByIdAsync(users[i].Id) != null) continue;
                var result = await userManager.CreateAsync(users[i], passwords[i]);
                if (result.Succeeded)
                {
                    var user = await userManager.FindByIdAsync(users[i].Id);
                    await userManager.AddToRolesAsync(user, new string[] { "SuperAdmin" });
                }
            }

            return true;
        }
    }
}
