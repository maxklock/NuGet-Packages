namespace Klockmann.Parsing.Html
{
    public class HtmlContent : BaseHtmlPart
    {
        #region constructors and destructors

        public HtmlContent(string value, HtmlTag parent) : base(parent)
        {
            Value = value;
        }

        #endregion

        #region explicit interfaces

        public override string ToString()
        {
            return Value;
        }

        #endregion

        #region properties

        public string Value { get; set; }

        #endregion
    }
}