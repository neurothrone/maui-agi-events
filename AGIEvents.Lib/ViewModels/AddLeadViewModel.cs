using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

[QueryProperty("EventId", "EventId")]
public partial class AddLeadViewModel : ObservableObject
{
    [ObservableProperty] private string _eventId;
    [ObservableProperty] private string _firstName;
    [ObservableProperty] private string _lastName;
    [ObservableProperty] private string _company;
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _phone;
    [ObservableProperty] private string _address;
    [ObservableProperty] private string _zipCode;
    [ObservableProperty] private string _city;

    // TODO: Validation for button, first name or last name must not be empty before enabling

    [RelayCommand]
    private async Task AddLead()
    {
        // TODO: Save to Database and notify through event to LeadsViewModel that a new Lead was added
        await Shell.Current.GoToAsync("..");
    }
}