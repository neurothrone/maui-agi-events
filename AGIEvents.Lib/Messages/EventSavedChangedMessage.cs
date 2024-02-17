namespace AGIEvents.Lib.Messages;

public record EventSavedChangedMessage(
    string EventId,
    bool IsSaved
);