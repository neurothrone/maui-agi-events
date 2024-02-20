namespace AGIEvents.Lib.Services;

public class DotEnvService : IDotEnvService
{
    void IDotEnvService.LoadEnvironmentVariables(Stream dotEnvStreamTask)
    {
        using var reader = new StreamReader(dotEnvStreamTask);
        while (reader.ReadLine() is { } line)
        {
            var parts = line.Split(
                '=',
                2,
                StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != 2)
                continue; // Skip malformed lines

            var key = parts[0].Trim();
            var value = parts[1].Trim().Trim('"'); // Remove whitespace and quotes

            Environment.SetEnvironmentVariable(key, value);
        }
    }
}