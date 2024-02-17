namespace AGIEvents.Lib.Messages;

public record QrScannerCompletedMessage(string QrCode);

public record QrScannerFailedMessage();

public record QrScannerDetectionEnabledMessage();