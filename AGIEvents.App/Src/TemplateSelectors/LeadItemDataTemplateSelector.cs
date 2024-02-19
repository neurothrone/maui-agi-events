using AGIEvents.Lib.ViewModels;

namespace AGIEvents.App.TemplateSelectors;

public class LeadItemDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate BothFullTemplate { get; set; }
    public DataTemplate FullNameOnlyTemplate { get; set; }
    public DataTemplate CompanyOnlyTemplate { get; set; }

    protected override DataTemplate? OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not LeadItemViewModel viewModel)
            return null;

        return (!string.IsNullOrWhiteSpace(viewModel.FullName), !string.IsNullOrWhiteSpace(viewModel.Company)) switch
        {
            (true, true) => BothFullTemplate,
            (true, false) => FullNameOnlyTemplate,
            (false, true) => CompanyOnlyTemplate,
            _ => null
        };
    }
}