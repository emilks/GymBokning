using GymBokning.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GymBokning.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<GymClass> GymClasses { get; set; }
        //public DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DbSet<ApplicationUserGymClass>? ApplicationUserGymClass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserGymClass>().HasKey(t => new {t.ApplicationUserId, t.GymClassId});

            //modelBuilder.Entity<GymClass>().HasQueryFilter();

            //modelBuilder.Entity<ApplicationUser>().Property<DateTime>("TimeOfRegistration");
        }

    }
}