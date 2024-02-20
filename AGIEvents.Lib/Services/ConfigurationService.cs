namespace AGIEvents.Lib.Services;

public class ConfigurationService : IConfigurationService
{
    string IConfigurationService.GetFirebaseUrl()
    {
        return Environment.GetEnvironmentVariable("FIREBASE_URL") ?? string.Empty;
    }
}