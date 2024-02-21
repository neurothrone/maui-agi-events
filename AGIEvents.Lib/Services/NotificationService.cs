namespace AGIEvents.Lib.Services;

public class NotificationService : INotificationService
{
    async Task INotificationService.ShowNotificationAsync(string title, string message, string cancel)
    {
        await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.DisplayAlert(title, message, cancel));
    }

    async Task<bool> INotificationService.ShowConfirmationPromptAsync(string title, string message)
    {
        return await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.DisplayAlert(
            title,
            message: message,
            accept: "Yes",
            cancel: "Cancel"
        ));
    }
}