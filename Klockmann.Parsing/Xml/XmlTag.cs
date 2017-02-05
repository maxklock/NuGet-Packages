namespace Klockmann.Parsing.Xml
{
    using System.Collections.Generic;
    using System.Linq;

    public class XmlTag : BaseXmlPart
    {
        #region constructors and destructors

        public XmlTag(string tag, XmlTag parent) : base(parent)
        {
            Tag = tag;
            Attributes = new Dictionary<string, string>();
            Children = new List<BaseXmlPart>();
        }

        #endregion

        #region explicit interfaces

        public override string ToString()
        {
            return Tag;
        }

        #endregion

        #region properties

        public List<BaseXmlPart> Children { get; set; }

        public IEnumerable<XmlTag> ChildTags => Children.Where(c => c is XmlTag).Cast<XmlTag>();

        public IEnumerable<XmlContent> ChildContent => Children.Where(c => c is XmlContent).Cast<XmlContent>();

        public Dictionary<string, string> Attributes { get; set; }

        public string Tag { get; set; }

        public IEnumerable<XmlContent> GetHtmlContentRecursive()
        {
            var result = new List<XmlContent>();
            foreach (var child in Children)
            {
                var tag = child as XmlTag;
                if (tag == null)
                {
                    result.Add(child as XmlContent);
                    continue;
                }
                var contents = tag.GetHtmlContentRecursive();
                result.AddRange(contents);
            }

            return result;
        }

        public IEnumerable<XmlTag> GetTagsWithTag(string tag)
        {
            var result = new List<XmlTag>();
            if (Tag == tag)
            {
                result.Add(this);
            }
            foreach (var child in Children.Where(c => c is XmlTag).Cast<XmlTag>())
            {
                result.AddRange(child.GetTagsWithTag(tag));
            }

            return result;
        }

        public IEnumerable<XmlTag> GetTagsWithAttributeRecursive(string attribute, string value)
        {
            var result = new List<XmlTag>();
            if (Attributes.ContainsKey(attribute) && Attributes[attribute] == value)
            {
                result.Add(this);
            }
            foreach (var child in Children.Where(c => c is XmlTag).Cast<XmlTag>())
            {
                result.AddRange(child.GetTagsWithAttributeRecursive(attribute, value));
            }

            return result;
        }

        #endregion
    }
}