namespace AGIEvents.Lib.Services;

public interface IAppInteractionsService
{
    Task OpenBrowserAsync(string url);
    Task ComposeEmailAsync(string recipient, string subject);
    Task OpenPhoneDialerAsync(string phoneNumber);
}