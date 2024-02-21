namespace AGIEvents.Lib.Services;

public class ConfigurationService : IConfigurationService
{
    string IConfigurationService.GetFirebaseUrl() => Environment.GetEnvironmentVariable("FIREBASE_URL") ?? string.Empty;

    string IConfigurationService.GetSupportEmail() =>
        Environment.GetEnvironmentVariable("SUPPORT_EMAIL") ?? string.Empty;
}