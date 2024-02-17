using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private string _eventId = string.Empty;
    [ObservableProperty] private string _eventImage = string.Empty;
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<LeadViewModel> _leads = [];

    public ObservableCollection<LeadViewModel> Leads
    {
        get => _leads;
        set => SetProperty(ref _leads, value);
    }

    public LeadsViewModel()
    {
        FetchLeads();
    }

    private async void FetchLeads()
    {
        IsLoading = true;
        // Sleep for 1 sec
        await Task.Delay(1000);

        Leads = new ObservableCollection<LeadViewModel>(
            Lead.Samples()
                .Select(lead => new LeadViewModel(lead))
                .OrderByDescending(lead => lead.ScannedDate)
                .ToList()
        );

        IsLoading = false;
    }

    [RelayCommand]
    private async Task NavigateToLeadDetail(LeadViewModel lead)
    {
        await Shell.Current.GoToAsync($"{nameof(AppRoute.LeadDetailPage)}?LeadId={lead.Id}");
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync($"{nameof(AppRoute.AddLeadPage)}?EventId={EventId}");
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
                    scannedDate: DateTime.Now
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
        if (!query.TryGetValue(nameof(EventViewModel.EventId), out var value))
            return;

        var eventId = value.ToString() ?? string.Empty;
        LoadEventInfo(eventId);
    }

    private async void LoadEventInfo(string eventId)
    {
        // TODO: load Event from database, load by id not eventId
        var matchedEvent = EventViewModel.Samples().FirstOrDefault(e => e.EventId == eventId);
        if (matchedEvent == null)
            return;

        EventId = matchedEvent.EventId;
        EventImage = matchedEvent.Image;

        // TODO: load leads from database
    }
}