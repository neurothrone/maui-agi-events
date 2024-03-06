namespace AGIEvents.App;
#if ANDROID
using Android.Content.Res;
#endif

public partial class App : Application
{
    public App()
    {
        Application.Current.UserAppTheme = AppTheme.Dark;

        // Remove underline from Android Entry control
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, _) =>
        {
#if ANDROID
            handler.PlatformView.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
#endif
        });

        InitializeComponent();

        MainPage = new AppShell();
    }
}