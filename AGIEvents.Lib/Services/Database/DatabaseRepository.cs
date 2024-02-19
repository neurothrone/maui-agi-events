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

    public async Task<LeadRecord> SaveLeadAsync(LeadRecord record)
    {
        await Init();

        var entity = new LeadEntity
        {
            EventId = record.EventId,
            FirstName = record.FirstName,
            LastName = record.LastName,
            Company = record.Company,
            Email = record.Email,
            Phone = record.Phone,
            Address = record.Address,
            ZipCode = record.ZipCode,
            City = record.City,
            ScannedDate = record.ScannedDate
        };
        await _database.InsertAsync(entity);

        return record with { LeadId = entity.LeadId };
    }

    public async Task<LeadRecord?> FetchLeadByIdAsync(int leadId)
    {
        await Init();

        var entity = await _database.Table<LeadEntity>().FirstOrDefaultAsync(lead => lead.LeadId == leadId);
        if (entity is null)
            return null;

        return new LeadRecord(
            entity.EventId,
            entity.FirstName,
            entity.LastName,
            entity.Company,
            entity.Email,
            entity.Phone,
            entity.Address,
            entity.ZipCode,
            entity.City,
            entity.Product,
            entity.Seller,
            entity.Notes,
            entity.ScannedDate,
            entity.LeadId);
    }

    public async Task<List<LeadRecord>> FetchLeadsByEventIdAsync(string eventId)
    {
        await Init();

        var entities = await _database.Table<LeadEntity>()
            .Where(lead => lead.EventId == eventId)
            .ToListAsync();

        return entities
            .Select(l => new LeadRecord(
                l.EventId, l.FirstName, l.LastName, l.Company, l.Email,
                l.Phone, l.Address, l.ZipCode, l.City, l.Product, l.Seller, l.Notes, l.ScannedDate, l.LeadId)
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

    public async Task<bool> DeleteLeadByIdAsync(int leadId)
    {
        await Init();
        return await _database.DeleteAsync<LeadEntity>(leadId) != 0;
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