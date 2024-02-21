namespace AGIEvents.Lib.Services;

public interface IDotEnvService
{
    void LoadEnvironmentVariables(Task<Stream> dotEnvStreamTask);
}