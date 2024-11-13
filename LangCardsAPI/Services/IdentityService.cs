using LangCardsDomain.Models;
using LangCardsPersistence;
using Microsoft.EntityFrameworkCore;

namespace LangCardsAPI.Services;

public static class IdentityService
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddIdentityCore<ApplicationUser>(opt =>
                {
                    opt.Password.RequireDigit = true;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireLowercase = true;
                    opt.Password.RequireUppercase = true;
                }
            )
            .AddEntityFrameworkStores<IdentityContext>();
        return services;
    }
}