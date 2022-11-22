using System.Reflection;

namespace Blazicons.Demo.Pages;

public partial class Index
{
    public IDictionary<String, SvgIcon> Icons { get; set; } = new Dictionary<String, SvgIcon>();

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