using System.Collections.ObjectModel;
using AGIEvents.App.Models;
using AGIEvents.App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.App.ViewModels;

internal partial class LeadsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty] private string _id = string.Empty;
    [ObservableProperty] private string _image = string.Empty;
    public ObservableCollection<LeadViewModel> Leads { get; }

    public LeadsViewModel()
    {
        Leads = new ObservableCollection<LeadViewModel>(
            Lead.Samples()
                .Select(lead => new LeadViewModel(lead))
                .OrderByDescending(lead => lead.ScannedAt)
                .ToList()
        );
    }

    [RelayCommand]
    private async Task NavigateToLeadDetail(LeadViewModel lead)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(LeadDetailPage)}?LeadId={lead.Id}"
        );
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync(
            $"{nameof(AddLeadPage)}?EventId={Id}");
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