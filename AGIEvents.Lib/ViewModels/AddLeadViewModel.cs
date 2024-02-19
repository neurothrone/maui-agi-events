using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

[QueryProperty(nameof(EventId), nameof(EventId))]
public partial class AddLeadViewModel : ObservableObject
{
    [ObservableProperty] private string _eventId = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _phone = string.Empty;
    [ObservableProperty] private string _address = string.Empty;
    [ObservableProperty] private string _zipCode = string.Empty;
    [ObservableProperty] private string _city = string.Empty;

    private string _firstName = string.Empty;

    public string FirstName
    {
        get => _firstName;
        set
        {
            SetProperty(ref _firstName, value);
            SubmitCommand.NotifyCanExecuteChanged();
        }
    }

    private string _lastName = string.Empty;

    public string LastName
    {
        get => _lastName;
        set
        {
            SetProperty(ref _lastName, value);
            SubmitCommand.NotifyCanExecuteChanged();
        }
    }

    private string _company = string.Empty;

    public string Company
    {
        get => _company;
        set
        {
            SetProperty(ref _company, value);
            SubmitCommand.NotifyCanExecuteChanged();
        }
    }

    private bool IsFormValid
    {
        get
        {
            var requiredFields = new List<string> { FirstName, LastName, Company };
            return requiredFields.Any(field => !string.IsNullOrWhiteSpace(field));
        }
    }

    public AsyncRelayCommand SubmitCommand { get; }

    public AddLeadViewModel()
    {
        SubmitCommand = new AsyncRelayCommand(OnSubmit, () => IsFormValid);
    }

    private async Task OnSubmit()
    {
        await AddLead();
    }

    private async Task AddLead()
    {
        // TODO: Save to Database and notify through Message (LeadInsertedMessage) to LeadsViewModel that a new Lead was added
        await Shell.Current.GoToAsync("..");
    }
}