using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public class ShareService(ICsvService csvService) : IShareService
{
    public async Task ShareLeads(string fileName, LeadDetailRecordDto[] leads)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        await csvService.WriteLeadsToFile(filePath, leads);
        await ShareFile("Share Leads", filePath);

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