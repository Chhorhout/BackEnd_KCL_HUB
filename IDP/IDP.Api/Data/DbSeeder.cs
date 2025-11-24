using IDP.Api.Models;

namespace IDP.Api.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAsync(ApplicationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role { Id = Guid.NewGuid(), Name = "Admin" },
                    new Role { Id = Guid.NewGuid(), Name = "User" },
                    new Role { Id = Guid.NewGuid(), Name = "Manager" }
                };

                context.Roles.AddRange(roles);
                await context.SaveChangesAsync();
            }
        }
    }
}

