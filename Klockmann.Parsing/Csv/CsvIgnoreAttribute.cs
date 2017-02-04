namespace Klockmann.Parsing.Csv
{
    using System;

    public class CsvIgnoreAttribute : Attribute
    {
        public bool IgnoreImport { get; set; }

        public bool IgnoreExport { get; set; }

        public CsvIgnoreAttribute()
        {
            IgnoreImport = false;
            IgnoreExport = false;
        }

        public CsvIgnoreAttribute(bool ignore) : this()
        {
            IgnoreImport = ignore;
            IgnoreExport = ignore;
        }
    }
}