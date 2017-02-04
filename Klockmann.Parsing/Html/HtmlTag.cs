namespace Klockmann.Parsing.Html
{
    using System.Collections.Generic;
    using System.Linq;

    public class HtmlTag : BaseHtmlPart
    {
        #region constructors and destructors

        public HtmlTag(string tag, HtmlTag parent) : base(parent)
        {
            Tag = tag;
            Attributes = new Dictionary<string, string>();
            Children = new List<BaseHtmlPart>();
        }

        #endregion

        #region explicit interfaces

        public override string ToString()
        {
            return Tag;
        }

        #endregion

        #region properties

        public List<BaseHtmlPart> Children { get; set; }

        public IEnumerable<HtmlTag> ChildTags => Children.Where(c => c is HtmlTag).Cast<HtmlTag>();

        public IEnumerable<HtmlContent> ChildContent => Children.Where(c => c is HtmlContent).Cast<HtmlContent>();

        public Dictionary<string, string> Attributes { get; set; }

        public string Tag { get; set; }

        public IEnumerable<HtmlContent> GetHtmlContentRecursive()
        {
            var result = new List<HtmlContent>();
            foreach (var child in Children)
            {
                var tag = child as HtmlTag;
                if (tag == null)
                {
                    result.Add(child as HtmlContent);
                    continue;
                }
                var contents = tag.GetHtmlContentRecursive();
                result.AddRange(contents);
            }

            return result;
        }

        public IEnumerable<HtmlTag> GetTagsWithTag(string tag)
        {
            var result = new List<HtmlTag>();
            if (Tag == tag)
            {
                result.Add(this);
            }
            foreach (var child in Children.Where(c => c is HtmlTag).Cast<HtmlTag>())
            {
                result.AddRange(child.GetTagsWithTag(tag));
            }

            return result;
        }

        public IEnumerable<HtmlTag> GetTagsWithAttributeRecursive(string attribute, string value)
        {
            var result = new List<HtmlTag>();
            if (Attributes.ContainsKey(attribute) && Attributes[attribute] == value)
            {
                result.Add(this);
            }
            foreach (var child in Children.Where(c => c is HtmlTag).Cast<HtmlTag>())
            {
                result.AddRange(child.GetTagsWithAttributeRecursive(attribute, value));
            }

            return result;
        }

        #endregion
    }
}