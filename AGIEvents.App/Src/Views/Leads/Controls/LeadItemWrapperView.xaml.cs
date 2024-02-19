namespace AGIEvents.App.Views.Leads.Controls;

public partial class LeadItemWrapperView : ContentView
{
    private static readonly BindableProperty InnerContentProperty = BindableProperty.Create(
        nameof(InnerContent),
        typeof(View),
        typeof(LeadItemWrapperView),
        propertyChanged: InnerContentPropertyChanged
    );

    public View InnerContent
    {
        get => (View)GetValue(InnerContentProperty);
        set => SetValue(InnerContentProperty, value);
    }

    private static void InnerContentPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is LeadItemWrapperView view)
            view.MyContent.Content = (View)newValue;
    }

    public LeadItemWrapperView()
    {
        InitializeComponent();
    }
}