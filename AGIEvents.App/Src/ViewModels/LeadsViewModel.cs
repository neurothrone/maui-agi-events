using AGIEvents.App.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.App.ViewModels;

public partial class LeadsViewModel : ObservableObject
{
    [RelayCommand]
    private async Task NavigateToLeadDetail()
    {
        await Shell.Current.GoToAsync(nameof(LeadDetailPage));
    }
}