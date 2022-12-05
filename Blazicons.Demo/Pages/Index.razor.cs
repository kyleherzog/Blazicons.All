using System.Reactive.Linq;
using Blazicons.Demo.Models;
using CodeCasing;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Blazicons.Demo.Pages;

public partial class Index : IDisposable
{
    private bool hasDisposed;
    private string? libraryFilter;
    private IDisposable? queryChangedSubscription;

    public Index()
    {
        Search = new IconSearchModel();
        SubscribeToChanges();
    }

    private void SubscribeToChanges()
    {
        queryChangedSubscription = Search.WhenPropertyChanged.Throttle(TimeSpan.FromMilliseconds(400)).Subscribe(x => ActiveQuery = Search.Query);
    }

    private void UnsubsribeFromChanges()
    {
        queryChangedSubscription?.Dispose();
    }

    ~Index()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public string ActiveIconDisplayName => ActiveIcon?.Name.ExpandToTitleCase() ?? string.Empty;

    public IconEntry ActiveIcon { get; set; } = new IconEntry();

    public string ActiveIconNugetAddress => $"https://www.nuget.org/packages/Blazicons.{ActiveIcon.Assembly}";

    public string ActiveIconCopyNameLink => $"javascript:navigator.clipboard.writeText('{ActiveIcon.Code}');";

    public string ActiveIconExample => $"<Blazicon Svg=\"{ActiveIcon.Code}\"></Blazicon>";

    public string ActiveIconCopyExampleLink => $"javascript:navigator.clipboard.writeText('{ActiveIconExample}');";

    public bool IsShowingModal { get; set; }

    public IList<IconEntry> FilteredIcons
    {
        get
        {
            var result = Icons.AsEnumerable();
            if (!string.IsNullOrEmpty(ActiveQuery))
            {
                result = Icons.Where(x => x.Name.Contains(ActiveQuery, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(LibraryFilter))
            {
                result = result.Where(x => x.Library == LibraryFilter);
            }

            return result.ToList();
        }
    }

    private string? activeQuery;

    public string? ActiveQuery
    {
        get
        {
            return activeQuery;
        }

        set
        {
            if (activeQuery != value)
            {
                activeQuery = value;
                _ = InvokeAsync(StateHasChanged);
            }
        }
    }

    public IList<IconEntry> Icons { get; } = new List<IconEntry>();

    public string? LibraryFilter
    {
        get
        {
            return libraryFilter;
        }

        set
        {
            if (libraryFilter != value)
            {
                libraryFilter = value;
                StateHasChanged();
            }
        }
    }

    public IconSearchModel Search { get; }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!hasDisposed)
        {
            if (disposing)
            {
                UnsubsribeFromChanges();
            }

            hasDisposed = true;
        }
    }

    protected override Task OnInitializedAsync()
    {
        AddLibraryIcons(typeof(MdiIcon));
        AddLibraryIcons(typeof(FontAwesomeRegularIcon));
        AddLibraryIcons(typeof(FontAwesomeSolidIcon));
        AddLibraryIcons(typeof(BootstrapIcon));
        AddLibraryIcons(typeof(GoogleMaterialOutlinedIcon));
        AddLibraryIcons(typeof(GoogleMaterialFilledIcon));
        AddLibraryIcons(typeof(GoogleMaterialRoundIcon));
        AddLibraryIcons(typeof(GoogleMaterialSharpIcon));
        AddLibraryIcons(typeof(GoogleMaterialTwoToneIcon));
        AddLibraryIcons(typeof(Ionicon));
        AddLibraryIcons(typeof(FluentUiIcon));
        AddLibraryIcons(typeof(FluentUiFilledIcon));

        return base.OnInitializedAsync();
    }

    private void AddLibraryIcons(Type type)
    {
        var properties = type.GetProperties();

        foreach (var property in properties)
        {
            var icon = (SvgIcon)property.GetValue(null);
            Icons.Add(new IconEntry
            {
                Name = property.Name,
                Icon = icon,
                Library = type.Name,
                Assembly = type.Assembly?.GetName().Name ?? string.Empty,
            });
        }
    }

    private void HideModal()
    {
        IsShowingModal = false;
    }

    private void ShowModal()
    {
        IsShowingModal = true;
    }

    private void ShowIconDetails(IconEntry entry)
    {
        ActiveIcon = entry;
        ShowModal();
    }
}