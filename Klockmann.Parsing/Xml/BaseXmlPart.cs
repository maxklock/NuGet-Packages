namespace Klockmann.Parsing.Xml
{
    public abstract class BaseXmlPart
    {
        #region constructors and destructors

        protected BaseXmlPart(XmlTag parent)
        {
            Parent = parent;
        }

        #endregion

        #region properties

        public XmlTag Parent { get; set; }

        #endregion
    }
}