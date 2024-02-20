using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public interface IShareService
{
    Task ExportLeads(string fileName, LeadDetailRecordDto[] leads);
    Task ExportLead(string fileName, LeadDetailRecordDto lead);
}