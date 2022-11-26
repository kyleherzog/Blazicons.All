namespace Blazicons.Demo.Models;

public class IconEntry
{
    public string Name { get; set; }

    public SvgIcon Icon { get; set; }

    public string Library { get; set; }

    public string Code => $"{Library}.{Name}";
}
