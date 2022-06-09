using MyApp.Core.Interfaces;

namespace MyApp.Infrastructure.DI;

/// <summary>
/// Used to resolve dependencies not managed by DI container.
/// </summary>
public static class DependencyResolver
{
    public static IAppConfigurationProvider GetConfigurationProvider()
    {
        return new AppSettingsConfigurationProvider();
    }
}