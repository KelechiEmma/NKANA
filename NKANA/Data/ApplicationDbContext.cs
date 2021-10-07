using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NKANA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NKANA.Data
{
    public class ApplicationDbContext : IdentityDbContext<NkanaUser, NkanaRole, string, NkanaUserClaim, NkanaUserRole, NkanaUserLogin, NkanaRoleClaim, NkanaUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistSkill> ArtistSkills { get; set; }
        public DbSet<ArtWork> ArtWorks { get; set; }
        public DbSet<ArtWorkCategory> ArtWorkCategories { get; set; }
        public DbSet<ArtWorkImage> ArtWorkImages { get; set; }
        public DbSet<ArtWorkRequest> ArtWorkRequests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public override DbSet<NkanaUser> Users { get; set; }
        public override DbSet<NkanaRole> Roles { get; set; }
        public override DbSet<NkanaUserLogin> UserLogins { get; set; }
        public override DbSet<NkanaUserRole> UserRoles { get; set; }
        public override DbSet<NkanaUserClaim> UserClaims { get; set; }
        public override DbSet<NkanaUserToken> UserTokens { get; set; }
        public override DbSet<NkanaRoleClaim> RoleClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ArtWorkCategory>(o =>
            {
                o.HasKey(x => new { x.CategoryId, x.ArtWorkId });
            });
            builder.Entity<ArtistSkill>(o =>
            {
                o.HasKey(x => new { x.ArtistId, x.SkillId });
            });
            builder.Entity<NkanaUser>().HasData(SeedData.Users);
            builder.Entity<NkanaRole>().HasData(SeedData.Roles);
            builder.Entity<NkanaUserRole>().HasData(SeedData.UserRoles);
        }
    }
    public static class SeedData
    {
        public static List<NkanaRole> Roles = new List<NkanaRole>
            {
                new NkanaRole { Id = "1135f23e-fcafce7b94ae-2eaf44ef830d", Name = "Admin", NormalizedName = "ADMIN" },
                new NkanaRole { Id = "bede-69a91d0e771d-4f974cf1-bc02-4aa7", Name = "Artist", NormalizedName = "ARTIST" },
                new NkanaRole { Id = "1135f23e-44ef-2eaf-830d-7b94aefcafce", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" }
            };

        public static List<NkanaUser> Users = new List<NkanaUser>
            {
                new NkanaUser{ Id = "4f974cf1-bc02-4aa7-bede-69a91d0e771d", UserName = "Admin", NormalizedUserName = "ADMIN", NormalizedEmail = "ADMIN@ACDTE.COM", Email = "admin@acdte.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOXP8qq84anuzQkszIvCVMnQYrgE6iKWHkppA6JH6w8/116oC+X5VKvPOw941/asYQ==", ConcurrencyStamp = "fa7ff9e8-fae8-4a2e-8664-a32dce6d7332", SecurityStamp = "fa7ff9e8-fae8-4a2e-8664-a32dce6d7332" },
                new NkanaUser{ Id = "0e20a2de-342e-4b9d-a153-1c180e7f6435", UserName = "SuperAdmin", NormalizedUserName = "SUPERADMIN", NormalizedEmail = "SUPERADMIN@ACDTE.COM", Email = "superadmin@acdte.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEELp6FqpcHmlQMXwrflJ4BAsAa3jwm133/AWr99VcuOQoBvMHGz78IRt3EOtXUfIyg==", ConcurrencyStamp = "66e5e17c-7391-4937-9a82-bc36661a2f7e" , SecurityStamp = "66e5e17c-7391-4937-9a82-bc36661a2f7e"},
                new NkanaUser{ Id = "1135f23e-2eaf-44ef-830d-fcafce7b94ae", UserName = "System", NormalizedUserName = "SYSTEM", NormalizedEmail = "SYSTEM@ACDTE.COM", Email = "system@acdte.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEOXP8qq84anuzQkszIvCVMnQYrgE6iKWHkppA6JH6w8/116oC+X5VKvPOw941/asYQ==", ConcurrencyStamp = "fa7ff9e8-fae8-4a2e-a32dce6d7332-8664", SecurityStamp = "fa7ff9e8-fae8-4a2e-a32dce6d7332-8664" },
                new NkanaUser{ Id = "1135f23e-44ef-2eaf-830d-7b94aefcafce", UserName = "Africhina", NormalizedUserName = "AFRICHINA", NormalizedEmail = "AFRICHINA@ACDTE.COM", Email = "africhina@acdte.com", EmailConfirmed = true, PasswordHash = "AQAAAAEAACcQAAAAEELp6FqpcHmlQMXwrflJ4BAsAa3jwm133/AWr99VcuOQoBvMHGz78IRt3EOtXUfIyg==", ConcurrencyStamp = "7391-66e5e17c-4937-9a82-bc36661a2f7e", SecurityStamp = "7391-66e5e17c-4937-9a82-bc36661a2f7e" },
            };

        public static List<NkanaUserRole> UserRoles = new List<NkanaUserRole>
        {
            new NkanaUserRole{ UserId = "0e20a2de-342e-4b9d-a153-1c180e7f6435", RoleId = "1135f23e-fcafce7b94ae-2eaf44ef830d" },
            new NkanaUserRole{ UserId = "0e20a2de-342e-4b9d-a153-1c180e7f6435", RoleId = "1135f23e-44ef-2eaf-830d-7b94aefcafce" },

            new NkanaUserRole{ UserId = "1135f23e-2eaf-44ef-830d-fcafce7b94ae", RoleId = "1135f23e-fcafce7b94ae-2eaf44ef830d" },
            new NkanaUserRole{ UserId = "1135f23e-2eaf-44ef-830d-fcafce7b94ae", RoleId = "1135f23e-44ef-2eaf-830d-7b94aefcafce" },

            new NkanaUserRole{ UserId = "1135f23e-44ef-2eaf-830d-7b94aefcafce", RoleId = "1135f23e-fcafce7b94ae-2eaf44ef830d" },
            new NkanaUserRole{ UserId = "1135f23e-44ef-2eaf-830d-7b94aefcafce", RoleId = "1135f23e-44ef-2eaf-830d-7b94aefcafce" },

            new NkanaUserRole{ UserId = "4f974cf1-bc02-4aa7-bede-69a91d0e771d", RoleId = "1135f23e-fcafce7b94ae-2eaf44ef830d" }

        };
    }
}
