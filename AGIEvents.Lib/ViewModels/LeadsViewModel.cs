using System.Collections.ObjectModel;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services.Database;
using AGIEvents.Lib.Services.Database.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadsViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDatabaseRepository _databaseRepository;

    [ObservableProperty] private string _eventId = string.Empty;
    [ObservableProperty] private string _eventImage = string.Empty;
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<LeadViewModel> _leads = [];

    public ObservableCollection<LeadViewModel> Leads
    {
        get => _leads;
        set => SetProperty(ref _leads, value);
    }

    public LeadsViewModel(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue(nameof(EventRecord), out var value))
            return;

        if (value is EventRecord record)
            LoadEventInfo(record);
    }

    private async void LoadEventInfo(EventRecord record)
    {
        EventId = record.EventId;
        EventImage = record.Image;

        await FetchLeads();
    }

    private async Task FetchLeads()
    {
        IsLoading = true;

        var leadsFromDatabase = await _databaseRepository.FetchLeadsByEventIdAsync(EventId);
        var leadsViewModels = leadsFromDatabase
            .Select(LeadViewModel.FromRecord)
            .OrderBy(lead => lead.ScannedDate)
            .ToList();

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            IsLoading = false;
            Leads = new ObservableCollection<LeadViewModel>(leadsViewModels);
        });
    }

    [RelayCommand]
    private async Task ShowQrScanner()
    {
        // var randomLead 
        var record = new LeadRecord(
            EventId,
            "Jane",
            "Doe",
            "Doe Industries",
            "jane.doe@example.com",
            "+46 123 456 78 90",
            "Some st. 49",
            "123 45",
            "Gothenburg",
            "Nothing",
            "Captain Crunch",
            "I have no notes!",
            DateTime.Now
        );
        var savedRecord = await _databaseRepository.SaveLeadAsync(record);

        // Insert to top of List as we are Ordering by Ascending (ScannedDate)
        await MainThread.InvokeOnMainThreadAsync(() => Leads.Insert(0, LeadViewModel.FromRecord(savedRecord)));
    }

    [RelayCommand]
    private async Task DeleteLead(LeadViewModel lead)
    {
        var wasDeleted = await _databaseRepository.DeleteLeadByIdAsync(lead.LeadId);

        if (!wasDeleted)
        {
            Console.WriteLine("❌ -> Failed to Delete Lead by Id");
            return;
        }

        await MainThread.InvokeOnMainThreadAsync(() => Leads.Remove(lead));
    }

    [RelayCommand]
    private async Task ExportLead(LeadViewModel lead)
    {
        // TODO: Export single lead feature
        Console.WriteLine($"✅ -> Exporting lead with id: {lead.LeadId}");
        await Task.Delay(100);
    }

    [RelayCommand]
    private async Task ExportLeads()
    {
        // TODO: Export leads feature
        await Task.Delay(100);
    }

    // TODO: refactor to use LeadItemRecord (List Item)
    // TODO: Add LeadDetailRecord (Detail View)

    [RelayCommand]
    private async Task NavigateToLeadDetail(LeadViewModel lead)
    {
        await Shell.Current.GoToAsync($"{nameof(AppRoute.LeadDetailPage)}?LeadId={lead.LeadId}");
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync($"{nameof(AppRoute.AddLeadPage)}?EventId={EventId}");
    }
}