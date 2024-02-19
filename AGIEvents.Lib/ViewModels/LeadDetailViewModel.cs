using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly IAppInteractionsService _appInteractionsService;
    private readonly IDatabaseRepository _databaseRepository;

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
    [ObservableProperty] private DateTime _scannedAt;

    [ObservableProperty] private bool _showNotes = false;

    public LeadDetailViewModel(
        IAppInteractionsService appInteractionsService,
        IDatabaseRepository databaseRepository)
    {
        _appInteractionsService = appInteractionsService;
        _databaseRepository = databaseRepository;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("LeadId", out var value))
            return;

        if (value is int leadId)
            LoadLeadInfo(leadId);
    }

    private async void LoadLeadInfo(int leadId)
    {
        // TODO: load Lead from database
        var matchedLead = await _databaseRepository.FetchLeadByIdAsync(leadId);
        if (matchedLead is null)
        {
            Console.WriteLine("❌ -> Failed to fetch lead by id.");
            return;
        }

        LeadId = matchedLead.LeadId;
        FirstName = matchedLead.FirstName;
        LastName = matchedLead.LastName;
        Company = matchedLead.Company;
        Email = matchedLead.Email;
        Phone = matchedLead.Phone;

        Address = matchedLead.Address;
        ZipCode = matchedLead.ZipCode;
        City = matchedLead.City;
        Product = matchedLead.Product;
        Seller = matchedLead.Seller;

        Notes = matchedLead.Notes;
        ScannedAt = matchedLead.ScannedDate;
    }

    [RelayCommand]
    private void TabBarPressed(bool pressedNotes)
    {
        Console.WriteLine($"ℹ️ -> pressedNotes: {pressedNotes}");
        ShowNotes = pressedNotes;
    }

    [RelayCommand]
    private async Task SaveChanges()
    {
        // TODO: save changes to database
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