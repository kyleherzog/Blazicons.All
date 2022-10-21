using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorComponentUtilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

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
