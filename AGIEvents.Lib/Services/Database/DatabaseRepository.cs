using AGIEvents.Lib.Services.Database.Domain;
using AGIEvents.Lib.Services.Database.DTO;
using SQLite;

namespace AGIEvents.Lib.Services.Database;

public class DatabaseRepository : IDatabaseRepository
{
    private SQLiteAsyncConnection _database;

    private async Task Init()
    {
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(
            DatabaseConstants.DatabasePath,
            DatabaseConstants.Flags
        );
        
        await _database.CreateTableAsync<EventEntity>();
        await _database.CreateTableAsync<LeadEntity>();
    }

    public async Task SaveEventAsync(EventRecord record)
    {
        await Init();

        var entity = new EventEntity
        {
            EventId = record.EventId,
            Title = record.Title,
            Image = record.Image,
            StartDate = record.StartDate,
            EndDate = record.EndDate
        };
        await _database.InsertAsync(entity);
    }

    public async Task<List<EventRecord>> FetchEventsAsync()
    {
        await Init();

        var entities = await _database.Table<EventEntity>().ToListAsync();
        return entities
            .Select(e => new EventRecord(e.EventId, e.Title, e.Image, e.StartDate, e.EndDate))
            .OrderBy(e => e.StartDate)
            .ToList();
    }

    public async Task SaveLeadAsync(LeadRecord record)
    {
        await Init();
    }

    public async Task<List<LeadRecord>> FetchLeadsByEventIdAsync(string eventId)
    {
        await Init();

        var entities = await _database.Table<LeadEntity>()
            .Where(lead => lead.EventId == eventId)
            .ToListAsync();
        return entities
            .Select(e => new LeadRecord(
                e.LeadId, e.EventId, e.FirstName, e.LastName, e.Company, e.Email,
                e.Phone, e.Address, e.ZipCode, e.City, e.ScannedDate)
            )
            .ToList();
    }

    public async Task UpdateLeadAsync(LeadRecord lead)
    {
        await Init();

        await _database.UpdateAsync(
            new LeadEntity
            {
                LeadId = lead.LeadId, // TODO: does this need to be updated?
                FirstName = lead.FirstName,
                LastName = lead.LastName,
                Company = lead.Company,
                Email = lead.Email,
                Phone = lead.Phone,
                Address = lead.Address,
                ZipCode = lead.ZipCode,
                City = lead.City,
                ScannedDate = lead.ScannedDate
            }
        );
    }

    public async Task DeleteLeadAsync(LeadRecord lead)
    {
        await Init();
        await _database.DeleteAsync<LeadEntity>(lead.LeadId);
    }


    public async Task DeleteLeadsByEventIdAsync(string eventId)
    {
        await Init();
        await _database.ExecuteAsync("DELETE FROM Leads WHERE EventId = ?", eventId);
    }

    public async Task DeleteAllDataAsync()
    {
        await Init();
        
        await _database.DeleteAllAsync<LeadEntity>();
        await _database.DeleteAllAsync<EventEntity>();
    }
}