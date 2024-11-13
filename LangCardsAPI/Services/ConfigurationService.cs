namespace LangCardsAPI.Services;

public static class ConfigurationService
{
    public static string? GetSecurityKey(this IConfiguration configuration) => configuration["SecurityKey"];
}