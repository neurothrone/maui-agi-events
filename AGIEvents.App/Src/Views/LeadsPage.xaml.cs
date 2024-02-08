using AGIEvents.App.ViewModels;

namespace AGIEvents.App.Views;

public partial class LeadsPage : ContentPage
{
    public LeadsPage()
    {
        InitializeComponent();
        BindingContext = new LeadsViewModel();
    }

    // TODO: Change "Your Leads" Header to have the image between "Your Leads" and Button in Landscape?

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone && Width > Height)
        {
            EventImage.IsVisible = false;
        }
        else
        {
            EventImage.IsVisible = true;
        }
    }
}