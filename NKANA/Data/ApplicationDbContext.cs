using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NKANA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NKANA.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Artist> Artists { get; set; }
        public DbSet<ArtistSkill> ArtistSkills { get; set; }
        public DbSet<ArtWork> ArtWorks { get; set; }
        public DbSet<ArtWorkCategory> ArtWorkCategories { get; set; }
        public DbSet<ArtWorkMedia> ArtWorkMedias { get; set; }
        public DbSet<ArtWorkRequest> ArtWorkRequests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<NkanaUser> User { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<NkanaUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
        }
    }
}
