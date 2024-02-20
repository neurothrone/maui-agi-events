using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public interface ICsvService
{
    IEnumerable<string> ConvertLeadsToCsvData(LeadDetailRecordDto[] leads);
}