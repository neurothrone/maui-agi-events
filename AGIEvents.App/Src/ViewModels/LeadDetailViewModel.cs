using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.App.ViewModels;

[QueryProperty("LeadId", "LeadId")]
internal partial class LeadDetailViewModel : ObservableObject
{
    [ObservableProperty] private string _leadId;

    [RelayCommand]
    private async Task SaveChanges()
    {
        // TODO: save changes to database
        await Shell.Current.GoToAsync("..");
    }
}