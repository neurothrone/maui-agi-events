using CommunityToolkit.Mvvm.ComponentModel;

namespace AGIEvents.App.ViewModels;

public partial class EventViewModel(Models.Event e) : ObservableObject
{
    public string Id => e.id;
    public string Title => e.title;
    public string Image => e.image;

    // Format: 19-21 Sep
    public string FormattedDateRange =>
        $"{e.startDate.Day}-{e.endDate.Day} {e.startDate:MMM}";
}