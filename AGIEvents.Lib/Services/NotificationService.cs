namespace AGIEvents.Lib.Services;

public class NotificationService : INotificationService
{
    async Task INotificationService.ShowNotificationAsync(string title, string message, string cancel)
    {
        await Shell.Current.DisplayAlert(title, message, cancel);
    }
}