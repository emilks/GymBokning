//using Bogus;
using GymBokning.Models;
using GymBokning.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace GymBokning.Data
{
    public class SeedData
    {
        private static ApplicationDbContext db = default!;
        private static RoleManager<IdentityRole> roleManager = default!;
        private static UserManager<ApplicationUser> userManager = default!;

        public static async Task SeedDataAsync(ApplicationDbContext context, IServiceProvider services, string adminPW)
        {
            if (string.IsNullOrWhiteSpace(adminPW))
                throw new ArgumentNullException(nameof(adminPW));
            if (context == null)
                throw new NullReferenceException(nameof(context));

            db = context;

            if (db.Users.Any()) return;

            roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

            if (roleManager is null)
                throw new NullReferenceException(nameof(RoleManager<IdentityRole>));

            userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            if (roleManager is null)
                throw new NullReferenceException(nameof(UserManager<ApplicationUser>));

            var roleNames = new[] { "Member", "Admin" };
            var adminEmail = "admin@gymbokning.com";
            //adminPW = "Admin123!";

            await AddRolesAsync(roleNames);

            var admin = await SeedAdmin(adminEmail, adminPW);

            foreach (var role in roleNames)
            {
                if (await userManager.IsInRoleAsync(admin, role)) continue;
                var result = await userManager.AddToRoleAsync(admin, role);
                if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));
            }


            await db.SaveChangesAsync();
        }

        private static async Task AddRolesAsync(string[] roleNames)
        {
            foreach (var roleName in roleNames)
            {
                var role = new IdentityRole { Name = roleName};
                var result = await roleManager.CreateAsync(role);

                if (!result.Succeeded) throw new Exception(String.Join("Error: ", result));
            }

        }

        private static async Task<ApplicationUser> SeedAdmin(string email, string PW)
        {
            var found = await userManager.FindByEmailAsync(email);

            if (found != null) return null!;

            var admin = new ApplicationUser { UserName = email, Email = email, FirstName = "Admin", LastName = "Adminson" };

            var result = await userManager.CreateAsync(admin, PW);
            if (!result.Succeeded) throw new Exception(string.Join("\n", result.Errors));

            return admin;

        }
    }
}
