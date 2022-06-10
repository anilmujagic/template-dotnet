using System.Reflection;
using MyApp.Core.Interfaces;

namespace MyApp.Core;

public static class Config
{
    private static IAppConfigurationProvider _provider;
    public static void SetConfigurationProvider(IAppConfigurationProvider provider)
    {
        _provider = provider;
    }

    public static string AppDir => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
    public static string WorkingDir => Directory.GetCurrentDirectory();

    public static string DB => _provider.GetString("DB");
}
