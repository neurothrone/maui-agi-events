namespace AGIEvents.Lib.Services;

public interface INotificationService
{
    Task ShowNotificationAsync(string title, string message, string cancel);
    Task<bool> ShowConfirmationPromptAsync(string title, string message);
    Task ShowSnackbar(string text);
}