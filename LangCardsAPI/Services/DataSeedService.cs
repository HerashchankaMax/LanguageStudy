using LangCardsDomain.Models;
using LangCardsPersistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LangCardsAPI.Services;

public static class DataSeedService
{
    public static async Task SeedData(IdentityContext context, UserManager<ApplicationUser> userManager)
    {
        await context.Database.MigrateAsync();
        if (!context.Users.Any())
        {
            ApplicationUser user = new ApplicationUser
            {
                UserName = "TestUser",
                NormalizedUserName = "testuser",
                Email = "testuser@gmail.com",
                NormalizedEmail = "testuser@gmail.com",
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "User",
            };
            var res = await userManager.CreateAsync(user, "Pa$$w0rd");
            Log.Logger.Information($"User {user.UserName} has been created");
            await context.SaveChangesAsync();
        }
    }
}