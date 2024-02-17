using AGIEvents.Lib.Services.Database.DTO;

namespace AGIEvents.Lib.Services.Database;

public interface IDatabaseRepository
{
    Task SaveEventAsync(EventRecord record);
    Task<List<EventRecord>> FetchEventsAsync();

    Task SaveLeadAsync(LeadRecord record);
    Task<List<LeadRecord>> FetchLeadsByEventIdAsync(string eventId);
    Task UpdateLeadAsync(LeadRecord lead);
    Task DeleteLeadAsync(LeadRecord lead);
    Task DeleteLeadsByEventIdAsync(string eventId);

    Task DeleteAllDataAsync();
}