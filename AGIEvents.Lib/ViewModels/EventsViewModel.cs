using System.Collections.ObjectModel;
using System.ComponentModel;
using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AGIEvents.Lib.ViewModels;

public partial class EventsViewModel : ObservableObject, IDisposable
{
    [ObservableProperty] private bool _isLoading;

    private ObservableCollection<EventViewModel> _yourEvents;

    public ObservableCollection<EventViewModel> YourEvents
    {
        get => _yourEvents;
        set => SetProperty(ref _yourEvents, value);
    }

    private ObservableCollection<EventViewModel> _comingEvents;

    public ObservableCollection<EventViewModel> ComingEvents
    {
        get => _comingEvents;
        set => SetProperty(ref _comingEvents, value);
    }

    public bool IsYourEventsEmpty => !IsLoading && YourEvents.Count == 0;
    public bool IsComingEventsEmpty => !IsLoading && ComingEvents.Count == 0;

    public EventsViewModel()
    {
        _yourEvents = [];
        _comingEvents = [];

        SubscribeToEvents();
        FetchEvents();
    }

    void IDisposable.Dispose()
    {
        UnsubscribeToEvents();
    }

    private void SubscribeToEvents()
    {
        PropertyChanged += OnYourEventsIsChanged;
        PropertyChanged += OnComingEventsIsChanged;
    }

    private void UnsubscribeToEvents()
    {
        PropertyChanged -= OnYourEventsIsChanged;
        PropertyChanged -= OnComingEventsIsChanged;
    }

    private void OnYourEventsIsChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IsLoading) or nameof(YourEvents))
            OnPropertyChanged(nameof(IsYourEventsEmpty));
    }

    private void OnComingEventsIsChanged(
        object? sender,
        PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(IsLoading) or nameof(ComingEvents))
            OnPropertyChanged(nameof(IsComingEventsEmpty));
    }


    [RelayCommand]
    private async Task AddEvent()
    {
        YourEvents.Add(
            new EventViewModel(new Event(
                    id: "2",
                    title: "Sign, Print & Promotion",
                    image: "sopno_logo.png",
                    startDate: DateTime.Now.AddDays(150),
                    endDate: DateTime.Now.AddDays(152)
                )
            )
        );
        OnPropertyChanged(nameof(YourEvents));
    }

    [RelayCommand]
    private async Task ClearEvents()
    {
        YourEvents.Clear();
        OnPropertyChanged(nameof(YourEvents));
    }

    [RelayCommand]
    private async Task AddEvents()
    {
        IsLoading = true;
        await Task.Delay(500);

        YourEvents = new ObservableCollection<EventViewModel>(
            [
                new EventViewModel(new Event(
                    id: "0",
                    title: "Sign&Print Scandinavia",
                    image: "sopse_logo.png",
                    startDate: DateTime.Now.AddDays(100),
                    endDate: DateTime.Now.AddDays(103)
                )),
                new EventViewModel(new Event(
                    id: "1",
                    title: "Sign Print & Pack Denmark",
                    image: "sopdk_logo.png",
                    startDate: DateTime.Now.AddDays(125),
                    endDate: DateTime.Now.AddDays(127)
                ))
            ]
        );

        IsLoading = false;
    }

    private async void FetchEvents()
    {
        IsLoading = true;
        await Task.Delay(500);

        // YourEvents = new ObservableCollection<EventViewModel>(
        //     [
        //         new EventViewModel(new Event(
        //             id: "0",
        //             title: "Sign&Print Scandinavia",
        //             image: "sopse_logo.png",
        //             startDate: DateTime.Now.AddDays(100),
        //             endDate: DateTime.Now.AddDays(103)
        //         )),
        //         new EventViewModel(new Event(
        //             id: "1",
        //             title: "Sign Print & Pack Denmark",
        //             image: "sopdk_logo.png",
        //             startDate: DateTime.Now.AddDays(125),
        //             endDate: DateTime.Now.AddDays(127)
        //         ))
        //     ]
        // );

        ComingEvents = new ObservableCollection<EventViewModel>(
            [
                new EventViewModel(
                    new Event(
                        id: "3",
                        title: "Sign, Print & Promotion",
                        image: "sopno_logo.png",
                        startDate: DateTime.Now.AddDays(165),
                        endDate: DateTime.Now.AddDays(168)
                    )
                ),
                new EventViewModel(
                    new Event(
                        id: "3",
                        title: "Sign, Print & Promotion",
                        image: "sopno_logo.png",
                        startDate: DateTime.Now.AddDays(165),
                        endDate: DateTime.Now.AddDays(168)
                    )
                ),
                new EventViewModel(
                    new Event(
                        id: "3",
                        title: "Sign, Print & Promotion",
                        image: "sopno_logo.png",
                        startDate: DateTime.Now.AddDays(165),
                        endDate: DateTime.Now.AddDays(168)
                    )
                ),
                new EventViewModel(
                    new Event(
                        id: "3",
                        title: "Sign, Print & Promotion",
                        image: "sopno_logo.png",
                        startDate: DateTime.Now.AddDays(165),
                        endDate: DateTime.Now.AddDays(168)
                    )
                ),
                new EventViewModel(
                    new Event(
                        id: "3",
                        title: "Sign, Print & Promotion",
                        image: "sopno_logo.png",
                        startDate: DateTime.Now.AddDays(165),
                        endDate: DateTime.Now.AddDays(168)
                    )
                ),
            ]
        );

        IsLoading = false;
    }

    [RelayCommand]
    private async Task NavigateToLeadsForEvent(EventViewModel eventViewModel)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(AppRoute.LeadsPage)}?EventId={eventViewModel.Id}"
        );
    }

    [RelayCommand]
    private async Task NavigateToSettings()
    {
        await Shell.Current.GoToAsync(nameof(AppRoute.SettingsPage));
    }
}