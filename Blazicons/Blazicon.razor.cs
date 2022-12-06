using Microsoft.AspNetCore.Components;

namespace Blazicons;

public partial class Blazicon : ComponentBase
{
    /// <summary>
    /// Gets or sets the attributes specified but not explicitly mapped to a property.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, object> Attributes { get; set; } = new Dictionary<string, object>();

    [Parameter]
    public SvgIcon? Svg { get; set; }
}