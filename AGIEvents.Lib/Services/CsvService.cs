using System.Globalization;
using AGIEvents.Lib.Domain;
using CsvHelper;
using CsvHelper.Configuration;

namespace AGIEvents.Lib.Services;

public class CsvService : ICsvService
{
    async Task ICsvService.WriteLeadsToFile(string filePath, IEnumerable<LeadDetailRecordDto> leads)
    {
        await WriteLeadsToCsvFile(filePath, leads);
    }

    async Task ICsvService.WriteLeadToFile(string filePath, LeadDetailRecordDto lead)
    {
        await WriteLeadsToCsvFile(filePath, new[] { lead });
    }

    private async Task WriteLeadsToCsvFile(string filePath, IEnumerable<LeadDetailRecordDto> leads)
    {
        await using var writer = new StreamWriter(filePath);
        await using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.Context.RegisterClassMap<LeadMap>();
        await csv.WriteRecordsAsync(leads);
    }
}

internal sealed class LeadMap : ClassMap<LeadDetailRecordDto>
{
    private LeadMap()
    {
        Map(l => l.Email).Index(0).Name("E-mail");
        Map(l => l.FirstName).Index(1).Name("First Name");
        Map(l => l.LastName).Index(2).Name("Last Name");
        Map(l => l.Company).Index(3).Name("Company");
        Map(l => l.Phone).Index(4).Name("Phone Number");
        Map(l => l.Address).Index(5).Name("Address");
        Map(l => l.ZipCode).Index(6).Name("Zip Code");
        Map(l => l.City).Index(7).Name("City");
        Map(l => l.Product).Index(8).Name("Product(s)");
        Map(l => l.Seller).Index(9).Name("Seller");
        Map(l => l.Notes).Index(10).Name("Notes");
        Map(l => l.FormattedScannedDate).Index(11).Name("Scanned Date");
    }
}