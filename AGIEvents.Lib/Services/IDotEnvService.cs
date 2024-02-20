namespace AGIEvents.Lib.Services;

public interface IDotEnvService
{
    void LoadEnvironmentVariables(Stream dotEnvStreamTask);
}