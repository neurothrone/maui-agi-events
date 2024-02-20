using ZXing.Net.Maui;

namespace AGIEvents.App.Views.Scanner;

public partial class QrScannerPage : ContentPage
{
    public QrScannerPage(Lib.ViewModels.QrScannerViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;

        InitBarcodeReader();
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
}