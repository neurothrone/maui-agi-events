using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public class ShareService : IShareService
{
    private readonly ICsvService _csvService;

    public ShareService(ICsvService csvService)
    {
        _csvService = csvService;
    }

    public async Task ShareLeads(string fileName, LeadDetailRecordDto[] leads)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);
        var csvData = _csvService.ConvertLeadsToCsvData(leads);

        await File.WriteAllLinesAsync(filePath, csvData);
        await ShareFile("Export Leads", filePath);

        File.Delete(filePath);
    }

    private async Task ShareFile(string title, string filePath)
    {
        await Share.Default.RequestAsync(new ShareFileRequest
        {
            Title = title,
            File = new ShareFile(filePath)
        });
    }
}