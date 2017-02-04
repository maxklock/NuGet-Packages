namespace Klockmann.Parsing.Html
{
    using System;
    using System.Linq;

    public class HtmlParser
    {
        #region methods

        public static HtmlTag ParseHtml(string html)
        {
            return ParseHtml(html, "script", "style");
        }

        public static HtmlTag ParseHtml(string html, params string[] ignoredTags)
        {
            html = html.Substring(html.IndexOf("<html", StringComparison.OrdinalIgnoreCase));
            HtmlTag last = null;

            do
            {
                var begin = html.IndexOf('<');
                var end = html.IndexOf('>');

                var comment = html.IndexOf("<!--", StringComparison.OrdinalIgnoreCase);
                if (begin == comment)
                {
                    html = html.Substring(html.IndexOf("-->", StringComparison.OrdinalIgnoreCase) + 3);
                    continue;
                }

                if (begin > 0)
                {
                    var str = html.Substring(0, begin);
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        last?.Children.Add(new HtmlContent(str, last));
                    }
                }

                var tag = html.Substring(begin + 1, end - begin - 1);
                var parts = tag.Split(new[] { ' ' }, 2);

                var ignored = ignoredTags.FirstOrDefault(t => t == parts[0].Trim());
                if (ignored != null)
                {
                    html = html.Substring(html.IndexOf("</" + ignored, StringComparison.OrdinalIgnoreCase));
                    html = html.Substring(html.IndexOf('>') + 1);
                    last?.Children.Add(new HtmlTag(ignored, last));
                    continue;
                }

                var htmlTag = new HtmlTag(parts[0].Trim(), last);

                var empty = false;
                var closingTag = string.Empty;

                if (parts[0].Trim().StartsWith("/"))
                {
                    closingTag = htmlTag.Tag.Substring(1);
                    htmlTag.Parent = last.Parent;
                }

                if (parts.Length > 1)
                {
                    var attr = parts[1].Replace('\'', '\"');
                    while (attr.Length > 0)
                    {
                        if (attr.Trim() == "/")
                        {
                            empty = true;
                            break;
                        }
                        var op = attr.IndexOf('=');
                        var key = attr.Substring(0, op);
                        attr = attr.Substring(op + 2);
                        string value;
                        if (attr.IndexOf('\"') < 0)
                        {
                            value = attr.IndexOf(' ') >= 0 ? attr.Substring(0, attr.IndexOf(' ')) : attr;
                            attr = attr.Substring(value.Length).Trim();
                        }
                        else
                        {
                            value = attr.Substring(0, attr.IndexOf('\"'));
                            attr = attr.Substring(value.Length + 1).Trim();
                        }
                        htmlTag.Attributes.Add(key, value);
                    }
                }

                html = html.Substring(end + 1);

                if (last == null)
                {
                    last = htmlTag;
                }
                else if (string.IsNullOrWhiteSpace(closingTag))
                {
                    last.Children.Add(htmlTag);
                    if (!empty)
                    {
                        last = htmlTag;
                    }
                }
                else if (last.Parent != null)
                {
                    while (last.Tag != closingTag)
                    {
                        last = last.Parent;
                    }
                    last = last.Parent;
                }
            }
            while (html.Length > 0);
            return last;
        }

        #endregion
    }
}