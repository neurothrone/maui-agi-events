namespace AGIEvents.App;

public partial class App : Application
{
    public App()
    {
        // TODO: Dark Theme
        Application.Current.UserAppTheme = AppTheme.Dark;
        InitializeComponent();

        MainPage = new AppShell();
    }
}