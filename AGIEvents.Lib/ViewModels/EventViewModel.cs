using AGIEvents.Lib.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AGIEvents.Lib.ViewModels;

public partial class EventViewModel(Event e) : ObservableObject
{
    public int Id => e.id;
    public string EventId => e.eventId;
    public string Title => e.title;
    public string Image => e.image;

    public DateTime StartDate => e.startDate;
    public DateTime EndDate => e.endDate;

    // Format: 19-21 Sep
    public string FormattedDateRange => $"{StartDate.Day}-{EndDate.Day} {StartDate:MMM}";

    public bool IsSaved => e.isSaved;

    public void ToggleIsSaved()
    {
        e.isSaved = !e.isSaved;
    }
}