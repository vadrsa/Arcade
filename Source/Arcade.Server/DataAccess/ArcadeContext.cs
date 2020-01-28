using BusinessEntities;
using Common.Faults;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using System;

namespace DataAccess
{
    public class ArcadeContext : IdentityDbContext<User, Role, Guid>
    {
        public static int idCounter = 1;

        public int id;

        string connectionString;

        public DbSet<Image> Images { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Fault> Faults { get; set; }

        public ArcadeContext(DbContextOptions<ArcadeContext> contextOptions)
        {
            connectionString = contextOptions.GetExtension<SqlServerOptionsExtension>().ConnectionString;
            id = idCounter++;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Image>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Image>()
                .HasKey(p => p.Id);

            builder.Entity<Game>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<Game>()
                .HasKey(p => p.Id);

            builder.Entity<Game>()
                .Property(p => p.Name)
                .IsRequired();

            builder.Entity<Game>()
                .Property(p => p.Category)
                .IsRequired();

            builder.Entity<Fault>()
                .HasKey(p => p.Code);

            builder.Entity<Fault>()
                .Property(p => p.Code)
                .ValueGeneratedNever();

            builder.Entity<Fault>()
                .Property(p => p.HttpStatusCode)
                .IsRequired();

        }
    }
}
