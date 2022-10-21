using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazicons;

public class SvgIcon
{
    private SvgIcon(string content, string viewBox)
    {
        Content = content;
        ViewBox = viewBox;
    }

    public string Content { get; }

    public string Markup
    {
        get
        {
            return $"<svg viewBox='{ViewBox}'>{Content}</svg>";
        }
    }

    public string ViewBox { get; }

    public static SvgIcon FromPathData(string pathData, string viewBox = "0 0 24 24")
    {
        return new SvgIcon($"<path d=\"{pathData}\" />", viewBox);
    }

    public static SvgIcon FromContent(string content, string viewBox = "0 0 24 24")
    {
        return new SvgIcon(content, viewBox);
    }
}
