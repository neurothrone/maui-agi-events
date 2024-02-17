using AGIEvents.Lib.Messages;
using CommunityToolkit.Mvvm.Messaging;
using ZXing.Net.Maui;

namespace AGIEvents.App.Views.Scanner;

public partial class QrScannerPage : ContentPage
{
    public QrScannerPage(Lib.ViewModels.QrScannerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        InitBarcodeReader();
        SubscribeToMessages();
    }

    private void InitBarcodeReader()
    {
        BarcodeReader.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = false,
            TryHarder = true
        };
    }

    private void SubscribeToMessages()
    {
        WeakReferenceMessenger.Default.Register<QrScannerFailedMessage>(
            this,
            (_, _) => DisplayFailedAlert()
        );
    }

    private async void DisplayFailedAlert()
    {
        await Dispatcher.DispatchAsync(
            async () =>
            {
                await DisplayAlert("Invalid QR Code", "Try again", "OK");
                WeakReferenceMessenger.Default.Send(new QrScannerDetectionEnabledMessage());
            }
        );
    }
}