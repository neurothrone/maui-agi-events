using CommunityToolkit.Mvvm.ComponentModel;

namespace AGIEvents.App.ViewModels;

internal partial class EventViewModel : ObservableObject
{
    private Models.Event _event;

    public string Id => _event.id;
    public string Title => _event.title;
    public string Image => _event.image;

    public DateTime StartDate => _event.startDate;
    public DateTime EndDate => _event.endDate;

    // TODO: Format: 19-21 Sep     StartDay-EndDay Month
    public string FormattedDateRange => $"{StartDate.Day}-{EndDate.Day} {StartDate:MMM}";

    public EventViewModel(Models.Event e)
    {
        _event = e;
    }
}