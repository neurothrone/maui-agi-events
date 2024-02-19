using System.Collections.ObjectModel;
using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadsViewModel :
    ObservableObject,
    IRecipient<LeadUpdatedMessage>,
    IQueryAttributable
{
    private readonly IDatabaseRepository _databaseRepository;

    [ObservableProperty] private string _eventId = string.Empty;
    [ObservableProperty] private string _eventImage = string.Empty;
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<LeadItemViewModel> _leads = [];

    public ObservableCollection<LeadItemViewModel> Leads
    {
        get => _leads;
        set => SetProperty(ref _leads, value);
    }

    public LeadsViewModel(IDatabaseRepository databaseRepository)
    {
        _databaseRepository = databaseRepository;
        SubscribeToMessages();
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    async void IRecipient<LeadUpdatedMessage>.Receive(LeadUpdatedMessage message)
    {
        var updatedLeadId = message.LeadId;
        var updatedLead = await _databaseRepository.FetchLeadDetailByIdAsync(updatedLeadId);
        if (updatedLead is null)
        {
            Console.WriteLine("❌ -> updatedLead from Database is null.");
            return;
        }

        var leadToUpdate = Leads.FirstOrDefault(lead => lead.LeadId == updatedLeadId);
        if (leadToUpdate is null)
        {
            Console.WriteLine("❌ -> Could not find Lead to update inside Leads.");
            return;
        }

        var index = Leads.IndexOf(leadToUpdate);

        var itemRecord = LeadItemRecordDto.FromLeadDetailRecord(updatedLead);
        var updatedViewModel = LeadItemViewModel.FromLeadItemRecord(itemRecord);

        await MainThread.InvokeOnMainThreadAsync(() => Leads[index] = updatedViewModel);
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue(nameof(EventRecordDto), out var value))
            return;

        if (value is EventRecordDto record)
            LoadEventInfo(record);

        // It is necessary to clear the query from memory so that when navigating back from
        // the LeadDetailPage the method `LoadEventInfo()` does not execute again.
        query.Clear();
    }

    private async void LoadEventInfo(EventRecordDto recordDto)
    {
        EventId = recordDto.EventId;
        EventImage = recordDto.Image;

        await FetchLeads();
    }

    private async Task FetchLeads()
    {
        IsLoading = true;

        // TODO: allow database to do Ordering?
        // Order By Descending (ScannedDate) to show the latest who was scanned
        var leadsFromDatabase = await _databaseRepository.FetchLeadsByEventIdAsync(EventId);
        var leadsViewModels = leadsFromDatabase
            .Select(LeadItemViewModel.FromLeadItemRecord)
            .OrderByDescending(lead => lead.ScannedDate)
            .ToList();

        await MainThread.InvokeOnMainThreadAsync(() =>
        {
            IsLoading = false;
            Leads = new ObservableCollection<LeadItemViewModel>(leadsViewModels);
        });
    }

    [RelayCommand]
    private async Task ShowQrScanner()
    {
        // TODO: testing purposes
        var record = new LeadDetailRecordDto(
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
        var itemRecord = LeadItemRecordDto.FromLeadDetailRecord(savedRecord);

        // Insert to top of List as we are Ordering by Ascending (ScannedDate)
        await MainThread.InvokeOnMainThreadAsync(
            () => Leads.Insert(0, LeadItemViewModel.FromLeadItemRecord(itemRecord)));
    }

    [RelayCommand]
    private async Task DeleteLead(LeadItemViewModel leadItem)
    {
        var wasDeleted = await _databaseRepository.DeleteLeadByIdAsync(leadItem.LeadId);

        if (!wasDeleted)
        {
            Console.WriteLine("❌ -> Failed to Delete Lead by Id");
            return;
        }

        await MainThread.InvokeOnMainThreadAsync(() => Leads.Remove(leadItem));
    }

    [RelayCommand]
    private async Task ExportLead(LeadItemViewModel leadItem)
    {
        // TODO: Export single lead feature
        Console.WriteLine($"✅ -> Exporting lead with id: {leadItem.LeadId}");
        await Task.Delay(100);
    }

    [RelayCommand]
    private async Task ExportLeads()
    {
        // TODO: Export leads feature
        Console.WriteLine($"✅ -> Exporting leads");
        await Task.Delay(100);
    }

    [RelayCommand]
    private async Task NavigateToLeadDetail(LeadItemViewModel leadItem)
    {
        var record = LeadItemRecordDto.FromViewModel(leadItem);
        await Shell.Current.GoToAsync($"{nameof(AppRoute.LeadDetailPage)}",
            new Dictionary<string, object> { { nameof(LeadItemRecordDto), record } });
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync($"{nameof(AppRoute.AddLeadPage)}?{nameof(EventViewModel.EventId)}={EventId}");
    }
}