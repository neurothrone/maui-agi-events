using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AGIEvents.Lib.ViewModels;

public partial class EventViewModel(Event e) : ObservableObject
{
    public string Id => e.id;
    public string Title => e.title;
    public string Image => e.image;

    // Format: 19-21 Sep
    public string FormattedDateRange => $"{e.startDate.Day}-{e.endDate.Day} {e.startDate:MMM}";
}