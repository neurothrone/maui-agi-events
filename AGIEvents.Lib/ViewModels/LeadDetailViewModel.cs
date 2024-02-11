using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadDetailViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private string _leadId;
    [ObservableProperty] private string _firstName;
    [ObservableProperty] private string _lastName;
    [ObservableProperty] private string _company;
    [ObservableProperty] private string _email;
    [ObservableProperty] private string _phone;

    [ObservableProperty] private string _address;
    [ObservableProperty] private string _zipCode;
    [ObservableProperty] private string _city;
    [ObservableProperty] private string _product;
    [ObservableProperty] private string _seller;

    [ObservableProperty] private string _notes;
    [ObservableProperty] private DateTime _scannedAt;

    [RelayCommand]
    private async Task SaveChanges()
    {
        // TODO: save changes to database
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task OpenPhoneDialer()
    {
        Console.WriteLine("✅ -> Open Phone Dialer");
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
        Company = matchedLead.company;
        Email = matchedLead.email;
        Phone = matchedLead.phone;

        Address = matchedLead.address;
        ZipCode = matchedLead.zipCode;
        City = matchedLead.city;
        Product = matchedLead.product;
        Seller = matchedLead.seller;

        Notes = matchedLead.notes;
        ScannedAt = matchedLead.scannedAt;
    }
}