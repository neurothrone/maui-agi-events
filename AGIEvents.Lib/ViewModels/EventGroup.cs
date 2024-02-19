using System.Collections.ObjectModel;

namespace AGIEvents.Lib.ViewModels;

public class EventGroup(
    string groupName,
    IEnumerable<EventViewModel> events) : ObservableCollection<EventViewModel>(events)
{
    public string GroupName { get; init; } = groupName;

    public void SortByDescendingStartDate()
    {
        var sorted = this.OrderByDescending(e => e.StartDate).ToList();

        for (var i = 0; i < sorted.Count; i++)
        {
            Move(this.IndexOf(sorted[i]), i);
        }
    }
}