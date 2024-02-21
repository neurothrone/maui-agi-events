using CommunityToolkit.Mvvm.ComponentModel;

namespace AGIEvents.Lib.ViewModels;

// TODO: Preferences Service with an enum of possible values, to get them

public partial class SettingsViewModel : ObservableObject
{
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
}