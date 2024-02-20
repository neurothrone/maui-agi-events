namespace AGIEvents.Lib.Services;

public interface INotificationService
{
    Task ShowNotificationAsync(string title, string message, string cancel);
}