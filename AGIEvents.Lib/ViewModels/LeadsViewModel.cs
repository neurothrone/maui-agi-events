using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Diagnostics;
using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class LeadsViewModel :
    ObservableObject,
    IRecipient<LeadUpdatedMessage>,
    IRecipient<LeadAddedManuallyMessage>,
    IRecipient<QrScannedVisitorMessage>,
    IQueryAttributable
{
    private readonly IDatabaseRepository _databaseRepository;
    private readonly INotificationService _notificationService;
    private readonly IShareService _shareService;

    [ObservableProperty] private string _eventId = string.Empty;
    [ObservableProperty] private string _eventTitle = string.Empty;
    [ObservableProperty] private string _eventImage = string.Empty;
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<LeadItemViewModel> _leads = [];

    public ObservableCollection<LeadItemViewModel> Leads
    {
        get => _leads;
        set => SetProperty(ref _leads, value);
    }

    public LeadsViewModel(
        IDatabaseRepository databaseRepository,
        INotificationService notificationService,
        IShareService shareService)
    {
        _databaseRepository = databaseRepository;
        _notificationService = notificationService;
        _shareService = shareService;
        SubscribeToMessages();
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register<LeadUpdatedMessage>(this);
        WeakReferenceMessenger.Default.Register<LeadAddedManuallyMessage>(this);
        WeakReferenceMessenger.Default.Register<QrScannedVisitorMessage>(this);
    }

    async void IRecipient<LeadUpdatedMessage>.Receive(LeadUpdatedMessage message)
    {
        // TODO: extract out to separate method UpdateLead()
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

    void IRecipient<LeadAddedManuallyMessage>.Receive(LeadAddedManuallyMessage message)
    {
        SaveLead(message.Lead);
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

    void IRecipient<QrScannedVisitorMessage>.Receive(QrScannedVisitorMessage message)
    {
        var scannedLead = message.Visitor;
        var leadDetailRecord = new LeadDetailRecordDto(
            EventId,
            scannedLead.FirstName,
            scannedLead.LastName,
            scannedLead.Company,
            scannedLead.Email,
            scannedLead.Phone,
            scannedLead.Address,
            scannedLead.ZipCode,
            scannedLead.City,
            string.Empty,
            string.Empty,
            string.Empty,
            DateTime.Now);
        SaveLead(leadDetailRecord);
    }

    private async void SaveLead(LeadDetailRecordDto leadToSave)
    {
        if (IsThisLeadSaved(leadToSave))
        {
            await _notificationService.ShowNotificationAsync("Uh Oh!", "You have already added this Lead", "OK");
            return;
        }

        // TODO: can just return the LeadId since that is all that is new
        var savedLeadDetailRecord = await _databaseRepository.SaveLeadAsync(leadToSave);

        var leadItemRecord = LeadItemRecordDto.FromLeadDetailRecord(savedLeadDetailRecord);
        var newLead = LeadItemViewModel.FromLeadItemRecord(leadItemRecord);

        await MainThread.InvokeOnMainThreadAsync(() => Leads.Insert(0, newLead));
    }

    private bool IsThisLeadSaved(ILeadHashable leadToCheck)
    {
        var searchHash = ILeadHashable.GetHash(leadToCheck);

        foreach (var lead in Leads)
        {
            // Already in list
            if (ILeadHashable.GetHash(lead) == searchHash)
                return true;
        }

        return false;
    }

    private async void LoadEventInfo(EventRecordDto eventRecord)
    {
        // TODO: just store record?
        EventId = eventRecord.EventId;
        EventTitle = eventRecord.Title;
        EventImage = eventRecord.Image;

        await FetchLeads();
    }

    private async Task FetchLeads()
    {
        IsLoading = true;

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

    private async Task NavigateToQrScanner(string eventId)
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.QrScannerPage),
            new Dictionary<string, object>
            {
                { nameof(EventViewModel.EventId), eventId },
                { nameof(ParticipantType), ParticipantType.Visitor }
            });
    }

    private async Task<bool> CanDeleteLead()
    {
        var isSafeDeleteOn = Preferences.Get(nameof(SettingsViewModel.IsSafeDeleteOn), false);
        if (!isSafeDeleteOn)
            return true;

        return await _notificationService.ShowConfirmationPromptAsync(
            "Delete Lead",
            "Are you sure you want to delete this Lead?");
    }

    [RelayCommand]
    private async Task DeleteLead(LeadItemViewModel lead)
    {
        var canDeleteLead = await CanDeleteLead();
        if (!canDeleteLead)
            return;

        var wasDeleted = await _databaseRepository.DeleteLeadByIdAsync(lead.LeadId);

        if (!wasDeleted)
        {
            Console.WriteLine("❌ -> Failed to Delete Lead by Id");
            return;
        }

        await MainThread.InvokeOnMainThreadAsync(() => Leads.Remove(lead));
    }

    [RelayCommand]
    private async Task ExportLead(LeadItemViewModel lead)
    {
        var leadDetailRecord = await _databaseRepository.FetchLeadDetailByIdAsync(lead.LeadId);

        if (leadDetailRecord is null)
        {
            Debug.WriteLine("❌ -> Failed to fetch lead from database.");
            return;
        }

        var fileName = $"AGI Events {EventTitle} Exported Lead.csv";
        await _shareService.ExportLead(fileName, leadDetailRecord);
    }

    [RelayCommand]
    private async Task ExportLeads()
    {
        // TODO: IsLoading = true?
        var records = await _databaseRepository.FetchDetailedLeadsByEventIdAsync(EventId);
        var fileName = $"AGI Events {EventTitle} Exported Leads.csv";
        await _shareService.ExportLeads(fileName, records.ToArray());
    }

    [RelayCommand]
    private async Task ShowQrScanner()
    {
        await NavigateToQrScanner(EventId);
    }

    [RelayCommand]
    private async Task NavigateToLeadDetail(LeadItemViewModel lead)
    {
        var record = LeadItemRecordDto.FromViewModel(lead);
        await Shell.Current.GoToAsync($"{nameof(AppRoute.LeadDetailPage)}",
            new Dictionary<string, object> { { nameof(LeadItemRecordDto), record } });
    }

    [RelayCommand]
    private async Task NavigateToAddLead()
    {
        await Shell.Current.GoToAsync($"{nameof(AppRoute.AddLeadPage)}?{nameof(EventViewModel.EventId)}={EventId}");
    }
}