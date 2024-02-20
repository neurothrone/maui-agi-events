using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Messages;

public record LeadAddedManuallyMessage(LeadDetailRecordDto Lead);