namespace AGIEvents.Lib.Services.Messages;

public record EventSavedChangedMessage(
    string EventId,
    bool IsSaved
);
