using Application.Common.Interfaces.Queries;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence;

public static  class ConfigurePersistence
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<MongoDbService>(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            return new MongoDbService(config);
        });

        services.AddRepositories();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ClassRepository>();
        services.AddScoped<IClassRepository>(provider => provider.GetService<ClassRepository>());
        services.AddScoped<IClassQuery>(provider => provider.GetService<ClassRepository>());

        services.AddScoped<UserRepository>();
        services.AddScoped<IUserRepository>(provider => provider.GetService<UserRepository>());
        services.AddScoped<IUserQuery>(provider => provider.GetService<UserRepository>());
    }
    
    
}