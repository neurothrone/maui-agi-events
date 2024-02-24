using AGIEvents.Lib.Domain;
using AGIEvents.Lib.Services.Database.Domain;
using SQLite;

namespace AGIEvents.Lib.Services.Database;

public class DatabaseRepository : IDatabaseRepository
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private SQLiteAsyncConnection _database;
#pragma warning restore CS8618

    private async Task Init()
    {
        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (_database is not null)
            return;

        _database = new SQLiteAsyncConnection(
            DatabaseConstants.DatabasePath,
            DatabaseConstants.Flags
        );

        await _database.CreateTableAsync<EventEntity>();
        await _database.CreateTableAsync<LeadEntity>();
    }

    public async Task SaveEventAsync(EventRecordDto record)
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

    public async Task<List<EventRecordDto>> FetchEventsAsync()
    {
        await Init();

        var entities = await _database.Table<EventEntity>().ToListAsync();
        return entities
            .Select(e => new EventRecordDto(e.EventId, e.Title, e.Image, e.StartDate, e.EndDate))
            .ToList();
    }

    public async Task<LeadDetailRecordDto> SaveLeadAsync(LeadDetailRecordDto record)
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

    public async Task<LeadDetailRecordDto?> FetchLeadDetailByIdAsync(int leadId)
    {
        await Init();

        var entity = await _database.Table<LeadEntity>().FirstOrDefaultAsync(lead => lead.LeadId == leadId);
        if (entity is null)
            return null;

        return new LeadDetailRecordDto(
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
            entity.LeadId
        );
    }

    public async Task<List<LeadItemRecordDto>> FetchLeadsByEventIdAsync(string eventId)
    {
        await Init();

        var entities = await _database.Table<LeadEntity>()
            .Where(lead => lead.EventId == eventId)
            .ToListAsync();

        return entities
            .Select(lead => new LeadItemRecordDto(
                lead.EventId,
                lead.FirstName,
                lead.LastName,
                lead.Company,
                lead.ScannedDate,
                lead.LeadId)
            )
            .ToList();
    }

    public async Task<List<LeadDetailRecordDto>> FetchDetailedLeadsByEventIdAsync(string eventId)
    {
        await Init();

        var entities = await _database.Table<LeadEntity>()
            .Where(lead => lead.EventId == eventId)
            .ToListAsync();

        return entities
            .Select(lead => new LeadDetailRecordDto(
                lead.EventId,
                lead.FirstName,
                lead.LastName,
                lead.Company,
                lead.Email,
                lead.Phone,
                lead.Address,
                lead.ZipCode,
                lead.City,
                lead.Product,
                lead.Seller,
                lead.Notes,
                lead.ScannedDate,
                lead.LeadId)
            )
            .ToList();
    }

    public async Task UpdateLeadAsync(LeadDetailRecordDto record)
    {
        await Init();

        await _database.UpdateAsync(
            new LeadEntity
            {
                LeadId = record.LeadId,
                EventId = record.EventId,
                FirstName = record.FirstName,
                LastName = record.LastName,
                Company = record.Company,
                Email = record.Email,
                Phone = record.Phone,
                Address = record.Address,
                ZipCode = record.ZipCode,
                City = record.City,
                Product = record.Product,
                Seller = record.Seller,
                Notes = record.Notes,
                ScannedDate = record.ScannedDate
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