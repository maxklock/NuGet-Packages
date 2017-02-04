namespace Klockmann.BotHelper.Extensions
{
    public static class MarkdownExtension
    {
        public static string MakeBold(this string str)
        {
            return "**" + str + "**";
        }

        public static string MakeItalic(this string str)
        {
            return "*" + str + "*";
        }

        public static string MakeInlineCode(this string code)
        {
            return "`" + code + "`";
        }

        public static string MakeHyperlink(this string link, string title)
        {
            return "[" + title + "](" + link + ")";
        }

        public static string AddLineBreak(this string str)
        {
            return str + "  \n";
        }
    }
}