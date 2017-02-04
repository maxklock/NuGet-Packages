namespace Klockmann.Parsing.Html
{
    public abstract class BaseHtmlPart
    {
        #region constructors and destructors

        protected BaseHtmlPart(HtmlTag parent)
        {
            Parent = parent;
        }

        #endregion

        #region properties

        public HtmlTag Parent { get; set; }

        #endregion
    }
}