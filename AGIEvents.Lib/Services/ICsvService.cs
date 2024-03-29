using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public interface ICsvService
{
    Task WriteLeadsToFile(string filePath, IEnumerable<LeadDetailRecordDto> leads);
    Task WriteLeadToFile(string filePath, LeadDetailRecordDto lead);
}