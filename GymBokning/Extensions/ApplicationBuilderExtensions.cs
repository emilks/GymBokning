using GymBokning.Data;
using Microsoft.EntityFrameworkCore;

namespace GymBokning.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

                //db.Database.EnsureDeleted();
                //db.Database.Migrate();

                //dotnet user-secrets set "AdminPW" "BytMig123!"
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                var adminPW = config["AdminPW"];

                try
                {
                    await SeedData.SeedDataAsync(db, serviceProvider, adminPW);
                }
                catch (Exception e)
                {

                    throw;
                }
            }

            return app;

        }
    }
}