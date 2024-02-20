using System.Diagnostics;
using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Firebase.Domain;
using AGIEvents.Lib.Services.Realtime;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class QrScannerViewModel : ObservableObject, IQueryAttributable
{
    private readonly INotificationService _notificationService;
    private readonly IRealtimeService _realtimeService;
    private const int QrCodeRequiredLength = 40;

    private string _eventId = string.Empty;
    private ParticipantType _participantType = ParticipantType.Unknown;

    private bool _isParticipantScanOk;
    [ObservableProperty] private bool _isDetecting = true;

    public QrScannerViewModel(
        INotificationService notificationService,
        IRealtimeService realtimeService)
    {
        _notificationService = notificationService;
        _realtimeService = realtimeService;
    }

    void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.TryGetValue("EventId", out var eventIdValue))
        {
            Debug.WriteLine("❌ -> Failed to Get EventId Value from Query params");
            return;
        }

        if (!query.TryGetValue(nameof(ParticipantType), out var participantValue))
        {
            Debug.WriteLine("❌ -> Failed to Get ParticipantType from Query params");
            return;
        }

        if (eventIdValue is string eventId)
            _eventId = eventId;

        if (participantValue is ParticipantType participantType)
            _participantType = participantType;

        query.Clear();
    }

    [RelayCommand]
    private async Task QrCodeScanned(string qrCode)
    {
        IsDetecting = false;

        if (qrCode.Length != QrCodeRequiredLength)
        {
            await _notificationService.ShowNotificationAsync("Uh Oh!", "Invalid QR Code", "OK");
            IsDetecting = true;
            return;
        }

        await RequestFromFirebase(qrCode);

        if (_isParticipantScanOk)
            await MainThread.InvokeOnMainThreadAsync(() => Shell.Current.GoToAsync(".."));
        else
            IsDetecting = true;
    }

    private async Task RequestFromFirebase(string qrCode)
    {
        if (_participantType == ParticipantType.Exhibitor)
        {
            var response = await _realtimeService.FetchExhibitorById(qrCode, _eventId);
            if (response.errorMessage is not null)
            {
                await _notificationService.ShowNotificationAsync("Uh Oh!", response.errorMessage, "OK");
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new QrScannedExhibitorMessage(_eventId));
                _isParticipantScanOk = true;
            }
        }
        else
        {
            var response = await _realtimeService.FetchVisitorById(qrCode, _eventId);
            if (response.errorMessage is not null)
            {
                await _notificationService.ShowNotificationAsync("Uh Oh!", response.errorMessage, "OK");
            }
            else
            {
                WeakReferenceMessenger.Default.Send(new QrScannedVisitorMessage(response.visitor ?? new Visitor()));
                _isParticipantScanOk = true;
            }
        }
    }
}