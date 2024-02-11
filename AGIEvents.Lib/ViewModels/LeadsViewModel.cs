using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private string _id = string.Empty;
    [ObservableProperty] private string _image = string.Empty;
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<LeadViewModel> _leads;

    public ObservableCollection<LeadViewModel> Leads
    {
        get => _leads;
        set => SetProperty(ref _leads, value);
    }

    public LeadsViewModel()
    {
        _leads = [];
        FetchLeads();
    }

    private async void FetchLeads()
    {
        IsLoading = true;
        // Sleep for 2 sec
        await Task.Delay(2000);

        Leads = new ObservableCollection<LeadViewModel>(
            Lead.Samples()
                .Select(lead => new LeadViewModel(lead))
                .OrderByDescending(lead => lead.ScannedAt)
                .ToList()
        );

        IsLoading = false;
    }

    [RelayCommand]
    private async Task NavigateToLeadDetail(LeadViewModel lead)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(AppRoute.LeadDetailPage)}?LeadId={lead.Id}"
        );
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync(
            $"{nameof(AppRoute.AddLeadPage)}?EventId={Id}");
    }

    [RelayCommand]
    private async Task ShowQrScanner()
    {
        Leads.Add(
            new LeadViewModel(
                new Lead(
                    id: "1",
                    eventId: "0",
                    firstName: "Jane",
                    lastName: "Doe",
                    company: "Doe Industries",
                    email: "jane.doe@example.com",
                    phone: "+46 123 456 78 90",
                    scannedAt: DateTime.Now
                )
            )
        );
    }

    [RelayCommand]
    private async Task ExportLead(LeadViewModel lead)
    {
        Console.WriteLine($"✅ -> Exporting lead with id: {lead.Id}");
    }

    [RelayCommand]
    private async Task DeleteLead(LeadViewModel lead)
    {
        Console.WriteLine($"✅ -> Deleting lead with id: {lead.Id}");
    }

    [RelayCommand]
    private async Task ExportLeads()
    {
        // TODO: Export leads feature
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("EventId", out var value))
            return;

        LoadEventInfo(value.ToString() ?? "");
    }

    private async void LoadEventInfo(string eventId)
    {
        // TODO: load Event from database
        var matchedEvent = Event.Samples().FirstOrDefault((n) => n.id == eventId);
        if (matchedEvent == null)
            return;

        Id = matchedEvent.id;
        Image = matchedEvent.image;

        // TODO: load leads from database
    }
}