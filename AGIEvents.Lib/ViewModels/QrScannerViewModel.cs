using AGIEvents.Lib.Services.Messages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class QrScannerViewModel : ObservableObject
{
    private const int QrCodeRequiredLength = 40;

    [ObservableProperty] private bool _isLoading = false;
    [ObservableProperty] private bool _isDetecting = true;

    public QrScannerViewModel()
    {
        SubscribeToMessages();
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register<QrScannerDetectionEnabledMessage>(
            this,
            (_, _) => IsDetecting = true
        );
    }


    [RelayCommand]
    private void QrCodeScanned(string qrCode)
    {
        IsDetecting = false;

        if (qrCode.Length != QrCodeRequiredLength)
        {
            Console.WriteLine("❌ -> Invalid QR Code.");

            WeakReferenceMessenger.Default.Send(new QrScannerFailedMessage());
            return;
        }

        WeakReferenceMessenger.Default.Send(new QrScannerCompletedMessage(qrCode));
        MainThread.BeginInvokeOnMainThread(() => Shell.Current.Navigation.PopAsync());
    }
}