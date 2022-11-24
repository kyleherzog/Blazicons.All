using Blazicons.Demo.Models;

namespace Blazicons.Demo.Pages;

public partial class Index
{
    public IDictionary<string, SvgIcon> Icons { get; set; } = new Dictionary<string, SvgIcon>();

    public IDictionary<string, SvgIcon> FilteredIcons
    {
        get
        {
            if (string.IsNullOrEmpty(Search.Query))
            {
                return Icons;
            }

            return Icons.Where(x => x.Key.Contains(Search.Query, StringComparison.OrdinalIgnoreCase)).ToDictionary(x => x.Key, x => x.Value);
        }
    }

    public IconSearchModel Search { get; } = new IconSearchModel();

    protected override Task OnInitializedAsync()
    {
        var type = typeof(Blazicons.MdiIcon);
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var icon = (SvgIcon)property.GetValue(null);
            Icons.Add(property.Name, icon);
        }

        return base.OnInitializedAsync();
    }
}