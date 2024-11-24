using LangCardsDomain.IRepositories;
using LangCardsDomain.Models;
using LangCardsPersistence;
using LangCardsPersistence.Repositories;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace LangCardsAPI.Services;

public static class DataAccessService
{
    public static IServiceCollection AddIdentityContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString"));
        });
        return services;
    }

    public static IServiceCollection AddCardDataBase(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(typeof(Guid), new GuidSerializer(GuidRepresentation.Standard));
        services.AddSingleton<CardsDbContext>();
        services.AddScoped<IValuableRepository<WordEntity>, WordsRepository>();
        services.AddScoped<IValuableRepository<CollectionEntity>, CollectionsRepository>();
        return services;
    }
}