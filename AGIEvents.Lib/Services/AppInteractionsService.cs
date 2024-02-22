namespace AGIEvents.Lib.Services;

public class AppInteractionsService(INotificationService notificationService) : IAppInteractionsService
{
    public async Task OpenBrowserAsync(string url)
    {
        try
        {
            var uri = new Uri(url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception)
        {
            await notificationService.ShowNotificationAsync(
                "An unexpected error occurred.",
                "No browser may be installed on the device.",
                "OK");
        }
    }

    public async Task ComposeEmailAsync(string recipient, string subject)
    {
        if (!Email.Default.IsComposeSupported)
            return;

        var message = new EmailMessage
        {
            Subject = subject,
            BodyFormat = EmailBodyFormat.PlainText,
            To = [recipient]
        };

        await Email.Default.ComposeAsync(message);
    }

    public async Task OpenPhoneDialerAsync(string phoneNumber)
    {
        try
        {
            if (PhoneDialer.Default.IsSupported)
                PhoneDialer.Default.Open(phoneNumber);
        }
        catch (ArgumentNullException)
        {
            await notificationService.ShowNotificationAsync(
                "Unable to dial",
                "Phone number was not valid.",
                "OK");
        }
        catch (Exception)
        {
            // Other error has occurred.
            await notificationService.ShowNotificationAsync(
                "Unable to dial",
                "Phone dialing failed.",
                "OK");
        }
    }
}