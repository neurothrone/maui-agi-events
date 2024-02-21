using AGIEvents.Lib.Messages;
using AGIEvents.Lib.Services.Database;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace AGIEvents.Lib.ViewModels;

public partial class SettingsViewModel(IDatabaseRepository databaseRepository) : ObservableObject
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
        WeakReferenceMessenger.Default.Send<EventsDeletedMessage>();
        
        IsLoading = false;
        ShowDeleteButton = false;

        // TODO: Show Toast/Snackbar when operation is completed
    }
}