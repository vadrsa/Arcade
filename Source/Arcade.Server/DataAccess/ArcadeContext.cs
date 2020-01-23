using BusinessEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DataAccess
{
    public class ArcadeContext : IdentityDbContext<User, Role, Guid>
    {

        public DbSet<Image> Images { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=DESKTOP-9UMEBQ0;Database=Arcade;Trusted_Connection=True;Enlist=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Image>()
                .HasKey(p => p.Id);
        }
    }
}
