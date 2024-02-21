using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Models;
using AGIEvents.Lib.Services;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class SettingsViewModel(
    IDatabaseRepository databaseRepository,
    INotificationService notificationService) : ObservableObject
{
    [ObservableProperty] private bool _isLoading;
    [ObservableProperty] private bool _showDeleteButton;

    public bool IsSafeDeleteOn
    {
        get => Preferences.Get(nameof(IsSafeDeleteOn), false);
        set
        {
            Preferences.Set(nameof(IsSafeDeleteOn), value);
            if (value != IsSafeDeleteOn)
                OnPropertyChanged();
        }
    }

    [RelayCommand]
    private async Task DeleteAllData()
    {
        IsLoading = true;

        await databaseRepository.DeleteAllDataAsync();
        WeakReferenceMessenger.Default.Send<AllDataDeletedMessage>();

        IsLoading = false;
        ShowDeleteButton = false;

        await notificationService.ShowSnackbar("Your data has successfully been deleted.");
    }

    [RelayCommand]
    private async Task NavigateToAboutPage()
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.AboutPage));
    }
}