using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public class CsvService : ICsvService
{
    IEnumerable<string> ICsvService.ConvertLeadsToCsvData(LeadDetailRecordDto[] leads)
    {
        var lines = new List<string>();

        // Use the name of each property as column headers
        var header = string.Join(",", typeof(LeadDetailRecordDto)
            .GetProperties()
            .Select(property => property.Name)
            .ToArray());
        lines.Add(header);

        // Use the value of each property as values
        foreach (var lead in leads)
        {
            var values = string.Join(",", typeof(LeadDetailRecordDto)
                .GetProperties()
                .Select(property => property.GetValue(lead)));

            lines.Add(values);
        }

        return lines.AsReadOnly();
    }
}