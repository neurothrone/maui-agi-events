using AGIEvents.Lib.Services.Database.DTO;

namespace AGIEvents.Lib.Services.Database;

public interface IDatabaseRepository
{
    Task SaveEventAsync(EventRecord record);
    Task<List<EventRecord>> FetchEventsAsync();

    Task<LeadRecord> SaveLeadAsync(LeadRecord record);
    Task<LeadRecord?> FetchLeadByIdAsync(int leadId);
    Task<List<LeadRecord>> FetchLeadsByEventIdAsync(string eventId);
    Task UpdateLeadAsync(LeadRecord lead);
    Task<bool> DeleteLeadByIdAsync(int leadId);
    Task DeleteLeadsByEventIdAsync(string eventId);

    Task DeleteAllDataAsync();
}