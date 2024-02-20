using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public interface IShareService
{
    Task ShareLeads(string fileName, LeadDetailRecordDto[] leads);
}