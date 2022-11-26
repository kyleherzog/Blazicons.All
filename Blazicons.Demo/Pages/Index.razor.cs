﻿using Blazicons.Demo.Models;

namespace Blazicons.Demo.Pages;

public partial class Index
{
    public IList<IconEntry> Icons { get; } = new List<IconEntry>();

    public IList<IconEntry> FilteredIcons
    {
        get
        {
            if (string.IsNullOrEmpty(Search.Query))
            {
                return Icons;
            }

            return Icons.Where(x => x.Name.Contains(Search.Query, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }

    public IconSearchModel Search { get; } = new IconSearchModel();

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
            });
        }
    }
}