namespace AGIEvents.Lib.Services.Messages;

public record QrScannerCompletedMessage(string QrCode);

public record QrScannerFailedMessage();

public record QrScannerDetectionEnabledMessage();