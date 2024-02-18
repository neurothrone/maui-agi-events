using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadDetailViewModel : ObservableObject, IQueryAttributable
{
    private readonly IAppInteractionsService _appInteractionsService;

    [ObservableProperty] private string _leadId = string.Empty;
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

    public LeadDetailViewModel(IAppInteractionsService appInteractionsService)
    {
        _appInteractionsService = appInteractionsService;
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

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("LeadId", out var value))
            return;

        LoadLeadInfo(value.ToString() ?? "");
    }

    private async void LoadLeadInfo(string leadId)
    {
        // TODO: load Lead from database
        var matchedLead = Lead.Samples().FirstOrDefault((lead) => lead.Id == leadId);
        if (matchedLead == null)
            return;

        LeadId = matchedLead.Id;
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
}