namespace Klockmann.Parsing.Xml
{
    public class XmlContent : BaseXmlPart
    {
        #region constructors and destructors

        public XmlContent(string value, XmlTag parent) : base(parent)
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