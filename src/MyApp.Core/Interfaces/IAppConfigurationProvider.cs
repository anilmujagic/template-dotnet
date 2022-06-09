namespace MyApp.Core.Interfaces;

public interface IAppConfigurationProvider
{
    string GetString(string settingKey, string defaultValue = null);
    bool? GetBoolean(string settingKey, bool? defaultValue = null);
    int? GetInteger(string settingKey, int? defaultValue = null);
    decimal? GetDecimal(string settingKey, decimal? defaultValue = null);
}