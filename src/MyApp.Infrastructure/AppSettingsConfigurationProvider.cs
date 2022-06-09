using System.Globalization;
using Microsoft.Extensions.Configuration;
using MyApp.Common.Extensions;
using MyApp.Core;
using MyApp.Core.Interfaces;

namespace MyApp.Infrastructure;

public class AppSettingsConfigurationProvider : IAppConfigurationProvider
{
    private readonly IConfigurationRoot _configuration;

    public AppSettingsConfigurationProvider()
    {
        _configuration = new ConfigurationBuilder()
            .SetBasePath(Config.WorkingDir)
            .AddJsonFile("appsettings.json")
            .Build();
    }

    public string GetString(string settingKey, string defaultValue = null)
    {
        var value = this.GetSetting(settingKey);
        if (!value.IsNullOrEmpty())
            return value;

        return defaultValue;
    }

    public bool? GetBoolean(string settingKey, bool? defaultValue = null)
    {
        var value = this.GetSetting(settingKey);
        if (!value.IsNullOrEmpty())
            if (bool.TryParse(value, out var boolValue))
                return boolValue;

        return defaultValue;
    }

    public int? GetInteger(string settingKey, int? defaultValue = null)
    {
        var value = this.GetSetting(settingKey);
        if (!value.IsNullOrEmpty())
            if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out var intValue))
                return intValue;

        return defaultValue;
    }

    public decimal? GetDecimal(string settingKey, decimal? defaultValue = null)
    {
        var value = this.GetSetting(settingKey);
        if (!value.IsNullOrEmpty())
            if (decimal.TryParse(value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var decimalValue))
                return decimalValue;

        return defaultValue;
    }

    private string GetSetting(string settingKey)
    {
        return _configuration[settingKey];
    }
}
