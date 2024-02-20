using AGIEvents.Lib.Services.Firebase.Domain;

namespace AGIEvents.Lib.Messages;

public record QrScannerFailedMessage(string Error);

public record QrScannedExhibitorMessage(string EventId);

public record QrScannedVisitorMessage(Visitor Visitor);