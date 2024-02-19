using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly IAppInteractionsService _appInteractionsService;
    private readonly IDatabaseRepository _databaseRepository;

    [ObservableProperty] private string _eventId = string.Empty;
    [ObservableProperty] private int _leadId;

    [ObservableProperty] private string _firstName = string.Empty;
    [ObservableProperty] private string _lastName = string.Empty;
    [ObservableProperty] private string _company = string.Empty;
    [ObservableProperty] private string _email = string.Empty;
    [ObservableProperty] private string _phone = string.Empty;

    [ObservableProperty] private string _address = string.Empty;
    [ObservableProperty] private string _zipCode = string.Empty;
    [ObservableProperty] private string _city = string.Empty;
    [ObservableProperty] private string _product = string.Empty;
    [ObservableProperty] private string _seller = string.Empty;

    [ObservableProperty] private string _notes = string.Empty;
    [ObservableProperty] private DateTime _scannedDate;

    public LeadDetailViewModel(
        IAppInteractionsService appInteractionsService,
        IDatabaseRepository databaseRepository)
    {
        _appInteractionsService = appInteractionsService;
        _databaseRepository = databaseRepository;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue(nameof(LeadItemRecordDto), out var value))
        {
            Console.WriteLine("❌ -> Failed to TryGetValue from Query.");
            return;
        }


        if (value is LeadItemRecordDto record)
            LoadLeadInfo(record.LeadId);

        query.Clear();
    }

    private async void LoadLeadInfo(int leadId)
    {
        var lead = await _databaseRepository.FetchLeadDetailByIdAsync(leadId);
        if (lead is null)
        {
            Console.WriteLine("❌ -> Failed to fetch lead by id.");
            return;
        }

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            EventId = lead.EventId;
            LeadId = lead.LeadId;

            FirstName = lead.FirstName;
            LastName = lead.LastName;
            Company = lead.Company;
            Email = lead.Email;
            Phone = lead.Phone;

            Address = lead.Address;
            ZipCode = lead.ZipCode;
            City = lead.City;
            Product = lead.Product;
            Seller = lead.Seller;

            Notes = lead.Notes;
            ScannedDate = lead.ScannedDate;
        });
    }

    [RelayCommand]
    private async Task SaveChanges()
    {
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
            Product,
            Seller,
            Notes,
            ScannedDate,
            LeadId
        );
        await _databaseRepository.UpdateLeadAsync(record);

        WeakReferenceMessenger.Default.Send(new LeadUpdatedMessage(LeadId));
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task OpenPhoneDialer()
    {
        await _appInteractionsService.OpenPhoneDialerAsync(Phone);
    }

    [RelayCommand]
    private async Task OpenEmailClient()
    {
        await _appInteractionsService.ComposeEmailAsync(Email, "AGI Events");
    }
}