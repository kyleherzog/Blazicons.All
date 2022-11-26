using System.Text;
using Microsoft.AspNetCore.Components;

namespace Blazicons.Demo.Components;

public partial class FontLibrarySelection
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public string Id { get; } = Guid.NewGuid().ToString();

    [Parameter]
    public bool IsSelected { get; set; }
}