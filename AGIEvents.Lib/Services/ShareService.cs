using AGIEvents.Lib.Domain;

namespace AGIEvents.Lib.Services;

public class ShareService(ICsvService csvService) : IShareService
{
    async Task IShareService.ExportLeads(string fileName, LeadDetailRecordDto[] leads)
    {
        await ShareLeads("Share Leads", fileName, leads);
    }

    async Task IShareService.ExportLead(string fileName, LeadDetailRecordDto lead)
    {
        await ShareLeads("Share Lead", fileName, [lead]);
    }

    private async Task ShareLeads(string title, string fileName, IEnumerable<LeadDetailRecordDto> leads)
    {
        var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

        await csvService.WriteLeadsToFile(filePath, leads);
        await ShareFile(title, filePath);

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