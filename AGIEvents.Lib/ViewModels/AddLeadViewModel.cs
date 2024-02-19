using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

[QueryProperty(nameof(EventId), nameof(EventId))]
public partial class AddLeadViewModel : ObservableObject
{
    private readonly IDatabaseRepository _databaseRepository;

    [ObservableProperty] private string _eventId = string.Empty; // TODO: Does this have to be an ObservableProperty?
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

    public AddLeadViewModel(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
        SubmitCommand = new AsyncRelayCommand(AddLead, () => IsFormValid);
    }

    private async Task AddLead()
    {
        if (!IsFormValid)
        {
            Console.WriteLine("❌ -> Form Is not Valid.");
            return;
        }

        var record = new LeadDetailRecordDto(
            EventId,
            FirstName,
            LastName,
            Company,
            Email,
            Phone,
            Address,
            ZipCode,
            City,
            string.Empty,
            string.Empty,
            string.Empty,
            DateTime.Now
        );
        var savedLead = await _databaseRepository.SaveLeadAsync(record);

        WeakReferenceMessenger.Default.Send(new LeadInsertedMessage(savedLead.LeadId));
        await Shell.Current.GoToAsync("..");
    }
}