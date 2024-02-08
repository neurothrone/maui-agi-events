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
    private async Task NavigateToLeadDetail()
    {
        await Shell.Current.GoToAsync(nameof(LeadDetailPage));
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync(
            $"{nameof(AddLeadPage)}?EventId={Id}");
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