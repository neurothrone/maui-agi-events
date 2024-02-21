namespace AGIEvents.App.Views.About;

public partial class AboutPage : ContentPage
{
    public AboutPage(Lib.ViewModels.AboutViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}