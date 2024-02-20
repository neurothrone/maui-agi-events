using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services.Database;

public interface IDatabaseRepository
{
    Task SaveEventAsync(EventRecordDto record);
    Task<List<EventRecordDto>> FetchEventsAsync();

    Task<LeadDetailRecordDto> SaveLeadAsync(LeadDetailRecordDto record);
    Task<LeadDetailRecordDto?> FetchLeadDetailByIdAsync(int leadId);
    Task<List<LeadItemRecordDto>> FetchLeadsByEventIdAsync(string eventId);
    Task<List<LeadDetailRecordDto>> FetchDetailedLeadsByEventIdAsync(string eventId);
    Task UpdateLeadAsync(LeadDetailRecordDto record);
    Task<bool> DeleteLeadByIdAsync(int leadId);
    Task DeleteLeadsByEventIdAsync(string eventId);

    Task DeleteAllDataAsync();
}