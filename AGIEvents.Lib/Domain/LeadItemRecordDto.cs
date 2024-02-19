using AGIEvents.Lib.ViewModels;

namespace AGIEvents.Lib.Domain;

public record LeadItemRecordDto(
    string EventId,
    string FirstName,
    string LastName,
    string Company,
    DateTime ScannedDate,
    int LeadId = -1
)
{
    public static LeadItemRecordDto FromViewModel(LeadItemViewModel leadItem)
    {
        return new LeadItemRecordDto(
            leadItem.EventId,
            leadItem.FirstName,
            leadItem.LastName,
            leadItem.Company,
            leadItem.ScannedDate,
            leadItem.LeadId);
    }

    public static LeadItemRecordDto FromLeadDetailRecord(LeadDetailRecordDto lead)
    {
        return new LeadItemRecordDto(
            lead.EventId,
            lead.FirstName,
            lead.LastName,
            lead.Company,
            lead.ScannedDate,
            lead.LeadId);
    }
};